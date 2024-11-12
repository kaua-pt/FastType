/* [grial-metadata] id: Grial#NewsTopicsViewModel.cs version: 1.0.1 */
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FastType;

public partial class OptionsViewModel : ObservableObject
{
    public OptionsViewModel()
    {
        LoadData();

        TopicTappedCommand = new Command<NewsTopicData>(async (topic) => await GoToTopic(topic));
    }

    public Command TopicTappedCommand { get; }

    public ObservableCollection<NewsTopicData> Topics { get; } = new ObservableCollection<NewsTopicData>();

    [RelayCommand]
    public async Task GoToTopic(NewsTopicData topic)
    {
        switch (topic.SectionTitle)
        {
            case "Jogar":
                await Shell.Current.GoToAsync(nameof(GamePage));
                break;
            case "Opções":
                break;
            case "Instruções":
                break;
            case "Sair":
                break;
        }
    }

    private void LoadData()
    {
        Topics.Clear();

        JsonHelper.Instance.LoadViewModel(this, source: "News.json", pageName: "NewsTopicsPage.xaml");
    }
}