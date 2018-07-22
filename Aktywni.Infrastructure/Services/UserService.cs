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

        public async Task<bool> RegisterAsync(string login, string email, string password)
        {
            var user = await _userRepository.GetAsync(login);
            if(user != null)
            {
               return false;
            }
            user = new Users(login, email, password);
            await _userRepository.AddAsync(user);
            return true;    
        }

        public async Task<TokenDTO> LoginAsync(string login, string password)
        { 
            var user = await _userRepository.GetAsync(login);
            if(user == null)
            {
                return null;
               // throw new Exception("Błędy login lub błędne hasło.");
            }
            if(!(bool)user.IsActive)
            {
                return null;
                //throw new Exception("Błędy login lub błędne hasło.");
            }
            if(!Crypto.VerifyHashedPassword(user.Password, password))
            {
                return null;
                //throw new Exception("Błędy login lub błędne hasło.");
            }
            var jwt = _jwtHandler.CreateToken(user.UserId, user.Role);
            
            return new TokenDTO
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }

        public async Task<bool> LogoutAsync(int userID)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                return false;
            }
            return true; // TODO : usuń token
        }
        public async Task<IEnumerable<AccountDTO>> BrowseAsync(int userID)
        {
            var users = await _userRepository.GetAllAsync();    
            return _mapper.Map<IEnumerable<Users>, IEnumerable<AccountDTO>>(users);
        }
        public async Task<string> ChangeEmailAsync(int userID, string email)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                return "Błędny login";
            }
            if(String.IsNullOrWhiteSpace(email))
            {
               return "Podano pusty adres e-mail.";
            }
            user.SetEmail(email);
            await _userRepository.UpdateAsync(user);
            return "True";
        }

        public async Task<string> ChangePasswordAsync(int userID, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                return "Błędy login.";
            }
             if(!Crypto.VerifyHashedPassword(user.Password, currentPassword))
            {
                return "Błędne hasło.";
            }
            user.SetPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return "True";
        }

        public async Task<string> ChangePersonalDataAsync(int userID, string name, string surname, string city)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                return "Błędy login.";
            }
            user.SetName(name);
            user.SetSurname(surname);
            user.SetCity(city);
            await _userRepository.UpdateAsync(user);
            return "True";
        }

        public async Task<string> ChangeDescriptionAsync(int userID, string description)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
              return "Błędy login.";
            }
            user.SetDescription(description);
            await _userRepository.UpdateAsync(user);
            return "True";
        }

        public async Task<string> RemoveAccountAsync(int userID)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                return "Błędy login.";
            }
            user.DisactiveUser();
            await _userRepository.UpdateAsync(user);
            return "True";
        }
    }
}