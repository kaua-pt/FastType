/* [grial-metadata] id: Grial#NewsSourcesPage.cs version: 1.0.1 */
namespace FastType;

public partial class NewsSourcesPage : ContentPage
{
	public NewsSourcesPage()
	{
		InitializeComponent();

        BindingContext = new ScoreTertiaryViewModel(Navigation);
    }
}
