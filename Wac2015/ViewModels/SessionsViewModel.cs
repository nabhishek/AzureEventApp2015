using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Data;
using Xamarin.Forms;

namespace Wac2015.ViewModels
{
    public class SessionsViewModel : BaseViewModel
    {
        private readonly IDataStore _dataStore;
        private readonly List<TrackState> _tracks;

        private bool _noSessionsNow;
        private TrackState _selectedTrack;
        private Command<Page> _filterCommand;
        private Command _loadSessionsCommand;
        private Command _updateNowSessionsCommand;
        private ObservableCollection<SessionState> _nowSessions;
        private ObservableCollection<SessionState> _sessions;
        private ObservableCollection<Grouping<SessionDateInfo, SessionState>> _sessionsGroupedByDate;
        private readonly ILogger _logger;

        public SessionsViewModel()
        {
            Title = "Sessions";

            _logger = AppSettings.Log.CreateLogger(Title);

            _dataStore = App.DataStore;
            _tracks = new List<TrackState>();
            _selectedTrack = null;
            _sessions = new ObservableCollection<SessionState>();
            _sessionsGroupedByDate = new ObservableCollection<Grouping<SessionDateInfo, SessionState>>();

            DependencyService.Get<IMessageBus>().OnEventRefreshed(this, state =>
            {
                Sessions.Clear();
                foreach (SessionState session in state.Sessions)
                {
                    Sessions.Add(session);
                }
                Tracks.Clear();
                foreach (var track in state.Tracks)
                {
                    Tracks.Add(track);
                }
                FilterSessions(_selectedTrack);
            });
        }

        public List<TrackState> Tracks
        {
            get { return _tracks; }
        }

        public ObservableCollection<SessionState> Sessions
        {
            get { return _sessions; }
            private set { SetProperty(ref _sessions, value); }
        }

        public ObservableCollection<Grouping<SessionDateInfo, SessionState>> SessionsGroupedByDate
        {
            get { return _sessionsGroupedByDate; }
            private set { SetProperty(ref _sessionsGroupedByDate, value); }
        }

        public ObservableCollection<SessionState> NowSessions
        {
            get { return _nowSessions; }
            private set { SetProperty(ref _nowSessions, value); }
        }

        public Command FilterCommand
        {
            get { return _filterCommand ?? (_filterCommand = new Command<Page>(async page => await ExecuteFilterCommand(page))); }
        }

        public Command UpdateNowSessionsCommand
        {
            get
            {
                return _updateNowSessionsCommand ??
                       (_updateNowSessionsCommand = new Command(ExecuteUpdateNowSessionsCommand));
            }
        }


        public bool NoSessionsNow
        {
            get { return _noSessionsNow; }
            set { SetProperty(ref _noSessionsNow, value); }
        }

        public void FilterSessions(TrackState track)
        {
            SessionsGroupedByDate.Clear();
            foreach (var grouping in Sessions.SortAndFilter(track))
                SessionsGroupedByDate.Add(grouping);
        }

        private async Task ExecuteFilterCommand(Page page)
        {
            if (IsBusy)
                return;

            var trackNames = new List<string>(Tracks.Select(t => t.Title));

            if (trackNames.Count == 0)
                return;
            trackNames.Insert(0, "Show All");

            if (Device.OS == TargetPlatform.Android)
            {
                DependencyService.Get<IDialogs>().DisplayActionSheet("Select Tracks", "Cancel",
                    trackNames.ToArray(),
                    which =>
                    {
                        _selectedTrack = which == 0 ? null : Tracks[which - 1];
                        FilterSessions(_selectedTrack);
                    }); //if show all pass null
            }
            else
            {
                string action = await page.DisplayActionSheet("Select Track", "Cancel", null, trackNames.ToArray());

                if (action == "Cancel")
                    return;

                _selectedTrack = Tracks.FirstOrDefault(t => t.Title == action);
                FilterSessions(_selectedTrack);
            }
        }

        public Command LoadSessionsCommand
        {
            get
            {
                return _loadSessionsCommand ?? (_loadSessionsCommand = new Command(async () => await LoadSessions()));
            }
        }

        public async Task LoadSessions()
        {
            if (IsBusy)
                return;

            try
            {
                _logger.Info("Load Start");
                IsBusy = true;

                EventState = await _dataStore.RefreshEvent();

                if (EventState == null)
                {
                    OnError(new AuthenticationErrorEventArgs("Whoops! Something went weird. Please double check your internet connection and try again."));
                    return;
                }

                var sessions = EventState.Sessions;

                ICollection<TrackState> tracks = EventState.Tracks;
                if (tracks.Count == 0)
                    tracks = new List<TrackState>(await _dataStore.GetTracks());

                foreach (TrackState track in tracks)
                    _tracks.Add(track);

                Sessions = new ObservableCollection<SessionState>(sessions);

                FilterSessions(null);

                IsInitialized = true;
                NowSessions = new ObservableCollection<SessionState>();
                ExecuteUpdateNowSessionsCommand();
                _logger.Info("Load Finish");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ExecuteUpdateNowSessionsCommand()
        {
            if (!IsInitialized)
                return;

            //query all shows that are on currenly or started in the last 30 minutes
            DateTime date = DateTime.UtcNow;

            //var date = new DateTime (2014, 10, 6, 10, 0, 0).ToUniversalTime();//testing
            IOrderedEnumerable<SessionState> query = from session in Sessions
                                                     where date <= session.EndDateUtc && date >= session.StartDateUtc.AddMinutes(-30)
                                                     orderby session.StartDate
                                                     select session;

            NowSessions.Clear();
            foreach (SessionState item in query)
                NowSessions.Add(item);

            NoSessionsNow = NowSessions.Count == 0;
        }
    }
}
