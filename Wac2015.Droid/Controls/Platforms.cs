using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Wac2015.Controls;
using Wac2015.Droid.Controls;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Platforms))]
namespace Wac2015.Droid.Controls
{
    public class Platforms: IPlatforms
    {
        public Platforms() { }

        public void MakeToast(string message)
        {
            Toast.MakeText(Forms.Context, message, ToastLength.Short).Show();
        }
    }
}