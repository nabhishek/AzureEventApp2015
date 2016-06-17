using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wac2015.Models;
using Wac2015.ViewModels;

namespace Wac2015.Helpers
{
    public class ConferenceManager : ModelBase
    {
        public static string JsonDataFilename = "sessions.json";

        public string SessionData = String.Empty;

        public static Dictionary<string, Session> Sessions = new Dictionary<string, Session>();

        public static Dictionary<string, Speaker> Speakers = new Dictionary<string, Speaker>();

        public void LoadFromString(string jsonString)
        {
//#if WINDOWS_PHONE
            try
            {
                //jsonString = jsonString.Replace("0000-04:00", ""); // HACK: excuse this timezone hack
                var sessions = JsonConvert.DeserializeObject<List<Session>>(jsonString);
                Sessions = new Dictionary<string, Session>();
                Speakers = new Dictionary<string, Speaker>();
                var obsSessions = new ObservableCollection<Session>();
                var obsSpeakers = new ObservableCollection<Speaker>();
                var sess = (from s in sessions where s.Speakers == null select s).ToList();
                
                foreach (var session in sessions)
                {
                    Sessions.Add(session.Id, session);
                    obsSessions.Add(session);
                    //Console.WriteLine("Session: " + session.Title);
                    if (session.Speakers != null)
                    {
                        foreach (var speaker in session.Speakers)
                        {
                            if (!Speakers.ContainsKey(speaker.Id))
                            {
                                speaker.Sessions.Add(session);
                                Speakers.Add(speaker.Id, speaker);
                                obsSpeakers.Add(speaker);
                            }
                            else
                            {
                                var temp = Speakers[speaker.Id];
                                temp.Sessions.Add(session);
                                Speakers[speaker.Id] = temp;
                            }
                            //Console.WriteLine("Speaker: " + speaker.Name);
                        }
                    }
                }

                SessionList = obsSessions;
                SpeakerList = Speakers.Values.ToObservableCollection();
            }
            catch (Exception ex)
            {
                
            }

//#else
            /*var jsonObject = JsonValue.Parse(jsonString);

            if (jsonObject != null)
            {
                for (var j = 0; j < jsonObject.Count; j++)
                {
                    var jsonSession = jsonObject[j];// as JsonValue;
                    var session = new Session(jsonSession);

                    Sessions.Add(session.Id, session);
                    Console.WriteLine("Session: " + session.Title);

                    var jsonSpeakers = jsonSession["speakers"];// as JsonValue;

                    for (var k = 0; k < jsonSpeakers.Count; k++)
                    {
                        var jsonSpeaker = jsonSpeakers[k]; // as JsonValue;
                        var speaker = new Speaker(jsonSpeaker);

                        if (!Speakers.ContainsKey(speaker.Id))
                        {
                            Speakers.Add(speaker.Id, speaker);
                        }
                        else
                        {
                            speaker = Speakers[speaker.Id];
                        }
                        speaker.Sessions.Add(session);
                        session.Speakers.Add(speaker);

                        Console.WriteLine("Speaker: " + speaker.Name);
                    }
                }
            }*/
//#endif
            //Console.WriteLine("done");
        }

        public ObservableCollection<Session> SessionList { get; set; }
        public ObservableCollection<Speaker> SpeakerList { get; set; }
        //private ObservableCollection<TwitterStatusItemModel> TwitterFeed;

        public event LoadEventHandler DataLoaded;

        private bool _sessionsAreNotOnlineYet;

        public bool SessionsAreNotOnlineYet
        {
            get { return _sessionsAreNotOnlineYet; }
            set
            {
                _sessionsAreNotOnlineYet = value;
                NotifyPropertyChanged("SessionsAreNotOnlineYet");
            }
        }


        public ObservableCollection<Session> GetSessions()
        {
            return SessionList;
        }

        async public Task<bool> GetData(string conferenceUrl, bool isJson)
        {
            SessionList = new ObservableCollection<Session>();
            SpeakerList = new ObservableCollection<Speaker>();
            DateTime sessionLastDownload = DateTime.MinValue;



            // Get the data from Isolated storage if it is there
            //if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionData"))
            //{
            //    var converted = (IsolatedStorageSettings.ApplicationSettings["SessionData"] as IEnumerable<Session>);

            //    SessionList = converted.ToObservableCollection(SessionList);
            //    var loadedEventArgs = new LoadEventArgs { IsLoaded = true, Message = string.Empty };
            //    OnDataLoaded(loadedEventArgs);
            //}
            // Get the data from Isolated storage if it is there
            //if (IsolatedStorageSettings.ApplicationSettings.Contains("SpeakerData"))
            //{
            //    var converted = (IsolatedStorageSettings.ApplicationSettings["SpeakerData"] as IEnumerable<Speaker>);

            //    SpeakerList = converted.ToObservableCollection(SpeakerList);
            //    var loadedEventArgs = new LoadEventArgs { IsLoaded = true, Message = string.Empty };
            //    OnDataLoaded(loadedEventArgs);
            //}



            // Get the last time the data was downloaded.
            //if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionLastDownload"))
            //{
            //    sessionLastDownload = (DateTime)IsolatedStorageSettings.ApplicationSettings["SessionLastDownload"];
            //}

            // Check if we need to download the latest data, or if we can just use the isolated storage data
            // Cache the data for 2 hours
            //if (!isJson && !String.IsNullOrEmpty(conferenceUrl) && conferenceUrl.StartsWith("http") && 
            //    (sessionLastDownload.AddHours(2) < DateTime.Now || 
            //    !IsolatedStorageSettings.ApplicationSettings.Contains("SessionData")))
            if (!isJson && !String.IsNullOrEmpty(conferenceUrl) && conferenceUrl.StartsWith("http") &&
                (sessionLastDownload.AddHours(2) < DateTime.Now))
            {
                var responseString = string.Empty;
                // Download the session data
                var httpClient = new HttpClient(new HttpClientHandler()); 
                var response = await httpClient.GetAsync(conferenceUrl);
                response.EnsureSuccessStatusCode();
                responseString = await response.Content.ReadAsStringAsync();
                LoadFromString(responseString);

                //conferenceWebClient.DownloadStringCompleted += conferenceWebClient_DownloadStringCompleted;
                //conferenceWebClient.DownloadStringAsync(new Uri(Settings.ConferenceDataUri));

                // Download speaker data
                //var speakerWebClient = new SharpGIS.GZipWebClient();
                //speakerWebClient.DownloadStringCompleted += speakerWebClient_DownloadStringCompleted;
                //speakerWebClient.DownloadStringAsync(new Uri(Settings.SpeakerServiceUri));
            }
            else if (!String.IsNullOrEmpty(conferenceUrl) && isJson)
            {
                LoadFromString(conferenceUrl);
            }
            return true;
        }


        protected virtual void OnDataLoaded(LoadEventArgs e)
        {
            if (DataLoaded != null)
            {
                DataLoaded(this, e);
            }
        }

        public ObservableCollection<Speaker> GetSpeakers()
        {
            return SpeakerList;
        }

        public async Task<int> GetLatestVersion()
        {
            try
            {
                var responseString = await Utils.GetDataFromUrl(Settings.ConferenceDataVersionUri);
                int version;
                if (!int.TryParse(responseString.Trim(), out version))
                    version = -1;
                return version;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<string> GetLatestData()
        {
            var responseString = await Utils.GetDataFromUrl(Settings.ConferenceDataUri);
            return responseString;
        }

//#if !WINDOWS_PHONE
//        public static void LoadFromFile()
//        {
//            string xmlPath = JsonDataFilename;
//            var basedir = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

//            if (File.Exists(Path.Combine(basedir, JsonDataFilename)))
//            {	// load a downloaded copy
//                xmlPath = Path.Combine(basedir, JsonDataFilename);
//            }

//            var jsonString = File.ReadAllText(xmlPath);

//            LoadFromString(jsonString);
//        }
//#endif
    }
}
