using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IMessageUserService
    {
        Task<ReturnResponse> GetAllHeaderMessageUsers(int myId);
        Task<ReturnResponse> GetLatestMessageInFriend(int myId, int friendId);
        Task<ReturnResponse> SendMessageAsync(int userFromId, int userId, string content);
        Task<ReturnResponse> GetPartMessagesInFriendAsync(int myId, int friendId);
    }
}