/* [grial-metadata] id: Grial#NewsSourcesViewModel.cs version: 1.0.1 */
namespace FastType
{
    public class ScoreTertiaryViewModel : ObservableObject
    {
        private readonly INavigation _navigation;

        public ScoreTertiaryViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        public Command ToggleFollowCommand { get; }
        public Command SourceTappedCommand { get; }
    }
}