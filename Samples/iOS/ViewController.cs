using System;

using UIKit;
using Akavache;
using System.Reactive.Linq;
using AkavacheExplorer.iOS;

namespace AkavacheExplorerSample.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bands = Helpers.GenerateSampleData();

            // Insert all bands
            await BlobCache.LocalMachine.InsertObject("all_bands", bands);

            // Insert each band separately
            foreach(var band in bands)
                await BlobCache.LocalMachine.InsertObject($"band_{band.Id}", band);

            openButton.TouchUpInside += (sender, e) => 
            {
				// Simply present navigation controller
				PresentViewController(Explorer.GetNavigationController(), true, null);
            };
        }
    }
}
