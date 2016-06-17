using Android.Net;
using Wac2015.Helpers;

namespace Wac2015.Droid.Controls
{
    public class NetworkMonitor: INetworkMonitor
    {
        private readonly ConnectivityManager _connectivityManager;

        public NetworkMonitor(ConnectivityManager connectivityManager)
        {
            _connectivityManager = connectivityManager;
        }

        public bool IsAvailable()
        {
            var networkInfo = _connectivityManager.ActiveNetworkInfo;

            return ((networkInfo != null) && networkInfo.IsConnected);
        }
    }
}