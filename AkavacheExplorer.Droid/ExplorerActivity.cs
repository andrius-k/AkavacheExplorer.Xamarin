
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

namespace AkavacheExplorer.Droid
{
    [Activity(Label = "KeysActivity")]
    public class ExplorerActivity : Activity
    {
        private ListView _listView;
        private IEnumerable<string> _keys;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_keys);

            Title = "Local Machine";

            _listView = FindViewById<ListView>(Resource.Id.listView);

			try
			{
                _keys = await BlobCache.LocalMachine.GetAllKeys();
                _listView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, _keys.ToArray());
			}
			catch (Exception ex)
			{
                Toast.MakeText(Application, $"Exception retrieving keys: {ex.Message}", ToastLength.Long).Show();
			}

            _listView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(DataActivity));
            intent.PutExtra(DataActivity.LOCAL_STORE_KEY, _keys.ElementAt(e.Position));

            StartActivity(intent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _listView.ItemClick -= ListView_ItemClick;
        }
    }
}
