using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using Wac2015.Helpers;
using Wac2015.iOS.Controls;
using Wac2015.Models;

namespace Wac2015.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        MobileServiceClient _client;
        private IMobileServiceTable<Feedback2> _feedbackTable;
        private IMobileServiceTable<Eventfeedback> _eventFeedbackTable;
        private IMobileServiceTable<ContactUs> _contactUsTable;
        private IMobileServiceTable<NewsPublish> _newsTable;
        FeedbackManager _feedbackManager;


        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            CurrentPlatform.Init();
            _client = new MobileServiceClient(Settings.AzureUrl, Settings.AzureKey);
            _feedbackTable = _client.GetTable<Feedback2>();
            _eventFeedbackTable = _client.GetTable<Eventfeedback>();
            _contactUsTable = _client.GetTable<ContactUs>();
            _newsTable = _client.GetTable<NewsPublish>();
            _feedbackManager = new FeedbackManager(_feedbackTable, _eventFeedbackTable, _contactUsTable, _newsTable);
            App.SetFeedbackManager(_feedbackManager);

            App.NetworkMonitor = new NetworkMonitor();

            return base.FinishedLaunching(app, options);
        }
    }
}
