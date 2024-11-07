/* [grial-metadata] id: Grial#NewsTopicsPage.cs version: 1.0.1 */
namespace FastType;

public partial class NewsTopicsPage : ContentPage
{
	public NewsTopicsPage()
	{
		InitializeComponent();

        BindingContext = new ScoreSecondaryViewModel();
    }
}
