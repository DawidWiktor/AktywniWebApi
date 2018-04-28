using System;
using System.Threading.Tasks;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Extensions;
using Aktywni.Core.Model;
using Aktywni.Infrastructure.Services;
using AutoMapper;
using CryptoHelper;

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
           
            var user = await _userRepository.GetOrFailasync(userId);
           
            return _mapper.Map<AccountDTO>(user);
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
                throw new Exception("Błędy login lub błędne hasł1o.");
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

        
    }
}