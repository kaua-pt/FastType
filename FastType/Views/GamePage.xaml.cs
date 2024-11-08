/* [grial-metadata] id: Grial#NewsLoginPage.cs version: 1.0.1 */
using FastType.ViewModels;

namespace FastType;

public partial class GamePage : ContentPage
{
    public GameViewModel GameViewModel { get; set; }
    public GamePage()
    {
        InitializeComponent();
        GameViewModel = new GameViewModel();
        BindingContext = GameViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GameViewModel.LoadWordsAsync();
    }
}
