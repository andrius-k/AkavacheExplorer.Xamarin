using System;
using UIKit;
namespace AkavacheExplorer.iOS
{
    public static class Explorer
    {
        public static UINavigationController GetNavigationController()
        {
            var root = new KeysViewController();
            var navigationController = new UINavigationController(root);

            return navigationController;
        }
    }
}
