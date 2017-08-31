using System;
using UIKit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Foundation;
namespace AkavacheExplorer.iOS
{
    internal class KeysTableViewSource : UITableViewSource
    {
		public delegate void TableViewEventHandler(object sender, TableViewEventArgs args);
		public event TableViewEventHandler ItemSelected;

        private const string KEY_CELL_ID = "keyCell";
        private IEnumerable<string> _keys;

        public KeysTableViewSource(IEnumerable<string> keys)
        {
            _keys = keys;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _keys.Count();
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
			UITableViewCell cell = tableView.DequeueReusableCell(KEY_CELL_ID);
            string item = _keys.ElementAt(indexPath.Row);

			if (cell == null)
			    cell = new UITableViewCell(UITableViewCellStyle.Default, KEY_CELL_ID);

			cell.TextLabel.Text = item;
            cell.TextLabel.Lines = 0;
            cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;

			return cell;
        }

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			ItemSelected?.Invoke(this, new TableViewEventArgs(_keys.ElementAt(indexPath.Row)));
		}
    }

    internal class TableViewEventArgs : EventArgs
    {
        public string Key { get; private set; }

        public TableViewEventArgs(string key)
        {
            Key = key;
        }
    }
}
