using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Repositories;
using Aktywni.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
        public async Task<IEnumerable<Users>> GetAllAsync(int myId)
            => await _dbContext.Users.Where(x=>x.IsActive == true)
                                     .Where(x=>x.UserId != myId)
                                     .ToListAsync();

        public async Task<IEnumerable<Users>> GetAllAsync(int myId, string fragmentLogin)
            => await _dbContext.Users.Where(x => x.Login.Contains(fragmentLogin))
                                      .Where(x=>x.IsActive == true)
                                      .Where(x=>x.UserId != myId)
                                     .ToListAsync();

        public async Task<IEnumerable<Tuple<int, string, DateTime>>> GetUserActivity(int userId)
            => await _dbContext.UsersEvents.Where(x=>x.UserId == userId)
                                           .Where(x=>x.IsAccepted == true)
                                           .Select(z => new Tuple<int, string, DateTime>(z.EventId, 
                                                            z.Event.Name, (DateTime)z.Event.Date))
                                           .ToListAsync();

        public async Task<IEnumerable<Tuple<int, string, DateTime, bool>>> GetMyActivity(int myId)
            => await _dbContext.UsersEvents.Where(x=>x.UserId == myId)
                                           .Select(z => new Tuple<int, string, DateTime, bool>(z.EventId, 
                                                            z.Event.Name, (DateTime)z.Event.Date, (bool)z.IsAccepted))
                                           .ToListAsync();


        public async Task<string> GetLogin(int userId)
            => await _dbContext.Users.Where(x=>x.UserId == userId)
                                     .Select(x =>x.Login)
                                     .FirstOrDefaultAsync();

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