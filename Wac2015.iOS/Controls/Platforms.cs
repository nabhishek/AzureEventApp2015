using Wac2015.Controls;
using Wac2015.iOS.Controls;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Platforms))]
namespace Wac2015.iOS.Controls
{
    public class Platforms: IPlatforms
    {
        public Platforms() { }

        public void MakeToast(string message)
        {
            //Toast.MakeText(Forms.Context, message, ToastLength.Short).Show();
        }
    }
}