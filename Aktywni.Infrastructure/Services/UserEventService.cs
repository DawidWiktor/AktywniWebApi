using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO;
using AutoMapper;

namespace Aktywni.Infrastructure.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _userEventRepository;
        private readonly IMapper _mapper;
        public UserEventService(IUserEventRepository userEventRepository, IMapper mapper)
        {
            _userEventRepository = userEventRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse> GetUsersInEventAsync(int eventID)
        {
            var userEvent = await _userEventRepository.GetUsersInEvent(eventID);
            List<UserEventDTO> userEventDto = _mapper.Map<IEnumerable<UsersEvents>, List<UserEventDTO>>(userEvent);
            return new ReturnResponse { Response = (userEventDto.Count == 0) ? false.ToString() : true.ToString(), Info = userEventDto };
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