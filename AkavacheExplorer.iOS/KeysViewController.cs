using System;
using UIKit;
using Akavache;
using System.Reactive.Linq;
using Foundation;

namespace AkavacheExplorer.iOS
{
    internal partial class KeysViewController : UIViewController
    {
        private UIBarButtonItem _doneButton;
        private KeysTableViewSource _source;

        public KeysViewController (IntPtr handle) : base (handle)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = NSBundle.MainBundle.LocalizedString("explorer_title_local_machine", "");

            try
            {
                var keys = await BlobCache.LocalMachine.GetAllKeys();
                _source = new KeysTableViewSource(keys);
            }
            catch (Exception ex)
            {
                _source = new KeysTableViewSource(new string[0]);
                Console.WriteLine($"Exception retrieving keys: {ex.Message}");
            }

            _source.ItemSelected += Source_ItemSelected;
            tableView.Source = _source;
            tableView.ReloadData();

            _doneButton = new UIBarButtonItem(NSBundle.MainBundle.LocalizedString("explorer_title_done", ""), 
                                              UIBarButtonItemStyle.Done, null);
            _doneButton.Clicked += DoneButton_Clicked;
            NavigationItem.RightBarButtonItem = _doneButton;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            tableView.DeselectRow(tableView.IndexPathForSelectedRow, true);
        }

        private void Source_ItemSelected(object sender, TableViewEventArgs args)
        {
            var dataController = (DataViewController)Helpers.Storyboard
                                                            .InstantiateViewController("DataViewController");
            dataController.SetKey(args.Key);
            NavigationController.PushViewController(dataController, true);
        }

        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();

            _source.ItemSelected -= Source_ItemSelected;
            _doneButton.Clicked -= DoneButton_Clicked;
        }
    }
}