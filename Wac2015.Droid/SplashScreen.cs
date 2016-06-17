using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Wac2015.Helpers;
using Wac2015.Models;
using Xamarin;

namespace Wac2015.Droid
{
    [Activity(Label = "#Azure4Sure", Icon = "@drawable/azurelogo_100", MainLauncher = true, NoHistory = true, LaunchMode = LaunchMode.SingleTask,
      Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
      ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity// global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
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

            // Create your application here
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}