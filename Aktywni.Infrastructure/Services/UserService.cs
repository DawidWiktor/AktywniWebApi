using System;
using System.Threading.Tasks;
using Aktywni.Core.Domain;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Services;

namespace Aktywni.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        async Task IUserService.RegisterAsync(Guid Id, string login, string email, string password)
        {
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
                throw new Exception("Błędy login lub błędne hasło.");
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