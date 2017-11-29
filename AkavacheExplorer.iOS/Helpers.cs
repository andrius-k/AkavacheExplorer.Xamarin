using UIKit;
namespace AkavacheExplorer.iOS
{
    internal static class Helpers
    {
        private const string STORYBOARD_NAME = "ExplorerMain";
        private static UIStoryboard _storyboard;

        internal static UIStoryboard Storyboard
        {
            get => _storyboard ?? 
                (_storyboard = UIStoryboard.FromName(STORYBOARD_NAME, null));
        }
    }
}
