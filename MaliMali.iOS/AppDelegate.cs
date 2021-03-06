﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Syncfusion.SfRating.XForms.iOS;
using UIKit;

namespace MaliMali.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();

            // Initialize SFRating
            new SfRatingRenderer();

            // Color of the selected tab icon:
            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(3, 169, 245);

            // Initialize SFCarouesl
            new Syncfusion.SfCarousel.XForms.iOS.SfCarouselRenderer();

            // Initialize ImageCircle
            ImageCircleRenderer.Init();

            // Initalize Firebase App
            Firebase.Core.App.Configure();

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
