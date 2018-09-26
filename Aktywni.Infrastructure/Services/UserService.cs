using System;
using System.Threading.Tasks;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Extensions;
using Aktywni.Core.Model;
using Aktywni.Infrastructure.Services;
using AutoMapper;
using CryptoHelper;
using System.Collections.Generic;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.Commands.User;
using Aktywni.Infrastructure.DTO.Abonament;

namespace Aktywni.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<AccountDTO> GetAccountAsync(int userId)
        {
            var user = await _userRepository.GetAsync(userId);
            AccountDTO us = _mapper.Map<Users, AccountDTO>(user);
            List<UserComments> listUserComments = await _userRepository.GetComments(userId);
            decimal sum = 0;
            foreach(var item in listUserComments)
                sum += item.Rate;

            us.Rate = sum / listUserComments.Count;
            return us;
        }

        public async Task<ReturnResponse> RegisterAsync(string login, string email, string password)
        {
            var user = await _userRepository.GetAsync(login);
            if (user != null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Użytkownik o takim loginie już istnieje." };
            }
            user = new Users(login, email, password);
            await _userRepository.AddAsync(user);
            return new ReturnResponse { Response = true.ToString() };
        }

        public async Task<ReturnResponse> LoginAsync(string login, string password)
        {

            Console.WriteLine(DateTime.Now);
            var user = await _userRepository.GetAsync(login);
            if (user == null || !(bool)user.IsActive || !Crypto.VerifyHashedPassword(user.Password, password))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędny login lub hasło." };
            }
            var jwt = _jwtHandler.CreateToken(user.UserId, user.Role);
            return new ReturnResponse
            {
                Response = true.ToString(),
                Info = new TokenDTO
                {
                    Token = jwt.Token,
                    Expires = jwt.Expires,
                    Role = user.Role
                }
            };
        }

        public async Task<ReturnResponse> LogoutAsync(int userID)
        {
            var user = await _userRepository.GetAsync(userID);
            if (user == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błąd wylogowania." };
            }
            await CheckAndUpdateRoleUser(userID);
            return new ReturnResponse { Response = true.ToString(), Info = "Pomyślnie wylogowano." }; // TODO : usuń token
        }
        public async Task<IEnumerable<AccountDTO>> BrowseAsync(int myId)
        {
            var users = await _userRepository.GetAllAsync(myId);
            return _mapper.Map<IEnumerable<Users>, IEnumerable<AccountDTO>>(users);
        }
        public async Task<ReturnResponse> ChangeEmailAsync(int userID, string email)
        {
            var user = await _userRepository.GetAsync(userID);
            if (user == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędny login" };
            }
            if (String.IsNullOrWhiteSpace(email))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Podano pusty adres e-mail." };
            }
            user.SetEmail(email);
            await _userRepository.UpdateAsync(user);
            return new ReturnResponse { Response = true.ToString(), Info = "Adres e-mail został zmieniony." };
        }

        public async Task<ReturnResponse> ChangePasswordAsync(int userID, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userID);
            if (user == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędny login." };
            }
            if (!Crypto.VerifyHashedPassword(user.Password, currentPassword))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędne aktualne hasło." };
            }
            user.SetPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return new ReturnResponse { Response = true.ToString(), Info = "Hasło zostało zmienione." };
        }

        public async Task<ReturnResponse> ChangePersonalDataAsync(int userID, string name, string surname, string city)
        {
            var user = await _userRepository.GetAsync(userID);
            if (user == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędny login." };
            }
            user.SetName(name);
            user.SetSurname(surname);
            user.SetCity(city);
            await _userRepository.UpdateAsync(user);
            return new ReturnResponse { Response = true.ToString(), Info = "Dane personalne zostały zmienione." };
        }

        public async Task<ReturnResponse> ChangeDescriptionAsync(int userID, string description)
        {
            var user = await _userRepository.GetAsync(userID);
            if (user == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędy login." };
            }
            user.SetDescription(description);
            await _userRepository.UpdateAsync(user);
            return new ReturnResponse { Response = true.ToString(), Info = "Opis został zmieniony" };
        }

        public async Task<ReturnResponse> RemoveAccountAsync(int userID)
        {
            var user = await _userRepository.GetAsync(userID);
            if (user == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędny login." };
            }
            user.DisactiveUser();
            await _userRepository.UpdateAsync(user);
            return new ReturnResponse { Response = true.ToString(), Error = "Opis został zmieniony." };
        }

        public async Task<ReturnResponse> GetAllUsers(int myId)
        {
            var users = await _userRepository.GetAllAsync(myId);
            List<ListUserDTO> listUsers = _mapper.Map<IEnumerable<Users>, List<ListUserDTO>>(users);
            return new ReturnResponse { Response = true.ToString(), Info = listUsers };
        }

        public async Task<ReturnResponse> GetAllUsers(int myId, string fragmentLogin)
        {
            var users = await _userRepository.GetAllAsync(myId, fragmentLogin);
            List<ListUserDTO> listUsers = _mapper.Map<IEnumerable<Users>, List<ListUserDTO>>(users);
            return new ReturnResponse { Response = true.ToString(), Info = listUsers };
        }

        public async Task<ReturnResponse> GetUserActivity(int userId)
        {
            var userActivity = await _userRepository.GetUserActivity(userId);
            List<UserActivity> listActivity = new List<UserActivity>();
            foreach (var item in userActivity)
                listActivity.Add(new UserActivity { EventId = item.Item1, EventName = item.Item2, Date = item.Item3 });
            return new ReturnResponse { Response = true.ToString(), Info = listActivity };
        }

        public async Task<ReturnResponse> GetMyActivity(int userId)
        {
            var userActivity = await _userRepository.GetMyActivity(userId);
            List<MyActivity> listActivity = new List<MyActivity>();
            foreach (var item in userActivity)
                listActivity.Add(new MyActivity { EventId = item.Item1, EventName = item.Item2, Date = item.Item3, IsAccepted = item.Item4 });
            return new ReturnResponse { Response = true.ToString(), Info = listActivity };
        }

        public async Task<ReturnResponse> GetAbonaments(int myId)
        {
            List<AbonamentDTO> listAbonaments = _mapper.Map<IEnumerable<Abonaments>, List<AbonamentDTO>>(await _userRepository.GetAbonaments(myId));
            return new ReturnResponse { Response = true.ToString(), Info = listAbonaments };
        }

        public async Task<ReturnResponse> GetLastAbonament(int myId)
        {
            AbonamentDTO abonament = _mapper.Map<Abonaments, AbonamentDTO>(await _userRepository.GetLastAbonament(myId));
            return new ReturnResponse { Response = true.ToString(), Info = abonament };
        }

        public ReturnResponse GetLink()
            => new ReturnResponse { Response = true.ToString(), Info = @"http://localhost:49556/" };



        private async Task CheckAndUpdateRoleUser(int myId)
        {
            Users user = await _userRepository.GetAsync(myId);
            Abonaments abonamet = await _userRepository.GetLastAbonament(myId);
            if(abonamet == null)
                return;

            if(user.Role == "biznes" && abonamet.DateEnd < DateTime.Now)
            {
                user.SetRole("uzytkownik");
                await _userRepository.UpdateAsync(user);
            }
        } 

    }
}