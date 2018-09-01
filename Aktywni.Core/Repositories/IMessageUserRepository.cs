using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;


namespace Aktywni.Core.Repositories
{
    public interface IMessageUserRepository
    {
        Task<List<Tuple<int, string>>> GetAllHeaderMessageUsers(int myId);
        Task<List<Tuple<int, int, DateTime, string>>> GetLatestMessageInFriend(int myId, int friendId);
        Task<List<Tuple<int, int, DateTime, string>>> GetHistoryMessageInFriend(int myId, int friendId);
        Task<bool> SendMessageAsync(int userFromId, int userId, DateTime date, string content);

    }
}