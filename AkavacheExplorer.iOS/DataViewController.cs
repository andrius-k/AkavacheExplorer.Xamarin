using System;

using UIKit;
using Akavache;
using System.Reactive.Linq;
using Newtonsoft.Json;

namespace AkavacheExplorer.iOS
{
    internal partial class DataViewController : UIViewController
    {
        private string _key;

        public DataViewController(string key) : base("DataViewController", null)
        {
            _key = key;
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = _key;

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
    }
}

