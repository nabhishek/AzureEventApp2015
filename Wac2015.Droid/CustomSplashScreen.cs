using System;
using System.Collections.Generic;
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

namespace Wac2015.Droid
{
    [Activity(Label = "#Azure4Sure", Icon = "@drawable/icon", MainLauncher = false, NoHistory = true, LaunchMode = LaunchMode.SingleTask,
      Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
      ScreenOrientation = ScreenOrientation.Portrait)]
    public class CustomSplashScreen : Activity// global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            App.IsNewsPage = true;
            
            // Create your application here
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}