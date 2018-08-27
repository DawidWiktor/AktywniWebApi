using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserEventService
    {
        Task<ReturnResponse> GetUsersInEventAsync(int eventID);
        Task<ReturnResponse> AddUserInEvent(int eventID, int userID);
        Task<ReturnResponse> RemoveUserInEvent(int eventID, int userID);
    }
}