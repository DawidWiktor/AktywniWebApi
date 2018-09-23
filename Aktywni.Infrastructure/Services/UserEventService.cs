using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.DTO.UserEvent;
using AutoMapper;

namespace Aktywni.Infrastructure.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _userEventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public UserEventService(IUserEventRepository userEventRepository, IUserRepository userRepository,
                                 IEventRepository eventRepository, IMapper mapper)
        {
            _userEventRepository = userEventRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse> GetUsersInEventAsync(int eventID)
        {
            var userEvent = await _userEventRepository.GetUsersInEvent(eventID);
            List<UserEventDTO> userEventDto = _mapper.Map<IEnumerable<UsersEvents>, List<UserEventDTO>>(userEvent);
            foreach (var item in userEventDto)
            {
                Users user = await _userRepository.GetAsync(item.UserId);
                item.Login = user.Login;
            }
            return new ReturnResponse { Response = true.ToString(), Info = userEventDto };
        }

        public async Task<ReturnResponse> GetEventInUser(int myId)
        {
            var eventsUser = await _userEventRepository.GetEventsInUser(myId);
            List<EventsInUserDTO> listEventsUser = new List<EventsInUserDTO>();
            foreach (var item in eventsUser)
            {
                listEventsUser.Add(new EventsInUserDTO { EventId = item.Item1, EventName = item.Item2, Date = item.Item3, IsAccepted = item.Item4 });
            }
            return new ReturnResponse { Response = true.ToString(), Info = listEventsUser };
        }

        public async Task<ReturnResponse> GetMyInvitationsEvent(int myId)
        {
            var eventsUser = await _userEventRepository.GetMyInvitationsEvent(myId);
            List<InvitationsDTO> listEventsUser = new List<InvitationsDTO>();
            foreach (var item in eventsUser)
            {
                listEventsUser.Add(new InvitationsDTO { EventId = item.Item1, EventName = item.Item2, Date = item.Item3});
            }
            return new ReturnResponse { Response = true.ToString(), Info = listEventsUser };
        }

        public async Task<ReturnResponse> GetHistoryEvents(int myId)
        {
             var eventsUser = await _userEventRepository.GetHistoryEvents(myId);
            List<HistoryDTO> listEventsUser = new List<HistoryDTO>();
            foreach (var item in eventsUser)
            {
                listEventsUser.Add(new HistoryDTO { EventId = item.Item1, EventName = item.Item2, Date = item.Item3, Latitude = item.Item4, Longitude = item.Item5, DisciplineID = item.Item6, Description = item.Item7});
            }
            return new ReturnResponse { Response = true.ToString(), Info = listEventsUser };
        }

        public async Task<ReturnResponse> JoinToEventAsync(int myId, int eventID) // akceptacja zaproszenia lub dolaczenie do wydarzenia
        {
            UsersEvents userEvent = await _userEventRepository.GetUserInEvent(eventID, myId);
            if (userEvent != null && userEvent.IsAccepted == true)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Dołączyłeś już do wydarzenia." };
            }
            if (userEvent == null)
            {
                UsersEvents newUserEvent = new UsersEvents(eventID, myId, true);
                await _userEventRepository.AddAsync(newUserEvent);
            }
            else
            {
                userEvent.IsAccepted = true;
                await _userEventRepository.UpdateAsync(userEvent);
            }

            return new ReturnResponse { Response = true.ToString(), Info = "Dołączyłeś do wydarzenia." };
        }

        public async Task<ReturnResponse> ExceptFromEventAsync(int myId, int eventID) // odejscie, bądź usunięcie zaproszenia
        {
            UsersEvents userEvent = await _userEventRepository.GetUserInEvent(eventID, myId);
            if (userEvent == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Nie jesteś uczestnikiem wydarzenia." };
            }

            await _userEventRepository.DeleteAsync(userEvent);
            return new ReturnResponse { Response = true.ToString(), Info = "Usunięto użytkownika z wydarzenia." };
        }

        public async Task<ReturnResponse> AddUserInEvent(int eventID, int userID)
        {
            bool ifUserInEvent = await _userEventRepository.CheckUserInEvent(eventID, userID);
            if (ifUserInEvent)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Taki użytkownik już istnieje." };
            }
            UsersEvents newUserEvent = new UsersEvents(eventID, userID);
            await _userEventRepository.AddAsync(newUserEvent);
            return new ReturnResponse { Response = true.ToString(), Info = "Dodano użytkownika do wydarzenia." };
        }

        public async Task<ReturnResponse> RemoveUserInEvent(int eventID, int userID)
        {
            UsersEvents userEvent = await _userEventRepository.GetUserInEvent(eventID, userID);
            if (userEvent == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Taki użytkownik nie jest dodany do wydarzenia." };
            }

            await _userEventRepository.DeleteAsync(userEvent);
            return new ReturnResponse { Response = true.ToString(), Info = "Usunięto użytkownika z wydarzenia." };
        }
    }
}