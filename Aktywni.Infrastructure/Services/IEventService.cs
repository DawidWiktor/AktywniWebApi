using System;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IEventService
    {
        Task<ReturnResponse> GetEventAsync(int eventID, int userId);
        Task<ReturnResponse> GetEventAsync(string name, int userId);
        Task<ReturnResponse> GetAllEventsAsync(int userId);
        Task<ReturnResponse> GetAllMyEventsAsync(int userID);
        Task<ReturnResponse> SearchEventsAsync(string textInput, int userId); // wyszukiwanie po nazwie wydarzenia
        Task<ReturnResponse> SearchEventsInDisciplineAsync(string textInput, int disciplineId, int userId);
        Task<ReturnResponse> SearchEventsInDisciplineAndDistanceAsync(string textInput, int disciplineId, double distance, double latitude, double longitude, int userId);
        Task<ReturnResponse> SearchEventsNearest(double latitude, double longitude, int userId);
      //  Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, string description);
        // obecnie uzywana
        Task<ReturnResponse> AddEventAsync(string name, DateTime date, int whoCreatedID, bool IsCommerceUser, bool IsPrivate, int disciplineId, string description, decimal latitude, decimal longitude);
     //   Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, int disciplineId, decimal latitude, decimal longitude);
      //  Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, int admin, int disciplineId, decimal latitude, decimal longitude, string description);
        Task<ReturnResponse> ChangeNameEventAsync(int eventID, string newName);
        Task<ReturnResponse> ChangeVisibilityEventAsync(int eventID, string visibility);
        Task<ReturnResponse> ChangeDescription(int eventID, string description);
        Task<ReturnResponse> ChangeDateEventAsync(int eventID, DateTime date);
        Task<ReturnResponse> ChangeGeographicalCoordinatesEventAsync(int eventID, double latitude, double longitude);
        Task<ReturnResponse> RemoveEventAsync(int eventID);
    }
}