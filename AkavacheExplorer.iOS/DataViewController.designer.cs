// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AkavacheExplorer.iOS
{
    [Register ("DataViewController")]
    partial class DataViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl segmentedControl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView textView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIToolbar toolbar { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imageView != null) {
                imageView.Dispose ();
                imageView = null;
            }

            if (segmentedControl != null) {
                segmentedControl.Dispose ();
                segmentedControl = null;
            }

            if (textView != null) {
                textView.Dispose ();
                textView = null;
            }

            if (toolbar != null) {
                toolbar.Dispose ();
                toolbar = null;
            }
        }
    }
}