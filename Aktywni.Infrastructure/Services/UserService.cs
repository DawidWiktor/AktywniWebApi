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

        public async Task RegisterAsync(string login, string email, string password)
        {
            var user = await _userRepository.GetAsync(login);
            if(user != null)
            {
                throw new Exception($"Użytkownik o {login} już istnieje.");
            }
            user = new Users(login, email, password);
            await _userRepository.AddAsync(user);
            
        }

        public async Task<TokenDTO> LoginAsync(string login, string password)
        { 
            var user = await _userRepository.GetAsync(login);
            if(user == null)
            {
                throw new Exception("Błędy login lub błędne hasło.");
            }
            if(!(bool)user.IsActive)
            {
                throw new Exception("Błędy login lub błędne hasło.");
            }
            if(!Crypto.VerifyHashedPassword(user.Password, password))
            {
                throw new Exception("Błędy login lub błędne hasło.");
            }
            var jwt = _jwtHandler.CreateToken(user.UserId, user.Role);
            
            return new TokenDTO
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }

        public async Task<IEnumerable<AccountDTO>> BrowseAsync(int userID)
        {
            var users = await _userRepository.GetAllAsync();    
            return _mapper.Map<IEnumerable<Users>, IEnumerable<AccountDTO>>(users);
        }
        public async Task ChangeEmailAsync(int userID, string email)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                throw new Exception("Błędy login.");
            }
            if(String.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Podano pusty adres e-mail.");
            }
            user.SetEmail(email);
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(int userID, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                throw new Exception("Błędy login.");
            }
             if(!Crypto.VerifyHashedPassword(user.Password, currentPassword))
            {
                throw new Exception("Błędne hasło.");
            }
            user.SetPassword(Crypto.HashPassword(newPassword));
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePersonalDataAsync(int userID, string name, string surname, string city)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                throw new Exception("Błędy login.");
            }
            user.SetName(name);
            user.SetSurname(surname);
            user.SetCity(city);
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeDescriptionAsync(int userID, string description)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                throw new Exception("Błędy login.");
            }
            user.SetDescription(description);
            await _userRepository.UpdateAsync(user);
        }

        public async Task RemoveAccountAsync(int userID)
        {
            var user = await _userRepository.GetAsync(userID);
            if(user == null)
            {
                throw new Exception("Błędy login.");
            }
            user.DisactiveUser();
            await _userRepository.UpdateAsync(user);
        }
    }
}