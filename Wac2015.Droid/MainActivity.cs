using System;
using System.Collections.Generic;
using System.Globalization;
using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Wac2015.Droid.Controls;
using Wac2015.Helpers;
using Wac2015.Models;
using Gcm;
using Xamarin;
using Xamarin.Forms;
using ProgressBar = Android.Widget.ProgressBar;

namespace Wac2015.Droid
{
    [Activity(Label = "#MAC2015", Icon = "@drawable/azurelogo_100", LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        MobileServiceClient _client;
        private MobileServiceClient client;
        private IMobileServiceTable<Feedback2> _feedbackTable;
        private IMobileServiceTable<Eventfeedback> _eventFeedbackTable;
        private IMobileServiceTable<ContactUs> _contactUsTable;
        private IMobileServiceTable<NewsPublish> _newsTable;
        FeedbackManager _feedbackManager;
        public static Context ctx;
        private ProgressBar progressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            var manager = (ConnectivityManager)GetSystemService(ConnectivityService);
            App.NetworkMonitor = new NetworkMonitor(manager);
            ctx = this;

            if (!Insights.IsInitialized)
            {
                Xamarin.Insights.Initialize("cb9dddf47d18b81b88181a4f106fcb7565048148", this);
                Insights.ForceDataTransmission = true;
                if (!string.IsNullOrEmpty(App.uuid))
                {
                    var manyInfos = new Dictionary<string, string>
                    {
                        {Xamarin.Insights.Traits.GuestIdentifier, App.uuid},
                        {"CurrentCulture", CultureInfo.CurrentCulture.Name}
                    };
                    Xamarin.Insights.Identify(App.uuid, manyInfos);
                }
            }

            if(App.NetworkMonitor.IsAvailable())
                RegisterWithGCM();
            CurrentPlatform.Init();
            _client = new MobileServiceClient(Settings.AzureUrl, Settings.AzureKey);
            _feedbackTable = _client.GetTable<Feedback2>();
            _eventFeedbackTable = _client.GetTable<Eventfeedback>();
            _contactUsTable = _client.GetTable<ContactUs>();
            _newsTable = _client.GetTable<NewsPublish>();
            _feedbackManager = new FeedbackManager(_feedbackTable, _eventFeedbackTable, _contactUsTable, _newsTable);

            App.SetFeedbackManager(_feedbackManager);
            
        }

        private void RegisterWithGCM()
        {
            // Check to ensure everything's setup right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            // Register for push notifications
            System.Diagnostics.Debug.WriteLine("Registering...");
            GcmClient.Register(this, Constants.SenderID);
        }
    }
}

