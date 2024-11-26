using CommunityToolkit.Mvvm.Input;
using FastType;
using System.Collections.ObjectModel;

public class MenuViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    public MenuViewModel()
    {
        LoadData();
        TopicTappedCommand = new AsyncRelayCommand<NewsTopicData>(GoToTopic);
    }

    public IAsyncRelayCommand<NewsTopicData> TopicTappedCommand { get; }

    public ObservableCollection<NewsTopicData> Topics { get; } = new();

    private async Task GoToTopic(NewsTopicData topic)
    {
        if (topic == null)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", "Topic data is missing.", "Ok");
            return;
        }

        if (topic.SectionTitle.Equals("Jogar"))
            await Shell.Current.GoToAsync(nameof(GamePage));

    }

    private void LoadData()
    {
        Topics.Clear();
        JsonHelper.Instance.LoadViewModel(this, source: "News.json", pageName: "NewsTopicsPage.xaml");
    }
}
