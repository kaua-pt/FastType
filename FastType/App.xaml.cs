/* [grial-metadata] id: Grial#App.xaml version: 1.1.3 */
namespace FastType
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new FastType.AppShell());
        }
    }
}
