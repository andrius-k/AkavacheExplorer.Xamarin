using UIKit;
namespace AkavacheExplorer.iOS
{
    internal static class Extensions
    {
        /// <summary>
        /// Finds hairline (separator) view in navigation bar
        /// </summary>
        internal static UIImageView GetHairlineView(this UINavigationController nav)
        {
            if (nav != null)
            {
                foreach (var aView in nav.NavigationBar.Subviews)
                {
                    foreach (var bView in aView.Subviews)
                    {
                        if (bView is UIImageView &&
                           bView.Bounds.Size.Width == nav.NavigationBar.Frame.Size.Width &&
                           bView.Bounds.Size.Height < 2)
                        {
                            return (bView as UIImageView);
                        }
                    }
                }
            }
            // Defaut
            return null;
        }
    }
}
