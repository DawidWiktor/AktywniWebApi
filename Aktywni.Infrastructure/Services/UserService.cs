using System;
using System.Threading.Tasks;
using Aktywni.Core.Domain;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Extensions;
using Aktywni.Infrastructure.Model;
using Aktywni.Infrastructure.Services;
using AutoMapper;

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

        public async Task<AccountDTO> GetAccountAsync(Guid userId)
        {
            System.Diagnostics.Debug.WriteLine("userid " + userId);
            var user = await _userRepository.GetOrFailasync(userId);
            System.Diagnostics.Debug.WriteLine("login " + user.Login);
            return _mapper.Map<AccountDTO>(user);
        }

        async Task IUserService.RegisterAsync(Guid Id, string login, string email, string password)
        {
            using(var db = new AktywniDBContext())
            {
                var usr = db.Users;
                foreach (Users item in usr)
                    System.Diagnostics.Debug.WriteLine(item.Name + " " + item.UserId);
            }
            var user = await _userRepository.GetAsync(login);
            if(user != null)
            {
                throw new Exception($"Użytkownik o {login} już istnieje.");
            }
            user = new User(Id, login, "uzytkownik", email, password, "salt");
            await _userRepository.AddAsync(user);
            
        }

        public async Task<TokenDTO> LoginAsync(string login, string password)
        { 
            
            
            var user = await _userRepository.GetAsync(login);
            if(user == null)
            {
                throw new Exception("Błędy login lub błędne hasł1o.");
            }
            if(user.Password != password)
            {
                throw new Exception("Błędy login lub błędne hasło.");
            }
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            
            return new TokenDTO
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }

        
    }
}