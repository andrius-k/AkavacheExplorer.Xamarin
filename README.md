# AkavacheExplorer.Xamarin

AkavacheExplorer.iOS and AkavacheExplorer.Android are packages that allow to examine the contents of your Xamarin app's Akavache cache. Explorer will not attempt to modify the content of the cache in any way. 

You have to include the package in project targeting Xamarin.iOS or Xamarin.Android and simply present explorer's UIViewController/Activity whenever you want to examine the content of Akavache local store.

### iOS Usage:

```cs
using AkavacheExplorer.iOS;
...
PresentViewController(Explorer.GetNavigationController(), true, null);
```

### Android Usage:

```cs
using AkavacheExplorer.Droid;
...
StartActivity(typeof(ExplorerActivity));
```

### Screenshots:

<img src="/images/screenshot_ios.png" width="350"> <img src="/images/screenshot_android.png" width="350">