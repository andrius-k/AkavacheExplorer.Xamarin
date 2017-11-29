using System;
using UIKit;
using Akavache;
using System.Reactive.Linq;
using AkavacheExplorer.iOS;
using System.Text;
using Foundation;

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
            
            // Insert binaray data
            string someText = "some looong text!!! :)";
            byte[] textByteArray = Encoding.ASCII.GetBytes(someText);
            await BlobCache.LocalMachine.Insert("binary", textByteArray);   

            // Insert image
            var image = UIImage.FromBundle("image");
            using (NSData imageData = image.AsPNG())
            {
                byte[] imageByteArray = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, imageByteArray, 0, Convert.ToInt32(imageData.Length));
                await BlobCache.LocalMachine.Insert("image", imageByteArray);
            }

            openButton.TouchUpInside += (sender, e) => 
            {
				// Simply present navigation controller
				PresentViewController(Explorer.GetNavigationController(), true, null);
            };
        }
    }
}
