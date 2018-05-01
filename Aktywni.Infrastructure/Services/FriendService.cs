using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO;
using AutoMapper;

namespace Aktywni.Infrastructure.Services
{
    public class FriendService : IFriendService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        public FriendService(IUserRepository userRepository, IFriendRepository friendRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<FriendDTO> GetFriendAsync(int myID, int friendID)
        {
            var user = await _userRepository.GetAsync(friendID);
            var friend = await _friendRepository.GetAsync(myID, friendID);
            FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
            return _mapper.Map<Friends, FriendDTO>(friend, friendDTO);
        }

        public async Task<IEnumerable<FriendDTO>> GetAllFriendsAsync(int myID)
        {
            List<FriendDTO> listFriends = new List<FriendDTO>();
            var friends = await _friendRepository.GetAllAsync(myID);
            foreach (Friends item in friends)
            {
                var user = await _userRepository.GetAsync(item.FriendFrom);
                FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
                friendDTO = _mapper.Map<Friends, FriendDTO>(item);
                listFriends.Add(friendDTO);
            }
            return listFriends;
        }

        public async Task<IEnumerable<FriendDTO>> SearchFriendsAsync(int myID, string textInput)
        {
            List<FriendDTO> listFriends = new List<FriendDTO>();
            var friends = await _friendRepository.GetFromTextAsync(myID, textInput);
            foreach (Friends item in friends)
            {
                var user = await _userRepository.GetAsync(item.FriendFrom);
                FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
                friendDTO = _mapper.Map<Friends, FriendDTO>(item);
                listFriends.Add(friendDTO);
            }
            return listFriends;
        }

        public async Task AddFriendAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend != null)
            {
                throw new Exception($"Taki znajomy już istnieje.");
            }
            friend = new Friends(myID, friendID, false);
            await _friendRepository.AddAsync(friend);
        }
        public async Task AcceptInvitationAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend == null)
            {
                throw new Exception($"Brak zaproszenia od użytkownika.");
            }
            friend.AcceptInvitation();
            await _friendRepository.UpdateAsync(friend);
        }
        public async Task RemoveFriendAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend == null)
            {
                throw new Exception($"Brak zaproszenia od użytkownika.");
            }
            await _friendRepository.DeleteAsync(friend);
        }
    }
}