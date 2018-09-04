using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IMessageEventService
    {
        Task<ReturnResponse> GetAllHeaderMessageEvent(int myId);
        Task<ReturnResponse> GetLatestMessagesInEvent(int myId, int eventId);
        Task<ReturnResponse> GetUnreadMessagesInEvent(int myId, int eventId);
        Task<ReturnResponse> GetHistoryMessagesInEvent(int myId, int eventId, int latestMessageId);
        Task<ReturnResponse> SendMessageAsync(int userFrom, int eventId, string content);
    }
}