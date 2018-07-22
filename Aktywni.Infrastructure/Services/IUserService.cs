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
        Task<bool> LogoutAsync(int userID);
        Task<string> ChangeEmailAsync(int userID, string email);
        Task<string> ChangePasswordAsync(int userID, string currentPassword, string newPassword);
        Task<string> ChangePersonalDataAsync(int userID, string name, string surname, string city);
        Task<string> ChangeDescriptionAsync(int userID, string description);
        Task<string> RemoveAccountAsync(int userID);

    }
}