/* [grial-metadata] id: Grial#NewsTopicsPage.cs version: 1.0.1 */
using FastType.ViewModels;

namespace FastType;

public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();

        BindingContext = new MenuViewModel();
    }
}
