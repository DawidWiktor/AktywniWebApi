using System;
using System.Threading.Tasks;
using Aktywni.Core.Domain;

namespace Aktywni.Core.Repositories
{
    public interface IUserRepository
    {
         Task<User> GetAsync(Guid Id);
         Task<User> GetAsync(string login); 
         Task AddAsync(User user);
         Task UpdateAsync(User user);
         Task DeleteAsync(User user);
    }
}