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
            if (friend == null)
                return new ReturnResponse { Response = false.ToString(), Error = "Brak znajomego" };

            var user = await _userRepository.GetAsync(friendID);
            FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
            return new ReturnResponse { Response = true.ToString(), Info = _mapper.MergeInto<FriendDTO>(user, friend) };
        }

        public async Task<ReturnResponse> GetAllFriendsAsync(int myID)
        {
            List<FriendDTO> listFriends = await GetListFriends(myID, "");
            return new ReturnResponse { Response = true.ToString(), Info = listFriends };
        }

        public async Task<ReturnResponse> GetInvitations(int myId)
         {
            List<FriendDTO> listFriends = new List<FriendDTO>();
            IEnumerable<Friends> friendsTemp = await _friendRepository.GetInvitations(myId);
            
            foreach (Friends item in friendsTemp)
            {
                var user = await _userRepository.GetAsync(item.FriendFrom);
                FriendDTO friendDTO = _mapper.Map<Users, FriendDTO>(user);
                friendDTO = _mapper.MergeInto<FriendDTO>(user, item);
                listFriends.Add(friendDTO);
            }
            return new ReturnResponse { Response = true.ToString(), Info = listFriends};
        }

        public async Task<ReturnResponse> SearchFriendsAsync(int myID, string textInput)
        {
            List<FriendDTO> listFriends = await GetListFriends(myID, textInput);
            return new ReturnResponse { Response = true.ToString(), Info = listFriends };
        }

        private async Task<List<FriendDTO>> GetListFriends(int myID, string textInput)
        {
            List<FriendDTO> listFriends = new List<FriendDTO>();
            IEnumerable<Friends> friendsTemp;
            if (string.IsNullOrWhiteSpace(textInput))
                friendsTemp = await _friendRepository.GetAllAsync(myID);
            else
                friendsTemp = await _friendRepository.GetFromTextAsync(myID, textInput);

            foreach (Friends item in friendsTemp)
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
        public async Task<ReturnResponse> AddFriendAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend != null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Taki znajomy już istnieje." };
            }
            friend = new Friends(myID, friendID, false);
            await _friendRepository.AddAsync(friend);
            return new ReturnResponse { Response = true.ToString(), Info = "Wysłano zaproszenie do użytkownika." };
        }
        public async Task<ReturnResponse> AcceptInvitationAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend == null || friend.FriendFrom == myID)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Brak zaproszenia od użytkownika." };
            }
            friend.AcceptInvitation();
            await _friendRepository.UpdateAsync(friend);
            return new ReturnResponse { Response = true.ToString(), Info = "Dodanie znajomego." };
        }
        public async Task<ReturnResponse> RemoveFriendAsync(int myID, int friendID)
        {
            var friend = await _friendRepository.GetAsync(myID, friendID);
            if (friend == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Brak zaproszenia od użytkownika" };
            }
            await _friendRepository.DeleteAsync(friend);
            return new ReturnResponse { Response = true.ToString(), Info = "Usunięto znajomego." };
        }
    }
}