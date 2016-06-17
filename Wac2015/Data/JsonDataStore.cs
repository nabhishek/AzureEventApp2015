using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinEvolve.Mobile.Data;

namespace Wac2015.Data
{
    public class JsonDataStore : IDataStore
    {
        private readonly INetworkMonitor _networkMonitor;
        private readonly ILogger _logger;
        private readonly IDataCache _cache;
        private readonly IEventDayService _service;
        private readonly IMessageBus _messageBus;

        public JsonDataStore(IDataCache cache, ILogger logger, INetworkMonitor networkMonitor, IEventDayService service = null)
        {
            _networkMonitor = networkMonitor;
            _logger = logger.CreateLogger("DataStore");
            _cache = new MemoryDataCache(cache, logger); 
            _service = service ?? new EventDayService();

            try
            {
                _messageBus = DependencyService.Get<IMessageBus>();
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        public Guid EventId { get { return _service.EventId; } }

        public Task<EventState> GetEventAsync()
        {
            return GetEventAsync(_service.EventId);
        }

        public async Task<EventState> GetEventAsync(Guid id)
        {
            EventState state = await _cache.TryGetAsync<EventState>(id);

            if (state != null)
                return state;

            if(_networkMonitor.IsAvailable())
            {
				try{
	                state = await _service.GetEntireEvent(skipVersionCheck:true);
	                return await _cache.PutAsync(id, state);
				}catch(Exception ex){
					_logger.Error (ex);
				}
            }

            return null;
        }
        public async Task RefreshAsync(RefreshInfo info)
        {
		
            switch (info.RefreshType)
            {
                case RefreshType.Event:
                case RefreshType.Session:
                    _messageBus.StartEventRefresh();
                    EventState state = await RefreshEvent();
                    if (_messageBus != null)
                        _messageBus.RefreshEvent(state);
                    break;
                case RefreshType.Ticket:
                    Guid ticketId = info.Id;
                    TicketState ticket = await _service.GetTicket(ticketId);
                    if (ticket != null)
                    {
                        await _cache.PutAsync(ticketId, ticket);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public async Task<EventState> RefreshEvent()
        {
            if(_networkMonitor.IsAvailable())
            {
				try{
	                EventState state = await _service.GetEntireEvent();
	                if (state != null)
	                {
	                    return await _cache.PutAsync(EventId, state);
	                }
				}catch(Exception ex){
					_logger.Error (ex);
				}
            }

            return await GetEventAsync();
        }

        public async Task<TicketState> ValidateAuthCodeAsync(string code)
        {
            if (_networkMonitor.IsAvailable())
            {
				try{
	                TicketState ticket = await _service.ValidateAuthCode(code);

	                if (ticket == null)
	                    return null;

	                _logger.Info("Valid auth code: {0}, user: {1:n}", code, ticket.Id);

	                AppSettings.UserId = ticket.Id;
	                await _cache.PutAsync(ticket.Id, ticket);
	                return ticket;
				}catch(Exception ex) {
					_logger.Error (ex);
				}
            }

            return null;
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var userId = AppSettings.UserId;

            _logger.Info("User: {0:n}", userId);

            if (userId == Guid.Empty)
                return false;
            var ticket = await _cache.TryGetAsync<TicketState>(userId);
            return ticket != null;
        }


        public async Task<SessionState[]> GetFavoriteSessionsAsync()
        {
            Guid userId = AppSettings.UserId;
            if (userId == Guid.Empty) 
                return new SessionState[0];
            
            var favoriteSessions = await _cache.TryGetAsync<FavoriteSessions>(userId);
            if (favoriteSessions == null)
                return new SessionState[0];

            var eventState = await GetEventAsync();

            return eventState.Sessions.Where(x => favoriteSessions.Sessions.Contains(x.Id)).ToArray();
        }

        public async Task<SessionState[]> AddFavoriteSessionAsync(Guid sessionId)
        {
            var userId = AppSettings.UserId;

            if (userId == Guid.Empty)
                return new SessionState[0];

            var favoriteSessions = (await _cache.TryGetAsync<FavoriteSessions>(userId)) ?? new FavoriteSessions();
            var sessionIds = favoriteSessions.Sessions;
            if (_networkMonitor.IsAvailable())
            {
				try{
	                SessionState[] states = await _service.FavoriteSession(sessionId, userId);
	                sessionIds = new HashSet<Guid>(states.Select(x => x.Id));
				}catch(Exception ex){
					_logger.Error (ex);
				}
            }
            else
            {
                sessionIds.Add(sessionId);
            }
            await _cache.PutAsync(userId, new FavoriteSessions(sessionIds));
            var eventState = await GetEventAsync();

            return eventState.Sessions.Where(x => sessionIds.Contains(x.Id)).ToArray();
        }

        public async Task<SessionState[]> RemoveFavoriteSessionAsync(Guid sessionId)
        {
            Guid userId = AppSettings.UserId;
            if (userId == Guid.Empty) return new SessionState[0];
            await _cache.RemoveAsync<FavoriteSessions>(userId);

            var favoriteSessions = (await _cache.TryGetAsync<FavoriteSessions>(userId)) ?? new FavoriteSessions();
            var sessions = favoriteSessions.Sessions;
            if (_networkMonitor.IsAvailable())
            {
				try{
	                SessionState[] states = await _service.RemoveFavoriteSession(sessionId, userId);
	                sessions = new HashSet<Guid>(states.Select(x => x.Id));
				}catch(Exception ex){
					_logger.Error (ex);
				}
            }
            else
            {
                sessions.Remove(sessionId);
            }

            await _cache.PutAsync(userId, new FavoriteSessions(sessions));
            var eventState = await GetEventAsync();

            return eventState.Sessions.Where(x => sessions.Contains(x.Id)).ToArray();
        }

        public async Task<string> GenerateAuthCodeAsync(string emailAddress)
        {
            if (_networkMonitor.IsAvailable())
            {
				try{
                	return await _service.GenerateAuthCode(emailAddress.Trim());
				}catch(Exception ex) {
					_logger.Error (ex);
				}
            }
            return string.Empty;
        }

		public async Task<TrackState[]> GetTracks()
		{
            if(_networkMonitor.IsAvailable())
            {
				try{
	                var tracks = await _service.GetTracks ();
	                return tracks.ToArray ();
				}catch(Exception ex){
					_logger.Error (ex);
				}
            }

		    return new TrackState[0];
		}

        public async Task<EvaluationState> SubmitEvaluation(EvaluationState evalState, Guid sessionId)
        {
            var ticketId = AppSettings.UserId;
            
            _logger.Info("TicketId: {0}", ticketId);
            _logger.Info("Network Available: {0}", _networkMonitor.IsAvailable());

            if (ticketId == Guid.Empty || !_networkMonitor.IsAvailable())
                return evalState;

            try
            {
                var state = await _service.SubmitEvaluation(evalState, sessionId);
                var ticket = await _service.GetTicket(ticketId);
                if (ticket != null)
                {
                    var submissionState = ticket.EvaluationSubmissions.FirstOrDefault(x => x.Id == evalState.Id);
                    if (submissionState == null)
                    {
                        submissionState = new EvaluationSubmissionState
                        {
                            Id = evalState.Id,
                            SessionIds = new List<Guid> {sessionId}
                        };
                        ticket.EvaluationSubmissions.Add(submissionState);
                    }
                    else
                    {
                        if (!submissionState.SessionIds.Contains(sessionId))
                            submissionState.SessionIds.Add(sessionId);
                    }

                    await _cache.PutAsync(ticketId, ticket);
                }

                return state;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            return evalState;
        }

        public async Task<TicketState> GetUserTicketAsync()
        {
            var userId = AppSettings.UserId;
            if (userId == Guid.Empty)
                return new TicketState();
            var state = await _cache.TryGetAsync<TicketState>(userId);
            if (state != null)
                return state;

            state = await _service.GetTicket(userId);
            return await _cache.PutAsync(userId, state);
        }

        [DataContract]
        public class FavoriteSessions
        {
            public FavoriteSessions(IEnumerable<Guid> sessions)
            {
                Sessions = new HashSet<Guid>(sessions);
            }

            public FavoriteSessions()
            {
                Sessions = new HashSet<Guid>();
            }

            [DataMember]
            public HashSet<Guid> Sessions { get; set; }
        }
    }
}