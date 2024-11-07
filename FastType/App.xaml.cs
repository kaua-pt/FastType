/* [grial-metadata] id: Grial#App.xaml version: 1.1.3 */
namespace FastType
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new FastType.GrialEasyNavigationPage());
        }
    }
}
