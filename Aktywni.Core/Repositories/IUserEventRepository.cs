using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UsersEvents>> GetUsersInEvent(int eventId);
         Task<IEnumerable<UsersEvents>> GetUsersInEvent(string eventName); 
         Task AddAsync(UsersEvents userEvent);
         Task UpdateAsync(UsersEvents userEvent);
         Task DeleteAsync(UsersEvents userEvent);
    }
}