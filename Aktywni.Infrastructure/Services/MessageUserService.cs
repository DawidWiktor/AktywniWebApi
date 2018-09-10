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

        public async Task<ReturnResponse> GetAllHeaderMessagesUsers(int myId)
        {
            var tupleMessages = await _messageUserRepository.GetAllHeaderMessageUsers(myId);
            List<MessageListUsersDTO> listMessages = new List<MessageListUsersDTO>();
            foreach(var item in tupleMessages)
                listMessages.Add(new MessageListUsersDTO{UserFromId = item.Item1, UserLoginFrom = item.Item2});
            return new ReturnResponse { Response = true.ToString(), Info = listMessages };
        }

        public async Task<ReturnResponse> GetLatestMessagesInFriend(int myId, int friendId)
        {
            var latestMessagesInFriend = await _messageUserRepository.GetLatestMessageInFriend(myId, friendId);
            List<MessageUserDTO> listMessagesUser = new List<MessageUserDTO>();
            foreach(var item in latestMessagesInFriend)
                listMessagesUser.Add(new MessageUserDTO{UserFromId = item.Item1, UserId = item.Item2, MessageId = item.Item3, Date = item.Item4, Content = item.Item5, Login = item.Item6});
            return new ReturnResponse {Response = true.ToString(), Info = listMessagesUser};
        }

        public async Task<ReturnResponse> GetUnreadMessagesInFriend(int myId, int friendId)
        {
            var latestMessagesInFriend = await _messageUserRepository.GetUnreadMessagesInFriend(myId, friendId);
            List<MessageUserDTO> listMessagesUser = new List<MessageUserDTO>();
            foreach(var item in latestMessagesInFriend)
                listMessagesUser.Add(new MessageUserDTO{UserFromId = item.Item1, UserId = item.Item2, MessageId = item.Item3, Date = item.Item4, Content = item.Item5, Login = item.Item6});
            return new ReturnResponse {Response =  true.ToString(), Info = listMessagesUser};
        }

        public async Task<ReturnResponse> GetHistoryMessagesInFriend(int myId, int friendId, int latestMessageId)
          {
            var latestMessagesInFriend = await _messageUserRepository.GetHistoryMessagesInFriend(myId, friendId, latestMessageId);
            List<MessageUserDTO> listMessagesUser = new List<MessageUserDTO>();
            foreach(var item in latestMessagesInFriend)
                listMessagesUser.Add(new MessageUserDTO{UserFromId = item.Item1, UserId = item.Item2, MessageId = item.Item3, Date = item.Item4, Content = item.Item5, Login = item.Item6});
            return new ReturnResponse {Response =  true.ToString(), Info = listMessagesUser};
        }

        public async Task<ReturnResponse> SendMessageAsync(int userFromId, int userId, string content)
        {
            bool ifSent = await _messageUserRepository.SendMessageAsync(userFromId, userId, DateTime.Now, content);
            return new ReturnResponse {Response = ifSent.ToString(), Info = (ifSent) ? "Wysłano wiadomość" : "", Error = !(ifSent) ? "Błąd wysyłania" : ""};
        }

    }
}