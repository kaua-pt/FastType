using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FastType.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

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

    public GameViewModel()
    {
        LoadWords();
    }

    private void LoadWords()
    {
        Words.Clear();

        var filePath = "Data/Words.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            List<string> wordsJson = JsonConvert.DeserializeObject<List<string>>(jsonData) ?? [];
            if (wordsJson.Count == 0)
                return;
            foreach (var word in wordsJson)
                Words.Add(word);
        }
    }

    [RelayCommand]
    public async Task ConfirmWord()
    {
        ScoreService.Match(selectedWord, writtenWord);
    }
}
