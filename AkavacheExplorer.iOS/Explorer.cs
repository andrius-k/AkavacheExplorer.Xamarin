using UIKit;
namespace AkavacheExplorer.iOS
{
    public static class Explorer
    {
        public static UINavigationController GetNavigationController()
        {
            return (UINavigationController)Helpers.Storyboard
                                                  .InstantiateInitialViewController();
        }
    }
}
