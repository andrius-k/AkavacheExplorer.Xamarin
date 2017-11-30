using System;
using System.Text;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Akavache;
using System.Reactive.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Android.Graphics;
using Splat;

namespace AkavacheExplorer.Droid
{
    [Activity(Label = "DataActivity")]
    internal class DataActivity : Activity
    {
        public const string LOCAL_STORE_KEY = "local_store_key";

        private RadioGroup _radioGroup;
        private ScrollView _textScrollView;
        private TextView _textView;
        private ImageView _imageView;

        private string _key;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.explorer_activity_data);

            _radioGroup = FindViewById<RadioGroup>(Resource.Id.explorer_content_radio_group);
            _textScrollView = FindViewById<ScrollView>(Resource.Id.explorer_text_scroll_view);
            _textView = FindViewById<TextView>(Resource.Id.explorer_data_text_view);
            _imageView = FindViewById<ImageView>(Resource.Id.explorer_image_view);

            _radioGroup.Check(Resource.Id.explorer_radio_json);
            _radioGroup.CheckedChange += RadioGroup_CheckedChange;

            _key = Intent?.GetStringExtra(LOCAL_STORE_KEY);
            if(_key == null)
            {
                Toast.MakeText(Application, "Akavache store key was not found", ToastLength.Long).Show();
                return;
            }

			Title = _key;

            await ViewAsJson();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _radioGroup.CheckedChange -= RadioGroup_CheckedChange;
        }

        private async void RadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            if(e.CheckedId == Resource.Id.explorer_radio_json)
            {
                await ViewAsJson();
            }
            else if (e.CheckedId == Resource.Id.explorer_radio_text)
            {
                await ViewAsText();
            }
            else if (e.CheckedId == Resource.Id.explorer_radio_image)
            {
                await ViewAsImage();
            }
            else
            {
                await ViewAsText();
            }
        }

        private async Task ViewAsJson()
        {
            _imageView.Visibility = ViewStates.Gone;
            _textScrollView.Visibility = ViewStates.Visible;
            ClearScreen();

            try
            {
                var data = await BlobCache.LocalMachine.GetObject<object>(_key);
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);

                _textView.Text = json;
                _textView.SetTypeface(null, TypefaceStyle.Normal);
            }
            catch (Exception ex)
            {
                _textView.Text = ex.Message;

                // Make text italic if we are presenting an error instead of data
                _textView.SetTypeface(null, TypefaceStyle.Italic);
            }
        }

        private async Task ViewAsText()
        {
            _imageView.Visibility = ViewStates.Gone;
            _textScrollView.Visibility = ViewStates.Visible;
            ClearScreen();

            try
            {
                var data = await BlobCache.LocalMachine.Get(_key);
                var text = Encoding.ASCII.GetString(data);

                _textView.Text = text;
                _textView.SetTypeface(null, TypefaceStyle.Normal);
            }
            catch (Exception ex)
            {
                _textView.Text = ex.Message;

                // Make text italic if we are presenting an error instead of data
                _textView.SetTypeface(null, TypefaceStyle.Italic);
            }
        }

        private async Task ViewAsImage()
        {
            _imageView.Visibility = ViewStates.Visible;
            _textScrollView.Visibility = ViewStates.Gone;
            ClearScreen();

            try
            {
                var bitmap = await BlobCache.LocalMachine.LoadImage(_key);
                var image = bitmap.ToNative();

                _imageView.SetImageDrawable(image);
            }
            catch (Exception ex)
            {
                _imageView.Visibility = ViewStates.Gone;
                _textScrollView.Visibility = ViewStates.Visible;

                _textView.Text = ex.Message;

                // Make text italic if we are presenting an error instead of data
                _textView.SetTypeface(null, TypefaceStyle.Italic);
            }
        }

        private void ClearScreen()
        {
            _imageView.SetImageDrawable(null);
            _textView.Text = string.Empty;
        }
    }
}
