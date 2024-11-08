using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FastType.Services;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;

namespace FastType.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private ObservableCollection<string> Words { get; } = [];

    [ObservableProperty]
    private string selectedWord = string.Empty;

    [ObservableProperty]
    private string writtenWord = string.Empty;

    [ObservableProperty]
    private int score = 0;

    public async Task LoadWordsAsync()
    {
        if (Words.Count <= 100)
        {
            Words.Clear();

            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("FastType.Resources.Raw.Words.json")!;
            using StreamReader reader = new(stream);

            string jsonContent = await reader.ReadToEndAsync();
            var wordList = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);

            if (wordList != null && wordList.ContainsKey("words"))
                foreach (var word in wordList["words"])
                    Words.Add(word);
        }

        SelectedWord = Words[Random.Shared.Next(0, Words.Count - 1)];
    }

    [RelayCommand]
    public async Task ConfirmWord()
    {
        var MatchScore = ScoreService.Match(SelectedWord, WrittenWord);

    }
}
