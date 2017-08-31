
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Akavache;
using System.Reactive.Linq;
using Newtonsoft.Json;

namespace AkavacheExplorer.Droid
{
    [Activity(Label = "DataActivity")]
    internal class DataActivity : Activity
    {
        public const string LOCAL_STORE_KEY = "local_store_key";

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_data);

            var textView = FindViewById<TextView>(Resource.Id.datatTextView);

            var key = Intent?.GetStringExtra(LOCAL_STORE_KEY);
            if(key == null)
            {
                Toast.MakeText(Application, "Akavache store key was not found", ToastLength.Long).Show();
                return;
            }

			Title = key;

			try
			{
				var data = await BlobCache.LocalMachine.GetObject<object>(key);
				var json = JsonConvert.SerializeObject(data, Formatting.Indented);
				textView.Text = json;
			}
			catch (Exception ex)
			{
				textView.Text = ex.Message;

				Toast.MakeText(Application, "Error retrieving data from local store", ToastLength.Long).Show();
			}
        }
    }
}
