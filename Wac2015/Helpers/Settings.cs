using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wac2015.Helpers
{
    public class Settings
    {
        public const string SessionServiceUri = @"http://channel9.msdn.com/events/build/2012/sessions";
        //public const string SessionServiceUri = "https://eup84q.blu.livefilestore.com/y1piNwHiDJPW_mojkUSUKd6nfTndoiAT_h6d10zApUKusCfdgEEYUzWmjW6ABJNzX3Tl40cas3I8REzKw6UbzBsbA/ete2011.json?download&psid=1";
        public const string SpeakerServiceUri = @"http://channel9.msdn.com/events/build/2012/speakers?json=true";
        public const string TwitterServiceUri = "http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=";
        
        public const string ConferenceDataUri = @"https://www.microsoftazureconference.in/assets/SessionsDetails.ashx?get=sessions";
        public const string ConferenceDataVersionUri = @"https://www.microsoftazureconference.in/assets/SessionsDetails.ashx?get=version";

        public const string UserValidationUri = @"http://mstechedapp.cloudapp.net/api/userauthentication?RegistrationNumber={0}";
        public const string StaticData = @"Wac2015.SessionData.AzureSessions.json";
        //public const string StaticData = @"Wac2015.SessionData.SessionsData.json";
        //public const string StaticData = @"Wac2015.SessionData.Sessions.json";
        public const string ApplicationName = "Build 2012";
        public const string Subject = "please provide feedback";
        public const string DefaulImage = "/Images/defaultimage.png";
        public const string AzureUrl = "https://azureconf2015-ms.azure-mobile.net/";
        public const string AzureKey = "VjXUsqtPOcknRNtFuyGSOGlBOeHUvj46";
    }
}
