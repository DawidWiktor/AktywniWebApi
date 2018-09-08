using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO.MessageEventUser;
using AutoMapper;

namespace Aktywni.Infrastructure.Services
{
    public class MessageEventService : IMessageEventService
    {
        private readonly IMessageEventRepository _messageEventRepository;
        private readonly IUserEventRepository _userEventRepository;
        private readonly IMapper _mapper;
        public MessageEventService(IMessageEventRepository messageEventRepository, IUserEventRepository userEventRepository, IMapper mapper)
        {
            _messageEventRepository = messageEventRepository;
            _userEventRepository = userEventRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse> GetAllHeaderMessageEvent(int myId)
        {
            var tupleMessages = await _messageEventRepository.GetAllHeaderMessageEvent(myId);
            List<MessageEventListUserDTO> listMessages = new List<MessageEventListUserDTO>();
            foreach (var item in tupleMessages)
                listMessages.Add(new MessageEventListUserDTO { EventId = item.Item1, EventName = item.Item2, Date = item.Item3 });
            return new ReturnResponse { Response = true.ToString(), Info = listMessages };
        }

        public async Task<ReturnResponse> GetLatestMessagesInEvent(int myId, int eventId)
        {
            var latestMessagesEvent = await _messageEventRepository.GetLatestMessagesInEvent(myId, eventId);
            List<MessageEventUserDTO> listMessagesUser = FillListMessages(latestMessagesEvent);
            return new ReturnResponse { Response = true.ToString(), Info = listMessagesUser };
        }

        public async Task<ReturnResponse> GetUnreadMessagesInEvent(int myId, int eventId)
        {
            var unreadMessagesEvent = await _messageEventRepository.GetUnreadMessagesInEvent(myId, eventId);
            List<MessageEventUserDTO> listMessagesUser = FillListMessages(unreadMessagesEvent);
            return new ReturnResponse { Response =  true.ToString(), Info = listMessagesUser };
        }

        public async Task<ReturnResponse> GetHistoryMessagesInEvent(int myId, int eventId, int latestMessageId)
        {
            var historyMessagesEvent = await _messageEventRepository.GetHistoryMessagesInEvent(myId, eventId, latestMessageId);
            List<MessageEventUserDTO> listMessagesUser = FillListMessages(historyMessagesEvent);
            return new ReturnResponse { Response = true.ToString(), Info = listMessagesUser };
        }

        public async Task<ReturnResponse> SendMessageAsync(int userFromId, int eventId, string content)
        {
            var listUserInEvent = await _userEventRepository.GetUsersInEvent(eventId);
            bool ifSent = false;
            foreach (var item in listUserInEvent)
                ifSent = await _messageEventRepository.SendMessageAsync(userFromId, item.UserId, eventId, DateTime.Now, content);

            return new ReturnResponse { Response = ifSent.ToString(), Info = (ifSent) ? "Wysłano wiadomość" : "", Error = !(ifSent) ? "Błąd wysyłania" : "" };
        }

        private List<MessageEventUserDTO> FillListMessages(List<Tuple<int, int, string, string, DateTime, string>> list)
        {
            List<MessageEventUserDTO> listMessagesUser = new List<MessageEventUserDTO>();
            foreach (var item in list)
                listMessagesUser.Add(new MessageEventUserDTO { EventId = item.Item1, MessageId = item.Item2, EventName = item.Item3, Login = item.Item4, Date = item.Item5, Content = item.Item6 });
            return listMessagesUser;
        }
    }
}