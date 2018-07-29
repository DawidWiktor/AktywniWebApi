using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IFriendService
    {
        Task<ReturnResponse> GetFriendAsync(int myID, int friendID);
        Task<ReturnResponse> GetAllFriendsAsync(int myID);
        Task<ReturnResponse> SearchFriendsAsync(int myID, string textInput);
        Task<ReturnResponse> AddFriendAsync(int myID, int friendID);
        Task<ReturnResponse> AcceptInvitationAsync(int myID, int friendID);
        Task<ReturnResponse> RemoveFriendAsync(int myID, int friendID);

    }
}