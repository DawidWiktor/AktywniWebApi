using System;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IEventService
    {
        Task<ReturnResponse> GetEventAsync(int eventID);
        Task<ReturnResponse> GetEventAsync(string name);
        Task<ReturnResponse> GetAllEventsAsync();
        Task<ReturnResponse> GetAllMyEventsAsync(int userID);
        Task<ReturnResponse> SearchEventsAsync(string textInput); // wyszukiwanie po nazwie wydarzenia
        Task<ReturnResponse> SearchEventsInDisciplineAsync(string textInput, int disciplineId);
        Task<ReturnResponse> SearchEventsInDisciplineAndDistanceAsync(string textInput, int disciplineId, double distance);
        Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, string description);
        // obecnie uzywana
        Task<ReturnResponse> AddEventAsync(string name, DateTime date, int whoCreatedID, int disciplineId, string description, string geographicalCoordinates);
        Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, int disciplineId, string geographicalCoordinates);
        Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, int admin, int disciplineId, string geographicalCoordinates, string description);
        Task<ReturnResponse> ChangeNameEventAsync(int eventID, string newName);
        Task<ReturnResponse> ChangeVisibilityEventAsync(int eventID, string visibility);
        Task<ReturnResponse> ChangeDescription(int eventID, string description);
        Task<ReturnResponse> ChangeDateEventAsync(int eventID, DateTime date);
        Task<ReturnResponse> ChangeGeographicalCoordinatesEventAsync(int eventID, string geographicalCoordinates);
        Task<ReturnResponse> RemoveEventAsync(int eventID);
    }
}