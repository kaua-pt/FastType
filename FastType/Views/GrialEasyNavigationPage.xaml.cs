/* [grial-metadata] id: Grial#GrialEasyNavigationPage.xaml version: 1.0.1 */
using System.Windows.Input;

namespace FastType;

public partial class GrialEasyNavigationPage : ContentPage
{
	public GrialEasyNavigationPage()
	{
		InitializeComponent();

        LoadPages();
	}

    private void LoadPages()
    {
        var items = new List<PageItem>();

        var thisType = typeof(GrialEasyNavigationPage);

        var acceptedTypes = new Type[] { typeof(ContentPage), typeof(TabbedPage) };

        foreach (var type in thisType.Assembly.GetTypes())
        {
            if (type != thisType &&
                type.Name.EndsWith("Page") &&
                type.Name != "MainPage" &&
                acceptedTypes.Any(type.IsAssignableTo))
            {
                items.Add(new PageItem(type, Navigation));
            }
        }

        BindingContext = items.OrderBy(x => x.Name).ToList();
    }

    private class PageItem
    {
        private readonly INavigation _navigation;

        public PageItem(Type targetType, INavigation navigation)
        {
            Name = targetType.Name;
            _navigation = navigation;

            OpenCommand = new Command(() =>
            {
                var page = targetType.GetConstructor(Array.Empty<Type>()).Invoke(null) as Page;

                // Heuristic to decide between modal and regular navigation
                if (NavigationPage.GetHasNavigationBar(page))
                {
                    _navigation.PushAsync(page);
                }
                else
                {
                    _navigation.PushModalAsync(page);
                }
            });
        }

        public string Name { get; }

        public ICommand OpenCommand { get; }
    }
}
