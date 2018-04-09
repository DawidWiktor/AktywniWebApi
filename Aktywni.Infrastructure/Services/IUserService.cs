using System;
using System.Threading.Tasks;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid Id, string login, string email, string password);
        Task LoginRegister(string login, string password);
    }
}