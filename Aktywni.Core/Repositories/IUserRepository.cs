using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IUserRepository
    {
         Task<Users> GetAsync(int Id);
         Task<Users> GetAsync(string login);
         Task<IEnumerable<Users>> GetAllAsync(); 
         Task AddAsync(Users user);
         Task UpdateAsync(Users user);
         Task DeleteAsync(int Id);
    }
}