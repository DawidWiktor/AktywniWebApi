using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Repositories;
using Aktywni.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AktywniDBContext _dbContext;
        public UserRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Users> GetAsync(int Id)
          => await _dbContext.Users.SingleOrDefaultAsync(x => x.UserId == Id);

        public async Task<Users> GetAsync(string login)
           => await _dbContext.Users.SingleOrDefaultAsync(x => x.Login == login);

        public async Task<IEnumerable<Users>> GetAllAsync()
            => await _dbContext.Users.ToListAsync();

        public async Task<IEnumerable<Users>> GetAllAsync(string fragmentLogin)
            => await _dbContext.Users.Where(x => x.Login.Contains(fragmentLogin)).ToListAsync();

        public async Task AddAsync(Users user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int Id)
        {
            Users user = await GetAsync(Id);
            user.ActiveUser();
            await _dbContext.SaveChangesAsync();
        }
    }
}