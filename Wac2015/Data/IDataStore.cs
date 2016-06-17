using System;
using System.Threading.Tasks;
using XamarinEvolve.Mobile.Data;

namespace Wac2015.Data
{
    public interface IDataStore
    {
        Guid EventId { get; }
        Task RefreshAsync(RefreshInfo info);

        Task<EventState> GetEventAsync();
        
        Task<EventState> GetEventAsync(Guid id);

		Task<TrackState[]> GetTracks();

        Task<EventState> RefreshEvent();

        Task<TicketState> ValidateAuthCodeAsync(string code);
        
        Task<bool> IsLoggedInAsync();
        
        Task<SessionState[]> GetFavoriteSessionsAsync();

        Task<SessionState[]> AddFavoriteSessionAsync(Guid sessionId);
        Task<SessionState[]> RemoveFavoriteSessionAsync(Guid sessionId);

        Task<string> GenerateAuthCodeAsync(string emailAddress);

        Task<EvaluationState> SubmitEvaluation(EvaluationState evalState, Guid sessionId);

        Task<TicketState> GetUserTicketAsync();
    }
}