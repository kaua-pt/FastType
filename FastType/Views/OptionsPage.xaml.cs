/* [grial-metadata] id: Grial#NewsTopicsPage.cs version: 1.0.1 */
using FastType.ViewModels;

namespace FastType;

public partial class OptionsPage : ContentPage
{
    public OptionsPage()
    {
        InitializeComponent();

        BindingContext = new OptionsViewModel();
    }
}
