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
            return _mapper.Map<Users, AccountDTO>(user);
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
            return new ReturnResponse { Response = (listUsers.Count == 0) ? false.ToString() : true.ToString(), Info = listUsers };
        }

        public async Task<ReturnResponse> GetAllUsers(int myId, string fragmentLogin)
        {
            var users = await _userRepository.GetAllAsync(myId, fragmentLogin);
            List<ListUserDTO> listUsers = _mapper.Map<IEnumerable<Users>, List<ListUserDTO>>(users);
            return new ReturnResponse { Response = (listUsers.Count == 0) ? false.ToString() : true.ToString(), Info = listUsers };
        }
    }
}