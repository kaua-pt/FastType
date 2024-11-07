/* [grial-metadata] id: Grial#NewsLoginPage.cs version: 1.0.1 */
using FastType.ViewModels;

namespace FastType;

public partial class GamePage : ContentPage
{
    public GamePage(GameViewModel gameViewModel)
    {
        InitializeComponent();
        BindingContext = gameViewModel;
    }
}
