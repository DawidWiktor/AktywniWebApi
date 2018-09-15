using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO.UserComment;
using AutoMapper;

namespace Aktywni.Infrastructure.Services
{
    public class UserCommentService : IUserCommentService
    {
        private readonly IUserCommentRepository _userCommentRepository;
        private readonly IUserEventRepository _userEventRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public UserCommentService(IUserCommentRepository userCommentRepository, IUserEventRepository userEventRepository,
                                    IEventRepository eventRepository, IMapper mapper)
        {
            _userCommentRepository = userCommentRepository;
            _userEventRepository = userEventRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse> GetUserComments(int userId)
        {
            var userComments = FillDto(await _userCommentRepository.GetUserComments(userId));
            return new ReturnResponse { Response = true.ToString(), Info = userComments };
        }

        public async Task<ReturnResponse> GetMyComments(int myId)
        {
            var userComments = FillDto(await _userCommentRepository.GetMyComments(myId));
            return new ReturnResponse { Response = true.ToString(), Info = userComments };
        }

        public async Task<ReturnResponse> AddComment(int myId, int userIdRated, int eventId, int rate, string describe)
        {
            if(!await _userEventRepository.IsAdminInEvent(eventId, myId))
                return new ReturnResponse { Response = false.ToString(), Error = "Nie możesz ocenić uczestnika." };

            if(!await _userEventRepository.IsUserInEvent(eventId, userIdRated))
                return new ReturnResponse { Response = false.ToString(), Error = "Uczestnik nie brał udziału w wydarzeniu." };
        
            UserComments userComment = await _userCommentRepository.GetComment(myId, userIdRated, eventId);
            if(userComment != null)
                  return new ReturnResponse { Response = true.ToString(), Info = "Nie możesz drugi raz ocenić użytkownika." };
            await _userCommentRepository.AddAsync(userComment);
            return new ReturnResponse { Response = true.ToString(), Info = "Dodano ocenę." };
        }

        public async Task<ReturnResponse> UpdateComment(int myId, int userIdRated, int eventId, int rate, string describe)
        {
            UserComments userComment = await _userCommentRepository.GetComment(myId, userIdRated, eventId);
            userComment.Rate = rate;
            userComment.Describe = describe;
            await _userCommentRepository.UpdateAsync(userComment);
            return new ReturnResponse { Response = true.ToString(), Info = "Zaktualizowano ocenę." };
        }

        public async Task<ReturnResponse> RemoveComment(int myId, int userIdRated, int eventId)
        {
            UserComments userComment = await _userCommentRepository.GetComment(myId, userIdRated, eventId);
            await _userCommentRepository.DeleteAsync(userComment);
            return new ReturnResponse { Response = true.ToString(), Info = "Usunięto ocenę." };
        }

        private List<UserCommentDTO> FillDto(List<Tuple<int, string, int, string, int, string>> userComments)
        {
            List<UserCommentDTO> listUserComments = new List<UserCommentDTO>();
            foreach (var item in userComments)
                listUserComments.Add(new UserCommentDTO
                {
                    UserIdWhoComment = item.Item1,
                    UserLoginWhoComment = item.Item2,
                    UserIdRated = item.Item3,
                    UserLoginRated = item.Item4,
                    Rate = item.Item5,
                    Describe = item.Item6
                });

            return listUserComments;
        }
    }
}