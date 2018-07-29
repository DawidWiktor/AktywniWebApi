using System;
using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDTO> GetAccountAsync(int userID);
        Task<ReturnResponse> RegisterAsync(string login, string email, string password);
        Task<ReturnResponse> LoginAsync(string login, string password);
        Task<ReturnResponse> LogoutAsync(int userID);
        Task<ReturnResponse> ChangeEmailAsync(int userID, string email);
        Task<ReturnResponse> ChangePasswordAsync(int userID, string currentPassword, string newPassword);
        Task<ReturnResponse> ChangePersonalDataAsync(int userID, string name, string surname, string city);
        Task<ReturnResponse> ChangeDescriptionAsync(int userID, string description);
        Task<ReturnResponse> RemoveAccountAsync(int userID);

    }
}