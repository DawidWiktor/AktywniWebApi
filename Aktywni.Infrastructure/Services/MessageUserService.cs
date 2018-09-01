using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO.MessageUser;
using AutoMapper;

namespace Aktywni.Infrastructure.Services
{
    public class MessageUserService : IMessageUserService
    {
        private readonly IMessageUserRepository _messageUserRepository;
//private readonly IMapper _mapper;
        public MessageUserService(IMessageUserRepository messageUserRepository)
        {
            _messageUserRepository = messageUserRepository;
    //        _mapper = mapper;
        }

        public async Task<ReturnResponse> GetAllHeaderMessageUsers(int myId)
        {
            var tupleMessages = await _messageUserRepository.GetAllHeaderMessageUsers(myId);
            List<MessageListUsersDTO> listMessages = new List<MessageListUsersDTO>();
            foreach(var item in tupleMessages)
                listMessages.Add(new MessageListUsersDTO{UserFromId = item.Item1, UserLoginFrom = item.Item2});
            return new ReturnResponse { Response = (listMessages.Count == 0) ? false.ToString() : true.ToString(), Info = listMessages };
        }

        public async Task<ReturnResponse> GetLatestMessageInFriend(int myId, int friendId)
        {
            var latestMessagesInFriend = await _messageUserRepository.GetLatestMessageInFriend(myId, friendId);
            List<MessageUserDTO> listMessagesUser = new List<MessageUserDTO>();
            foreach(var item in latestMessagesInFriend)
                listMessagesUser.Add(new MessageUserDTO{UserFromId = item.Item1, UserId = item.Item2, Date = item.Item3, Content = item.Item4});
            return new ReturnResponse {Response = (listMessagesUser.Count == 0) ? false.ToString() : true.ToString(), Info = listMessagesUser};
        }

        public async Task<ReturnResponse> SendMessageAsync(int userFromId, int userId, string content)
        {
            bool ifSent = await _messageUserRepository.SendMessageAsync(userFromId, userId, DateTime.Now, content);
            return new ReturnResponse {Response = ifSent.ToString(), Info = (ifSent) ? "Wysłano wiadomość" : "", Error = !(ifSent) ? "Błąd wysyłania" : ""};
        }

        public async Task<ReturnResponse> GetPartMessagesInFriendAsync(int myId, int friendId)
        {
            throw new NotImplementedException();
        }
    }
}