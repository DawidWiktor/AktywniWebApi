using System;
using System.Threading.Tasks;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDTO> GetAccountAsync(Guid userID);
        Task RegisterAsync(Guid Id, string login, string email, string password);
        Task<TokenDTO> LoginAsync(string login, string password);
    }
}