using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aktywni.Core.Repositories
{
    public interface IMessageEventRepository
    {
        Task<IEnumerable<Tuple<int, string, DateTime>>> GetAllHeaderMessageEvent(int myId); // id wydarzenia i nazwa wydarzenia
        Task<List<Tuple<int, int, string, string, DateTime, string>>> GetLatestMessagesInEvent(int myId, int eventId); // id wydarzenia, id wiadomości, nazwa wydarzenia, nazwa użytkownika, data wiadomości, treść wiadomości
        Task<List<Tuple<int, int, string, string, DateTime, string>>> GetUnreadMessagesInEvent(int myId, int eventId);
        Task<List<Tuple<int, int, string, string, DateTime, string>>> GetHistoryMessagesInEvent(int myId, int eventId, int latestMessageId);
        Task<bool> SendMessageAsync(int userFrom, int userId, int eventId, DateTime date, string content);
    }
}