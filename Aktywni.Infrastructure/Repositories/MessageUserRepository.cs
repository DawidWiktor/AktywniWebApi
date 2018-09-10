using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO.MessageUser;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class MessageUserRepository : IMessageUserRepository
    {
        private readonly AktywniDBContext _dbContext;
        private readonly IUserRepository _userRepository;
        public MessageUserRepository(AktywniDBContext dBContext, IUserRepository userRepository)
        {
            _dbContext = dBContext;
            _userRepository = userRepository;
        }

        public async Task<List<Tuple<int, string>>> GetAllHeaderMessageUsers(int myId) // id uzytkownika i login
        {
            IEnumerable<MessageUser> messagesToUser = _dbContext.MessageUser.Where(x => x.UserFromId == myId) // wybranie listy osób, do których pisaliśmy (bez duplikatów)
                                    .GroupBy(p => p.UserId, (a, b) => b.OrderByDescending(e => e.MessageUserId)).Select(s => s.FirstOrDefault());

            IEnumerable<MessageUser> messagesFromUser = _dbContext.MessageUser.Where(x => x.UserId == myId) // wybranie listy osób, które do nas pisały (bez duplikatów)
                                    .GroupBy(p => p.UserFromId, (a, b) => b.OrderByDescending(e => e.MessageUserId)).Select(s => s.FirstOrDefault());

            List<Tuple<int, string>> listUsersMessage = new List<Tuple<int, string>>(); // id uzytkownika i login

            await AddUserHeaderToList(listUsersMessage, messagesToUser);
            await AddUserHeaderToList(listUsersMessage, messagesFromUser);

            RemoveDuplicateFromList(listUsersMessage);
            return listUsersMessage;
            //          await _dbContext.MessageUser.Where(x => (x.UserFromId == myId || x.UserId == myId)).GroupBy(x=>x.)
            //                                     .Select(z => new Tuple<int, int, DateTime, string>((int)z.UserFromId, z.UserId, z.Message.Date, z.Message.Content)).Distinct(z =>z.).ToListAsync();
        }

        public async Task<List<Tuple<int, int, int, DateTime, string, string>>> GetLatestMessageInFriend(int myId, int friendId) // id uzytkownika, który wysłał; id użytkownika odbierającego, id wiadomości, data wiadomości, treść wiadomości, logi użytkownika
            => await GetFromDataBase("dbo.GetLatestMessageInFriend", myId, friendId);

        public async Task<List<Tuple<int, int, int, DateTime, string, string>>> GetUnreadMessagesInFriend(int myId, int friendId)
            => await GetFromDataBase("dbo.GetUnreadMessagesInFriend", myId, friendId);

        public async Task<List<Tuple<int, int, int, DateTime, string, string>>> GetHistoryMessagesInFriend(int myId, int friendId, int latestMessageId)
             => await GetFromDataBase("dbo.GetHistoryMessagesInFriend", myId, friendId, latestMessageId);

        public async Task<bool> SendMessageAsync(int userFromId, int userId, DateTime date, string content)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlCommandAsync("dbo.InsertMessage @p0, @p1, @p2, @p3", userFromId, userId, date, content);
                return true;
            }
            catch
            {
                return false;
            }
        }



        #region [ PRIVATE METHOD ]

        private async Task<List<Tuple<int, int, int, DateTime, string, string>>> GetFromDataBase(string procedureName, int myId, int friendId, int latestMessageId = -2)
        {
            using (DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@myId", SqlDbType.Int) { Value = myId });
                command.Parameters.Add(new SqlParameter("@friendId", SqlDbType.Int) { Value = friendId });
                if (latestMessageId > 0)
                    command.Parameters.Add(new SqlParameter("@latestMessageId", SqlDbType.Int) { Value = latestMessageId });

                _dbContext.Database.OpenConnection();

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                List<Tuple<int, int, int, DateTime, string, string>> userMessage = new List<Tuple<int, int, int, DateTime, string, string>>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        userMessage.Add(new Tuple<int, int, int, DateTime, string, string>(Convert.ToInt32(reader[0].ToString()),
                             Convert.ToInt32(reader[1].ToString()), Convert.ToInt32(reader[2].ToString()),
                             Convert.ToDateTime(reader[3].ToString()), reader[4].ToString(), reader[5].ToString()));
                    }
                    return userMessage;
                }
            }
        }

        private async Task<List<Tuple<int, string>>> AddUserHeaderToList(List<Tuple<int, string>> listUsersMessage, IEnumerable<MessageUser> listMessageFromDb)
        {
            foreach (var item in listMessageFromDb)
            {
                Users user = await _userRepository.GetAsync((int)item.UserId);
                listUsersMessage.Add(new Tuple<int, string>((int)item.UserId, user.Login));
            }
            return listUsersMessage;
        }

        private List<Tuple<int, string>> RemoveDuplicateFromList(List<Tuple<int, string>> listUsersMessage)
        {
            for (int i = 0; i < listUsersMessage.Count; i++)   // usuniecie duplikatow
            {
                for (int j = i + 1; j < listUsersMessage.Count; j++)
                {
                    if (listUsersMessage[i].Item1 == listUsersMessage[j].Item1)
                        listUsersMessage.Remove(listUsersMessage[j]);
                }
            }
            return listUsersMessage;
        }

        #endregion[ PRIVATE MEthOD]
    }
}