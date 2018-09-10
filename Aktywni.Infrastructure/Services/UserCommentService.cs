using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IMapper _mapper;
        public UserCommentService(IUserCommentRepository userCommentRepository, IUserEventRepository userEventRepository, IMapper mapper)
        {
            _userCommentRepository = userCommentRepository;
            _userEventRepository = userEventRepository;
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

        public async Task<ReturnResponse> AddComment(int myId, int userIdRated, int rate, int describe)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ReturnResponse> UpdateComment(int myId, int userIdRated, int rate, int describe)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ReturnResponse> RemoveComment(int myId, int userIdRated)
        {
            throw new System.NotImplementedException();
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