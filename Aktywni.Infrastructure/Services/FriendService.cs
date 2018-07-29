using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO;
using AutoMapper;
using Aktywni.Infrastructure.Extensions;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public class FriendService : IFriendService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IMapper _mapper;
        public FriendService(IUserRepository userRepository, IFriendRepository friendRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse> GetFriendAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if(friend == null)
                return new ReturnResponse{ Response = false.ToString(), Error = "Brak znajomego"};

            var user = await _userRepository.GetAsync(friendID);
            FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
            return  new ReturnResponse { Response = true.ToString(), Info = _mapper.MergeInto<FriendDTO>(user, friend).ToString()};
        }

        public async Task<IEnumerable<FriendDTO>> GetAllFriendsAsync(int myID)
        {
            List<FriendDTO> listFriends = new List<FriendDTO>();
            var friends = await _friendRepository.GetAllAsync(myID);
            foreach (Friends item in friends)
            {
                int friendID;
                if (myID == item.FriendTo)
                    friendID = item.FriendFrom;
                else
                    friendID = item.FriendTo;

                var user = await _userRepository.GetAsync(friendID);
                FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
                friendDTO = _mapper.MergeInto<FriendDTO>(user, item);
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
                int friendID;
                if (myID == item.FriendTo)
                    friendID = item.FriendFrom;
                else
                    friendID = item.FriendTo;

                var user = await _userRepository.GetAsync(friendID);
                FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
                friendDTO = _mapper.MergeInto<FriendDTO>(user, item);
                listFriends.Add(friendDTO);
            }
            return listFriends;
        }

        public async Task<bool> AddFriendAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend != null)
            {
                return false;
                //throw new Exception($"Taki znajomy już istnieje.");
            }
            friend = new Friends(myID, friendID, false);
            await _friendRepository.AddAsync(friend);
            return true;
        }
        public async Task<bool> AcceptInvitationAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend == null || friend.FriendFrom == myID)
            {
                return false;
                //throw new Exception($"Brak zaproszenia od użytkownika.");
            }
            friend.AcceptInvitation();
            await _friendRepository.UpdateAsync(friend);
            return true;
        }
        public async Task<bool> RemoveFriendAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend == null)
            {
                return false;
                //throw new Exception($"Brak zaproszenia od użytkownika.");
            }
            await _friendRepository.DeleteAsync(friend);
            return true;
        }
    }
}