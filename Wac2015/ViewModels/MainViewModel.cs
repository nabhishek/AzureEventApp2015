using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using Microsoft.WindowsAzure.MobileServices;
using Wac2015.Helpers;
using Wac2015.Models;
using Xamarin;

namespace Wac2015.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            this.IsDataLoaded = false;
            this.LoadingNews = true;
            this.NoNews = false;
            this.Items = new ObservableCollection<ItemViewModel>();
            DayOneSessions = new ObservableCollection<Session>();
            DayTwoSessions = new ObservableCollection<Session>();
            SessionsGroup1 = new ObservableCollection<GroupedSessions>();
            SessionsGroup2 = new ObservableCollection<GroupedSessions>();
            this.TwitterFeeds = new ObservableCollection<Tweet>();
            InitializeSessionMenu();

            //if (!Insights.IsInitialized)
            //{
            //    Xamarin.Insights.Initialize("cb9dddf47d18b81b88181a4f106fcb7565048148");
            //    Insights.ForceDataTransmission = true;
            //    if (!string.IsNullOrEmpty(App.uuid))
            //    {
            //        var manyInfos = new Dictionary<string, string>
            //        {
            //            {Xamarin.Insights.Traits.GuestIdentifier, App.uuid},
            //            {"CurrentCulture", CultureInfo.CurrentCulture.Name}
            //        };
            //        Xamarin.Insights.Identify(App.uuid, manyInfos);
            //    }
            //}

            LoadData();
            //LoadTwitterFeeds();
        }


        private ObservableCollection<Tweet> _twitterFeeds;
        public ObservableCollection<Tweet> TwitterFeeds
        {
            get { return _twitterFeeds; }
            set
            {
                _twitterFeeds = value;
                NotifyPropertyChanged("HomeTwitterFeeds");
                NotifyPropertyChanged("TwitterFeeds");
                NotifyPropertyChanged("TwitterViewMore");
            }
        }
        public ObservableCollection<Tweet> HomeTwitterFeeds
        {
            get
            {
                if (TwitterFeeds != null)
                    return TwitterFeeds.Take(5).ToObservableCollection();
                return new ObservableCollection<Tweet>();
            }
        }

        /*public Visibility DisplayNoNews
        {
            get
            {

                if (NoNews == true)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }
        public Visibility DisplayLaodingNews
        {
            get
            {
                if (LoadingNews == true)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }
        
        public Visibility TwitterViewMore
        {
            get { return TwitterFeeds.Count > 5 ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility SavedSessionMessage
        {
            get
            {
                return (SavedSessions == null || SavedSessions.Count == 0) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility UuidBasedVisibility
        {
            get { return String.IsNullOrEmpty(Uuid) ? Visibility.Collapsed : Visibility.Visible; }
        }
        public Visibility NegateUuidBasedVisibility
        {
            get { return !String.IsNullOrEmpty(Uuid) ? Visibility.Collapsed : Visibility.Visible; }
        }*/

        private IEnumerable<Tweet> TempTweets
        {
            set
            {
                if (value != null && value.Any())
                    TwitterFeeds = value.ToObservableCollection();
            }
        }

        private ObservableCollection<NewsPublish> _newsFeeds;
        public ObservableCollection<NewsPublish> NewsFeeds
        {
            get { return _newsFeeds; }
            set
            {
                _newsFeeds = value; NotifyPropertyChanged("NewsFeeds");
                NotifyPropertyChanged("DisplayNoNews");
                NotifyPropertyChanged("DisplayLaodingNews");
            }
        }

        private ObservableCollection<SessionKeyGroup<Session>> _dayOneCollection;
        public ObservableCollection<SessionKeyGroup<Session>> DayOneCollection
        {

            get { return _dayOneCollection; }
            set { _dayOneCollection = value; NotifyPropertyChanged("DayOneCollection"); }
        }
        private ObservableCollection<SessionKeyGroup<Session>> _dayTwoCollection;
        public ObservableCollection<SessionKeyGroup<Session>> DayTwoCollection
        {

            get { return _dayTwoCollection; }
            set { _dayTwoCollection = value; NotifyPropertyChanged("DayTwoCollection"); }
        }
        private ObservableCollection<SessionKeyGroup<Session>> _timeBasedCollection;
        public ObservableCollection<SessionKeyGroup<Session>> TimeBasedCollection
        {

            get { return _timeBasedCollection; }
            set { _timeBasedCollection = value; NotifyPropertyChanged("TimeBasedCollection"); }
        }
        private ObservableCollection<SessionKeyGroup<Session>> _timeBasedMyAgendaCollection;
        public ObservableCollection<SessionKeyGroup<Session>> TimeBasedMyAgendaCollection
        {

            get { return _timeBasedMyAgendaCollection; }
            set { _timeBasedMyAgendaCollection = value; NotifyPropertyChanged("TimeBasedMyAgendaCollection"); }
        }

        private ObservableCollection<SessionKeyGroup<Session>> _trackBasedCollection;
        public ObservableCollection<SessionKeyGroup<Session>> TrackBasedCollection
        {

            get { return _trackBasedCollection; }
            set { _trackBasedCollection = value; NotifyPropertyChanged("TrackBasedCollection"); }
        }

        private ObservableCollection<Session> _dayOneSessions;
        public ObservableCollection<Session> DayOneSessions
        {
            get { return _dayOneSessions; }
            set { _dayOneSessions = value; NotifyPropertyChanged("DayOneSessions"); }
        }

        private ObservableCollection<Session> _dayTwoSessions;
        public ObservableCollection<Session> DayTwoSessions
        {
            get { return _dayTwoSessions; }
            set { _dayTwoSessions = value; NotifyPropertyChanged("DayTwoSessions"); }
        }
        public ObservableCollection<MenuModel> SessionMenu { get; set; }
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private ObservableCollection<Session> _sessions;
        public ObservableCollection<Session> Sessions
        {
            get
            {
                return _sessions;
            }
            set
            {
                if (value != _sessions)
                {
                    _sessions = value;
                    NotifyPropertyChanged("Sessions");
                }
            }
        }



        private ObservableCollection<Speaker> _speakers;
        public ObservableCollection<Speaker> Speakers
        {
            get
            {
                return _speakers;
            }
            set
            {
                if (value != _speakers)
                {
                    _speakers = value;
                    NotifyPropertyChanged("Speakers");
                }
            }
        }

        private ObservableCollection<Session> _savedSessions;
        public ObservableCollection<Session> SavedSessions
        {
            get
            {
                return _savedSessions;
            }
            set
            {
                if (value != _savedSessions)
                {
                    _savedSessions = value;
                    TimeBasedMyAgendaCollection = SessionKeyGroup<Session>.CreateDateGroups(SavedSessions, CultureInfo.CurrentCulture,
                        (Session s) => s.Begins, true);
                    NotifyPropertyChanged("SavedSessions"); NotifyPropertyChanged("SavedSessionMessage");
                }
            }
        }

        private ObservableCollection<GroupedSessions> _sessionsGroup1;

        public ObservableCollection<GroupedSessions> SessionsGroup1
        {
            get
            {
                return _sessionsGroup1;
            }
            set { _sessionsGroup1 = value; NotifyPropertyChanged("SessionsGroup1"); }
        }

        private ObservableCollection<GroupedSessions> _sessionsGroup2;

        public ObservableCollection<GroupedSessions> SessionsGroup2
        {
            get { return _sessionsGroup2; }
            set { _sessionsGroup2 = value; NotifyPropertyChanged("SessionsGroup2"); }
        }

        public IEnumerable<Group<Session>> SessionsByTimeSlot { get; private set; }
        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool NoNews
        {
            get;
            private set;
        }
        public bool LoadingNews
        {
            get;
            private set;
        }
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        private string _uuid;

        public string Uuid
        {
            get { return App.uuid; }
            set { _uuid = value; NotifyPropertyChanged("Uuid"); NotifyPropertyChanged("UuidBasedVisibility"); }
        }



        public async Task LoadTwitterFeeds()
        {
            //ObservableCollection<Tweet> result = null;

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    var twitterManager = new TwitterManager();
                    var account = twitterManager.Service;
                    var auth = new ApplicationOnlyAuthorizer()
                    {
                        CredentialStore = new InMemoryCredentialStore
                        {
                            ConsumerKey = account.ConsumerKey,
                            ConsumerSecret = account.ConsumerSecret,
                        },
                    };
                    await auth.AuthorizeAsync();

                    var twitterContext = new TwitterContext(auth);
                    Search queryResponse = null;
                    var hashtag = "\"#techedindia\"";
                    queryResponse = await
                            (from tweet in twitterContext.Search
                             where tweet.Type == SearchType.Search &&
                                   tweet.Query == hashtag &&
                                   tweet.Count == 20
                             select tweet).SingleOrDefaultAsync();


                    if (queryResponse != null && queryResponse.Statuses != null)
                    {
                        var queryTweets = queryResponse.Statuses;
                        var tweets = (from s in queryTweets
                                      select new Tweet
                                      {
                                          Text = s.Text,
                                          CreatedAt = s.CreatedAt,
                                          ScreenName = s.User.ScreenNameResponse,
                                          ProfileImage = s.User.ProfileImageUrl
                                      }).ToList();
                        if (tweets != null)
                            TwitterFeeds = tweets.ToObservableCollection();
                    }
                }
                catch (Exception ex)
                {
                    Insights.Report(ex);
                }
                //await Task.Factory.StartNew(() =>
                //{
                //    var done = false;
                //    try
                //    {
                //        var twitterManager = new TwitterManager();
                //        twitterManager.Service.Search(new SearchOptions() { Q = "#techedindia", Count = 20 },
                //            (ts, rep) =>
                //            {
                //                if (rep.StatusCode == HttpStatusCode.OK)
                //                {
                //                    result = ts.Statuses.ToObservableCollection();
                //                }
                //                done = true;
                //            });
                //        while (!done)
                //        {
                //        }
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //});
                //if (result != null)
                //    TwitterFeeds = result;

            }
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async Task LoadData()
        {
            var manager = new ConferenceManager();
            //bool networkAvailable = false;// App.NetworkMonitor.IsAvailable();

            int nextVersion = -1;


            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nextVersion = await manager.GetLatestVersion();

                if (nextVersion != -1 && (App.CurrentSessionVersion < nextVersion && App.DefaultSessionVersion < nextVersion))
                {
                    var newData = await manager.GetLatestData();
                    var res = await manager.GetData(newData, true);
                    if (res && manager.GetSessions().Any() && manager.GetSpeakers().Any())
                    {
                        App.CurrentSessionData = newData;
                        AppStorageHelper.SaveCurrentSessionData(App.CurrentSessionData);
                        AppStorageHelper.SaveCurrentVersionNumber(nextVersion.ToString());

                        Insights.Track("New Session Data", new Dictionary<string, string>{
                            {"New Version", nextVersion.ToString()},
                            {"Old Version", App.CurrentSessionVersion.ToString()},
                            {"Sessions Count", manager.GetSessions().Count.ToString()},
                            {"Speakers Count", manager.GetSpeakers().Count.ToString()}
                        });
                        App.CurrentSessionVersion = nextVersion;
                    }

                }
            }


            //nextVersion = -1;


            if (String.IsNullOrEmpty(App.CurrentSessionData))
            {
                var sessionData = Utils.ReadFile(Wac2015.Helpers.Settings.StaticData);
                App.CurrentSessionData = sessionData;
                var res = await manager.GetData(sessionData, true);
            }
            else
            {
                await manager.GetData(App.CurrentSessionData, true);
            }
            //var res = await manager.GetData(Settings.ConferenceDataUri);
            //Service.GetData();

            Sessions = manager.GetSessions();

            Speakers = manager.GetSpeakers();
            Speakers = Speakers.OrderBy(s => s.Name).ToObservableCollection();
            if (!String.IsNullOrEmpty(App.SessionId))
                App.CurrentSession =
                    (from s in Sessions
                     where s.Id.ToLower().Equals(App.SessionId.ToLower())
                     select s).FirstOrDefault();

            if (!String.IsNullOrEmpty(App.SpeakerId))
                App.CurrentSpeaker =
                    (from s in Speakers
                     where s.Id.ToLower().Equals(App.SpeakerId.ToLower())
                     select s).FirstOrDefault();

            if (App.SavedSessionIds == null)
                App.SavedSessionIds = new List<string>();
            else if (App.SavedSessionIds != null && App.SavedSessionIds.Count > 0)
            {
                App.SavedSessions = new ObservableCollection<Session>();
                foreach (var sessionId in App.SavedSessionIds)
                {
                    string id = sessionId;
                    var session = Sessions.FirstOrDefault(s => s.Id == id);
                    if (session != null)
                        App.SavedSessions.Add(session);
                }
            }
            //if (Service.SessionsAreNotOnlineYet)
            //    DiscoveredThatSessionsAreNotOnlineYet = true;

            //needs to get it from iso if available...
            App.Sessions = Sessions;
            App.Speakers = Speakers;

            if (App.SavedSessions == null)
                App.SavedSessions = new ObservableCollection<Session>();

            //SavedSessions = App.SavedSessions;
            Sessions = Sessions.OrderBy(s => s.Begins).ToObservableCollection();
            SavedSessions = (from s in Sessions
                             join sid in App.SavedSessionIds
                                 on s.Id equals sid
                             select s).ToObservableCollection();

            var date = "2015-03-18T09:30:00.0000000+05:30";
            var dt = DateTime.Parse(date);
            DayOneSessions = (from s in Sessions
                              where s.Begins.Date == dt.Date
                              select s).ToObservableCollection();


            DayOneCollection = SessionKeyGroup<Session>.CreateDateGroups(DayOneSessions, CultureInfo.CurrentCulture,
                (Session s) => s.Begins, true);

            dt = dt.AddDays(1);
            DayTwoSessions = (from s in Sessions
                              where s.Begins.Date == dt.Date
                              select s).ToObservableCollection();

            DayTwoCollection = SessionKeyGroup<Session>.CreateDateGroups(DayTwoSessions, CultureInfo.CurrentCulture,
                (Session s) => s.Begins, true);

            TimeBasedCollection = SessionKeyGroup<Session>.CreateDateGroups(Sessions, CultureInfo.CurrentCulture,
                (Session s) => s.Begins, true);

            TrackBasedCollection = SessionKeyGroup<Session>.CreateTrackGroups(Sessions, CultureInfo.CurrentCulture,
                (Session s) => s.TrackName, false);

            TimeBasedMyAgendaCollection = SessionKeyGroup<Session>.CreateDateGroups(SavedSessions, CultureInfo.CurrentCulture,
                (Session s) => s.Begins, true);

            this.IsDataLoaded = true;
            //await LoadTwitterFeeds();
            //if (result != null)
            //    TwitterFeeds = result;
            await Task.Factory.StartNew(async () => { await LoadNews(); });
        }

        public void UpdateSavedSessions()
        {
            SavedSessions = (from s in Sessions
                             join sid in App.SavedSessionIds
                                 on s.Id equals sid
                             select s).ToObservableCollection();
            TimeBasedMyAgendaCollection = SessionKeyGroup<Session>.CreateDateGroups(SavedSessions,
                CultureInfo.CurrentCulture,
                (Session s) => s.Begins, true);
            //}
        }

        public async Task LoadNews()
        {
            try
            {
                NewsFeeds = new ObservableCollection<NewsPublish>();
                //IMobileServiceTable<AzureNews> newsTable = App.MobileService.GetTable<AzureNews>();
                var nf = await App.feedbackManager.GetTaskAzync(); //newsTable.ToCollectionAsync();
                NewsFeeds = nf.OrderByDescending(e => e.CreatedDt).ToObservableCollection();
            }
            catch (Exception ex)
            {
                NotifyPropertyChanged("DisplayNoNews");
                Insights.Report(ex);
            }
        }
        private async void InitializeSessionMenu()
        {
            SessionMenu = new ObservableCollection<MenuModel>();
            SessionMenu.Add(new MenuModel() { Name = "All by Day", Description = "Show all sessions by day", MenuType = MenuTypes.SessionAllByDay, IconPath = "../Assets/Images/AllByDay.png" });
            SessionMenu.Add(new MenuModel() { Name = "My Agenda", Description = "View my agenda", MenuType = MenuTypes.SessionMyAgenda, IconPath = "../Assets/Images/MyAgenda.png" });
            SessionMenu.Add(new MenuModel() { Name = "By Speaker", Description = "Filter by speaker", MenuType = MenuTypes.SessionBySpeaker, IconPath = "../Assets/Images/BySpeaker.png" });
            SessionMenu.Add(new MenuModel() { Name = "By Time", Description = "Filter by time slot", MenuType = MenuTypes.SessionByTime, IconPath = "../Assets/Images/ByTime.png" });
            SessionMenu.Add(new MenuModel() { Name = "By Track", Description = "Filter by track", MenuType = MenuTypes.SessionByTrack, IconPath = "../Assets/Images/ByTrack.png" });

            try
            {
                NewsFeeds = new ObservableCollection<NewsPublish>();
                //IMobileServiceTable<TechEdNews> newsTable = App.MobileService.GetTable<TechEdNews>();
                //var nf = await newsTable.ToCollectionAsync();
                //NewsFeeds = nf.OrderByDescending(e => e.CreatedDt).ToObservableCollection();
                LoadingNews = false;
                NotifyPropertyChanged("DisplayLaodingNews");
            }
            catch (Exception ex)
            {
                LoadingNews = false;
                NotifyPropertyChanged("DisplayLaodingNews");
                NoNews = true;
                NotifyPropertyChanged("DisplayNoNews");
                //MessageBox.Show("Error loading news");
            }
        }
    }
}
