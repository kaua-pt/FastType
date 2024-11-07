/* [grial-metadata] id: Grial#NewsListPage.cs version: 1.0.1 */
namespace FastType;

public partial class NewsListPage : ContentPage
{
	public NewsListPage()
	{
		InitializeComponent();

        BindingContext = new ScoreViewModel(Navigation);
    }
}
