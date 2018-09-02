using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;


namespace Aktywni.Core.Repositories
{
    public interface IMessageUserRepository
    {
        Task<List<Tuple<int, string>>> GetAllHeaderMessageUsers(int myId); // id uzytkownika i login
        Task<List<Tuple<int, int, int, DateTime, string>>> GetLatestMessageInFriend(int myId, int friendId); // id uzytkownika, który wysłał; id użytkownika odbierającego, id wiadomości, data wiadomości, treść wiadomości
        Task<List<Tuple<int, int, int, DateTime, string>>> GetUnreadMessagesInFriend(int myId, int friendId);
        Task<List<Tuple<int, int, int, DateTime, string>>> GetHistoryMessagesInFriend(int myId, int friendId, int latestMessageId);
        Task<bool> SendMessageAsync(int userFromId, int userId, DateTime date, string content);

    }
}