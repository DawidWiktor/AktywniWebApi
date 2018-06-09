using System;
using System.Threading.Tasks;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDTO> GetAccountAsync(int userID);
        Task<bool> RegisterAsync(string login, string email, string password);
        Task<TokenDTO> LoginAsync(string login, string password);
        Task ChangeEmailAsync(int userID, string email);
        Task ChangePasswordAsync(int userID, string currentPassword, string newPassword);
        Task ChangePersonalDataAsync(int userID, string name, string surname, string city);
        Task ChangeDescriptionAsync(int userID, string description);
        Task RemoveAccountAsync(int userID);

    }
}