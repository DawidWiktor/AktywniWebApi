using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserEventService
    {
        Task<ReturnResponse> GetUsersInEventAsync(int eventID);
        Task<ReturnResponse> GetEventInUser(int myId);
        Task<ReturnResponse> JoinToEventAsync(int myId, int eventID); // akceptacja zaproszenia lub dolaczenie do wydarzenia
        Task<ReturnResponse> ExceptFromEventAsync(int myId, int eventID); // odejscie, bądź usunięcie zaproszenia
        Task<ReturnResponse> AddUserInEvent(int eventID, int userID);
        Task<ReturnResponse> RemoveUserInEvent(int eventID, int userID);
    }
}