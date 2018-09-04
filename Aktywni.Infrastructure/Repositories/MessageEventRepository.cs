using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class MessageEventRepository : IMessageEventRepository
    {
        private readonly AktywniDBContext _dbContext;
        private readonly IUserEventRepository _userEventRepository;

        public MessageEventRepository(AktywniDBContext dBContext, IUserEventRepository userEventRepository)
        {
            _dbContext = dBContext;
            _userEventRepository = userEventRepository;
        }

        public async Task<IEnumerable<Tuple<int, string, DateTime>>> GetAllHeaderMessageEvent(int myId)
            => await _userEventRepository.GetEventsInUser(myId);

        // id wydarzenia, id wiadomości, nazwa wydarzenia nazwa użytkownika, data wiadomości, treść wiadomości
        public async Task<List<Tuple<int, int, string, string, DateTime, string>>> GetLatestMessagesInEvent(int myId, int eventId)
            => await _dbContext.MessageEvent.Where(x => x.EventId == eventId)
                                   .OrderByDescending(x => x.MessageEventId)
                                   .Where(x => x.UserToId == myId)
                                   .Take(10)
                                   .Select(z => new Tuple<int, int, string, string, DateTime, string>((int)z.EventId, z.MessageId, z.Event.Name, z.UserFrom.Login, z.Message.Date, z.Message.Content))
                                   .ToListAsync();

        public async Task<List<Tuple<int, int, string, string, DateTime, string>>> GetUnreadMessagesInEvent(int myId, int eventId)
            => await _dbContext.MessageEvent.Where(x => x.EventId == eventId)
                                   .Where(x => x.UserToId == myId)
                                   .Where(x => x.IsOpened == false)
                                   .OrderByDescending(x => x.MessageEventId)
                                   .Select(z => new Tuple<int, int, string, string, DateTime, string>((int)z.EventId, z.MessageId, z.Event.Name, z.UserFrom.Login, z.Message.Date, z.Message.Content))
                                   .ToListAsync();

        public async Task<List<Tuple<int, int, string, string, DateTime, string>>> GetHistoryMessagesInEvent(int myId, int eventId, int latestMessageId)
            => await _dbContext.MessageEvent.Where(x => x.EventId == eventId)
                                   .Where(x => x.UserToId == myId)
                                   .Where(x => x.MessageId < latestMessageId)
                                   .Take(10)
                                   .OrderByDescending(x => x.MessageEventId)
                                   .Select(z => new Tuple<int, int, string, string, DateTime, string>((int)z.EventId, z.MessageId, z.Event.Name, z.UserFrom.Login, z.Message.Date, z.Message.Content))
                                   .ToListAsync();

        public async Task<bool> SendMessageAsync(int userFrom, int userId, int eventId, DateTime date, string content)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlCommandAsync("dbo.InsertMessageEvent @p0, @p1, @p2, @p3, @p4", userFrom, userId, eventId, date, content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}