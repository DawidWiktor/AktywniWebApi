using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IFriendService
    {
        Task<FriendDTO> GetFriendAsync(int myID, int friendID);
        Task<IEnumerable<FriendDTO>> GetAllFriendsAsync(int myID);
        Task<IEnumerable<FriendDTO>> SearchFriendsAsync(int myID, string textInput);
        Task<bool> AddFriendAsync(int myID, int friendID);
        Task<bool> AcceptInvitationAsync(int myID, int friendID);
        Task<bool> RemoveFriendAsync(int myID, int friendID);

    }
}