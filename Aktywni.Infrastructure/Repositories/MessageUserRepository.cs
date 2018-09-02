using System;
using System.Collections.Generic;
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

        public async Task<List<Tuple<int, int, int, DateTime, string>>> GetLatestMessageInFriend(int myId, int friendId) // id uzytkownika, który wysłał; id użytkownika odbierającego, id wiadomości, data wiadomości, treść wiadomości
            => await _dbContext.MessageUser
                .Where(x => ((x.UserFromId == myId && x.UserId == friendId) || (x.UserId == myId && x.UserFromId == friendId)))
                .OrderByDescending(x=>x.Message.Date)
                .Take(10)
                .Select(z => new Tuple<int, int, int, DateTime, string>((int)z.UserFromId, z.UserId, z.MessageId, z.Message.Date, z.Message.Content))
                .ToListAsync();

        public async Task<List<Tuple<int, int, int, DateTime, string>>> GetUnreadMessagesInFriend(int myId, int friendId)
            => await _dbContext.MessageUser
                .Where(x => ((x.UserFromId == myId && x.UserId == friendId) || (x.UserId == myId && x.UserFromId == friendId)))
                .Where(x=> x.IsOpened == false)
                .OrderByDescending(x=>x.Message.Date)
                .Select(z => new Tuple<int, int, int, DateTime, string>((int)z.UserFromId, z.UserId, z.MessageId, z.Message.Date, z.Message.Content))
                .ToListAsync(); 

        public async Task<List<Tuple<int, int, int, DateTime, string>>> GetHistoryMessagesInFriend(int myId, int friendId, int latestMessageId)
            => await _dbContext.MessageUser
                .Where(x => ((x.UserFromId == myId && x.UserId == friendId) || (x.UserId == myId && x.UserFromId == friendId)))
                .Where(x => x.MessageId < latestMessageId)
                .OrderByDescending(x=>x.Message.Date)
                .Take(10)
                .Select(z => new Tuple<int, int, int, DateTime, string>((int)z.UserFromId, z.UserId, z.MessageId, z.Message.Date, z.Message.Content))
                .ToListAsync(); 


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