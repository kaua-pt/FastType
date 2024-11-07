/* [grial-metadata] id: Grial#NewsCarouselItemTemplate.cs version: 1.0.1 */
using System.Windows.Input;

namespace FastType;

public partial class NewsCarouselItemTemplate : ContentView
{
    public static readonly BindableProperty GoToArticleCommandProperty = BindableProperty.Create(
        nameof(GoToArticleCommand),
        typeof(ICommand),
        typeof(NewsCarouselItemTemplate),
        null);

    public ICommand GoToArticleCommand
    {
        get { return (ICommand)GetValue(GoToArticleCommandProperty); }
        set { SetValue(GoToArticleCommandProperty, value); }
    }

    public static readonly BindableProperty ToggleFavoriteCommandProperty = BindableProperty.Create(
        nameof(ToggleFavoriteCommand),
        typeof(ICommand),
        typeof(NewsCarouselItemTemplate),
        null);

    public ICommand ToggleFavoriteCommand
    {
        get { return (ICommand)GetValue(ToggleFavoriteCommandProperty); }
        set { SetValue(ToggleFavoriteCommandProperty, value); }
    }

    public NewsCarouselItemTemplate()
	{
		InitializeComponent();
	}
}
