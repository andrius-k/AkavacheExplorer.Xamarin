using Android.App;
using Android.Widget;
using Android.OS;
using Akavache;
using System.Reactive.Linq;
using AkavacheExplorer.Droid;
using Android.Graphics;
using System.IO;
using System.Text;

namespace AkavacheExplorerSample.Droid
{
    [Activity(Label = "AkavacheExplorerSample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

			// Insert all bands
            var bands = Helpers.GenerateSampleData();

			await BlobCache.LocalMachine.InsertObject("all_bands", bands);

			// Insert each band separately
			foreach (var band in bands)
				await BlobCache.LocalMachine.InsertObject($"band_{band.Id}", band);

            // Insert binaray data
            string someText = "some looong text!!! :)";
            byte[] textByteArray = Encoding.ASCII.GetBytes(someText);
            await BlobCache.LocalMachine.Insert("binary", textByteArray);

            // Insert image
            var image = BitmapFactory.DecodeResource(Resources, Resource.Drawable.image);
            using (var stream = new MemoryStream())
            {
                image.Compress(Bitmap.CompressFormat.Png, 100, stream);
                var bytes = stream.ToArray();
                await BlobCache.LocalMachine.Insert("image", bytes);
            }
            
            var openButton = FindViewById<Button>(Resource.Id.open_button);
            openButton.Click += (sender, e) => 
            {
				// Simply start ExplorerActivity
				StartActivity(typeof(ExplorerActivity));
            };
        }
    }
}

