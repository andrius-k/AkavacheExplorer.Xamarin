using Android.App;
using Android.Widget;
using Android.OS;
using Akavache;
using System.Reactive.Linq;
using AkavacheExplorer.Droid;

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
            
            var openButton = FindViewById<Button>(Resource.Id.openButton);
            openButton.Click += (sender, e) => 
            {
				// Simply start ExplorerActivity
				StartActivity(typeof(ExplorerActivity));
            };
        }
    }
}

