using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserEventService
    {
        Task<ReturnResponse> GetUsersInEventAsync(int eventID);     // uzyskanie listy użytkowników, którzy uczestniczą w wydarzeniu
        Task<ReturnResponse> GetEventInUser(int myId);              // uzyskanie listy wydarzeń gdzie bierzemy udział lub jesteśmy zaproszeni
        Task<ReturnResponse> GetMyInvitationsEvent(int userId);     // uzyskanie listy wydarzeń gdzie zostaliśmy zaproszeni
        Task<ReturnResponse> GetHistoryEvents(int userId);          // uzyskanie listy wydarzeń, w których braliśmy udział i są zakończone
        Task<ReturnResponse> JoinToEventAsync(int myId, int eventID); // akceptacja zaproszenia lub dolaczenie do wydarzenia
        Task<ReturnResponse> ExceptFromEventAsync(int myId, int eventID); // odejscie, bądź usunięcie zaproszenia
        Task<ReturnResponse> AddUserInEvent(int eventID, int userID);
        Task<ReturnResponse> RemoveUserInEvent(int eventID, int userID);
    }
}