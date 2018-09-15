using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IMessageUserService
    {
        Task<ReturnResponse> GetAllHeaderMessagesUsers(int myId);
        Task<ReturnResponse> GetLatestMessagesInFriend(int myId, int friendId);
        Task<ReturnResponse> GetUnreadMessagesInFriend(int myId, int friendId);
        Task<ReturnResponse> GetHistoryMessagesInFriend(int myId, int friendId, int latestMessageId);
        Task<ReturnResponse> SendMessageAsync(int userFromId, int userId, string content);
        Task<ReturnResponse> IsUnreadMessage(int myId);
        Task<ReturnResponse> IsUnreadMessageFromUser(int myId, int userFromId);
    }
}