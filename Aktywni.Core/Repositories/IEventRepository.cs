using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IEventRepository
    {
        Task<Events> GetEventAsync(int eventID);
        Task<Events> GetEventAsync(int eventID, int userId);
        Task<Events> GetEventAsync(string eventName);
        Task<Events> GetEventAsync(string eventName, int userId);
        Task<int> GetIdEvent(string eventName);
        Task<IEnumerable<Events>> GetAllEventsAsync(int userId);
        Task<IEnumerable<Events>> GetAllMyEventsAsync(int userID);
        Task<IEnumerable<Events>> GetFromTextAsync(string textInput, int userId);
        Task<IEnumerable<Events>> GetFromTextAndDisciplineAsync(string textInput, int disciplineID, int userId);
        Task<IEnumerable<Events>> GetFromTextAndDisciplineAndDistanceAsync(string textInput, int disciplineID, double distance, double latitude, double longitude, int userId);
        Task<IEnumerable<Events>> GetNearestEvents(double latitude, double longitude, int userId);
        Task<IEnumerable<Events>> GetEventInDisciplineAsync(int disciplineID, int userId);
        Task<IEnumerable<Events>> GetEventsWhereNotComments(int userId);
        Task AddAsync(Events obj);
        Task UpdateAsync(Events obj);
        Task DeleteAsync(int eventID);
    }
}