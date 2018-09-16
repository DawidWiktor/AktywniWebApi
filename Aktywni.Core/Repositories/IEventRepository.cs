using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IEventRepository
    {
        Task<Events> GetEventAsync(int eventID);
        Task<Events> GetEventAsync(string eventName);
        Task<int> GetIdEvent(string eventName);
        Task<IEnumerable<Events>> GetAllEventsAsync();
        Task<IEnumerable<Events>> GetAllMyEventsAsync(int userID);
        Task<IEnumerable<Events>> GetFromTextAsync(string textInput);
        Task<IEnumerable<Events>> GetFromTextAndDisciplineAsync(string textInput, int disciplineID);
        Task<IEnumerable<Events>> GetFromTextAndDisciplineAndDistanceAsync(string textInput, int disciplineID, double distance, double latitude, double longitude);
        Task<IEnumerable<Events>> GetDisciplineAndDistanceAsync(int disciplineID, double distance, double latitude, double longitude);
        Task<IEnumerable<Events>> GetNearestEvents(double latitude, double longitude);
        Task<IEnumerable<Events>> GetEventInDisciplineAsync(int disciplineID);
        Task AddAsync(Events obj);
        Task UpdateAsync(Events obj);
        Task DeleteAsync(int eventID);
    }
}