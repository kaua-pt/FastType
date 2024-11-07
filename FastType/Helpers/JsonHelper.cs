/* [grial-metadata] id: Grial#JsonHelper.cs version: 1.0.1 */
using UXDivers.Grial;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FastType
{
    public class JsonHelper
    {
        private static JsonHelper _Instance;
        private JsonResources _resources;

        private JsonHelper()
        {
            _resources = new JsonResources();
        }

        public static JsonHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new JsonHelper();
                }

                return _Instance;
            }
        }

        public void LoadViewModel(object viewModel, string source, string pageName = null)
        {
            var name = pageName;
            if (string.IsNullOrEmpty(name))
            {
                const string ViewModelSuffix = "ViewModel";
                name = viewModel.GetType().Name;
                if (name.EndsWith(ViewModelSuffix, StringComparison.Ordinal))
                {
                    name = name.Substring(0, name.Length - ViewModelSuffix.Length);
                }

                name += "Page.xaml";
            }

            Populate(viewModel, $"$.['{name}']", _resources.GetResource(source));
        }

        private void Populate(object target, string path, JObject source)
        {
            var token = source?.SelectToken(path);
            if (token != null)
            {
                JsonConvert.PopulateObject(token.ToString(), target);
            }
        }

        private class JsonResources
        {
            private static ICultureService _CultureService;

            private readonly Dictionary<string, JObject> _sources;
            private readonly string[] _resourceNames;

            public JsonResources()
            {
                _sources = new Dictionary<string, JObject>();
                _resourceNames = typeof(JsonHelper).GetTypeInfo().Assembly.GetManifestResourceNames();
            }

            public static ICultureService CultureService
            {
                get
                {
                    if (_CultureService == null)
                    {
                        _CultureService = ServiceHelper.GetService<ICultureService>();
                    }

                    return _CultureService;
                }
            }

            public JObject GetResource(string source)
            {
                var culture = CultureService.CurrentCulture;
                var key = $"{culture.Name}/{source}";

                if (!_sources.TryGetValue(key, out JObject result))
                {
                    var assembly = typeof(JsonHelper).Assembly;

                    string fullResourceName = null;
                    for (var i = 0; i < _resourceNames.Length; i++)
                    {
                        if (_resourceNames[i].EndsWith(source, StringComparison.Ordinal))
                        {
                            fullResourceName = _resourceNames[i];
                            break;
                        }
                    }

                    if (fullResourceName == null)
                    {
                        return null;
                    }

                    Assembly satelliteAssembly = null;
                    if (culture.TwoLetterISOLanguageName != "en")
                    {
                        try
                        {
                            satelliteAssembly = assembly.GetSatelliteAssembly(culture);
                        }
                        catch
                        {
                            // No satellite assembly, keep en strings
                        }
                    }

                    try
                    {
                        // If resource not present in satellite use en strings
                        var stream =
                            satelliteAssembly?.GetManifestResourceStream(fullResourceName) ??
                            assembly.GetManifestResourceStream(fullResourceName);

                        if (stream != null)
                        {
                            string text;
                            using (var reader = new StreamReader(stream))
                            {
                                text = reader.ReadToEnd();
                            }

                            result = JObject.Parse(text, new JsonLoadSettings());
                            ResolveReferences(result, null, new Dictionary<string, JObject>(), result);

                            _sources[key] = result;
                        }
                    }
                    catch (Exception)
                    {
                        result = null;
#if DEBUG
                        throw;
#endif
                    }
                }

                return result;
            }

            private static void ResolveReferences(JObject current, Action<JToken> update, IDictionary<string, JObject> refdic, JObject root)
            {
                if (current == null)
                {
                    return;
                }

                var value = current["$ref"];
                if (value != null)
                {
                    if (update != null)
                    {
                        var reference = (string)value;
                        if (reference.StartsWith('$'))
                        {
                            var target = root.SelectToken(reference);
                            if (target != null)
                            {
                                update(target);
                            }
                        }
                        else
                        {
                            if (refdic.TryGetValue((string)value, out JObject obj))
                            {
                                update(obj);
                            }
                        }
                    }

                    return;
                }

                value = current["$id"];
                if (value != null)
                {
                    refdic[(string)value] = current;
                    current.Remove("$id");

                    if (current.Property("Id") == null)
                    {
                        // Map into an actual Id property in case it's needed
                        current.Add("Id", value);
                    }
                }

                foreach (var prop in current.Properties())
                {
                    if (prop.Value is JArray arr)
                    {
                        for (var i = 0; i < arr.Count; i++)
                        {
                            ResolveReferences(arr[i] as JObject, o => arr[i] = o, refdic, root);
                        }
                    }
                    else if (prop.Value is JObject obj)
                    {
                        ResolveReferences(obj, o => current[prop.Name] = o, refdic, root);
                    }
                }
            }
        }
    }
}
