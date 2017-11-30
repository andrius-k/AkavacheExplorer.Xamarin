using Foundation;
using System;
using UIKit;
using Akavache;
using Newtonsoft.Json;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Text;
using Splat;

namespace AkavacheExplorer.iOS
{
    internal partial class DataViewController : UIViewController, IUIToolbarDelegate
    {
        private UIImageView _navHairline;

        public DataViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetKey(string key)
        {
            _key = key;
        }

        private string _key;

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = _key;

            toolbar.Delegate = this;

            segmentedControl.RemoveAllSegments();
            segmentedControl.InsertSegment(NSBundle.MainBundle.LocalizedString("explorer_title_view_as_json", ""), 0, false);
            segmentedControl.InsertSegment(NSBundle.MainBundle.LocalizedString("explorer_title_view_as_text", ""), 1, false);
            segmentedControl.InsertSegment(NSBundle.MainBundle.LocalizedString("explorer_title_view_as_image", ""), 2, false);
            segmentedControl.SelectedSegment = 0;
            segmentedControl.ValueChanged += SegmentedControl_ValueChanged;

            textView.Editable = false;
            textView.Hidden = false;
            imageView.Hidden = true;

            // Find hairline (separator) view in navigation bar
            _navHairline = NavigationController?.GetHairlineView();

            await ViewAsJson();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (_navHairline != null)
                _navHairline.Hidden = true;
            else
            {
                // Try again
                _navHairline = NavigationController?.GetHairlineView();
                if (_navHairline != null)
                    _navHairline.Hidden = true;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            segmentedControl.ValueChanged -= SegmentedControl_ValueChanged;

            if (_navHairline != null)
                _navHairline.Hidden = false;
        }

        [Export("positionForBar:")]
        public UIBarPosition GetPositionForBar(IUIBarPositioning barPositioning)
        {
            return UIBarPosition.TopAttached;
        }

        private async void SegmentedControl_ValueChanged(object sender, EventArgs e)
        {
            switch(segmentedControl.SelectedSegment)
            {
                case 0:
                    await ViewAsJson();
                    break;
                case 1:
                    await ViewAsText();
                    break;
                case 2:
                    await ViewAsImage();
                    break;
                default:
                    await ViewAsText();
                    break;
            }
        }

        private async Task ViewAsJson()
        {
            imageView.Hidden = true;
            textView.Hidden = false;
            ClearScreen();

            try
            {
                var data = await BlobCache.LocalMachine.GetObject<object>(_key);
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);

                textView.Text = json;
                textView.TextColor = UIColor.Black;
            }
            catch (Exception ex)
            {
                textView.Text = ex.Message;

                // Make text gray if we are presenting an error instead of data
                textView.TextColor = UIColor.DarkGray;
            }
        }

        private async Task ViewAsText()
        {
            imageView.Hidden = true;
            textView.Hidden = false;
            ClearScreen();

            try
            {
                var data = await BlobCache.LocalMachine.Get(_key);
                var text = Encoding.ASCII.GetString(data);

                textView.Text = text;
                textView.TextColor = UIColor.Black;
            }
            catch (Exception ex)
            {
                textView.Text = ex.Message;

                // Make text gray if we are presenting an error instead of data
                textView.TextColor = UIColor.DarkGray;
            }
        }

        private async Task ViewAsImage()
        {
            imageView.Hidden = false;
            textView.Hidden = true;
            ClearScreen();

            try
            {
                var bitmap = await BlobCache.LocalMachine.LoadImage(_key);
                var image = bitmap.ToNative();

                imageView.Image = image;
            }
            catch (Exception ex)
            {
                imageView.Hidden = true;
                textView.Hidden = false;

                textView.Text = ex.Message;

                // Make text gray if we are presenting an error instead of data
                textView.TextColor = UIColor.DarkGray;
            }
        }

        private void ClearScreen()
        {
            imageView.Image = null;
            textView.Text = string.Empty;
        }
    }
}