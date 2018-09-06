using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO.MessageEventUser;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IEnumerable<Tuple<int, string, DateTime, bool>>> GetAllHeaderMessageEvent(int myId)
            => await _userEventRepository.GetEventsInUser(myId);

        // id wydarzenia, id wiadomości, nazwa wydarzenia nazwa użytkownika, data wiadomości, treść wiadomości
        public async Task<List<Tuple<int, int, string, string, DateTime, string>>> GetLatestMessagesInEvent(int myId, int eventId)
            => await GetFromDataBase("dbo.SelectLastMessageEvent", myId, eventId);

        public async Task<List<Tuple<int, int, string, string, DateTime, string>>> GetUnreadMessagesInEvent(int myId, int eventId)
            => await GetFromDataBase("dbo.SelectUnreadMessageEvent", myId, eventId);

        public async Task<List<Tuple<int, int, string, string, DateTime, string>>> GetHistoryMessagesInEvent(int myId, int eventId, int latestMessageId)
             => await GetFromDataBase("dbo.GetHistoryMessagesInEvent", myId, eventId, latestMessageId);

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

        private async Task<List<Tuple<int, int, string, string, DateTime, string>>> GetFromDataBase(string procedureName, int myId, int eventId, int latestMessageId = -2)
        {
            using (DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@myId", SqlDbType.Int) { Value = myId });
                command.Parameters.Add(new SqlParameter("@eventId", SqlDbType.Int) { Value = eventId });
                if (latestMessageId > 0)
                    command.Parameters.Add(new SqlParameter("@latestMessageId", SqlDbType.Int) { Value = latestMessageId });

                _dbContext.Database.OpenConnection();

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                List<Tuple<int, int, string, string, DateTime, string>> eventMessage = new List<Tuple<int, int, string, string, DateTime, string>>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        eventMessage.Add(new Tuple<int, int, string, string, DateTime, string>(Convert.ToInt32(reader[0].ToString()),
                             Convert.ToInt32(reader[1].ToString()), reader[2].ToString(), reader[3].ToString(),
                             Convert.ToDateTime(reader[4].ToString()), reader[5].ToString()));
                    }
                    return eventMessage;
                }
            }
        }
    }
}