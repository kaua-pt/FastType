/* [grial-metadata] id: Grial#NewsListItemTemplate.cs version: 1.0.1 */
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FastType;

public partial class NewsListItemTemplate : ContentView
{
    public static readonly BindableProperty ToggleFavoriteCommandProperty = BindableProperty.Create(
        nameof(ToggleFavoriteCommand),
        typeof(ICommand),
        typeof(NewsListItemTemplate),
        null);

    public ICommand ToggleFavoriteCommand
    {
        get { return (ICommand)GetValue(ToggleFavoriteCommandProperty); }
        set { SetValue(ToggleFavoriteCommandProperty, value); }
    }

    public static readonly BindableProperty IsRelatedStoryProperty = BindableProperty.Create(
        nameof(IsRelatedStory),
        typeof(bool),
        typeof(NewsListItemTemplate),
        false);

    public bool IsRelatedStory
    {
        get { return (bool)GetValue(IsRelatedStoryProperty); }
        set { SetValue(IsRelatedStoryProperty, value); }
    }

    public NewsListItemTemplate()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == IsRelatedStoryProperty.PropertyName)
        {
            bookmark.IsVisible = !IsRelatedStory;
        }
    }

    void OnArticleTapped(object sender, TappedEventArgs e)
    {
    }
}
