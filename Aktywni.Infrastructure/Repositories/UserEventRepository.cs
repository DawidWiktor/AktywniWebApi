using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
         private readonly AktywniDBContext _dbContext;
        public UserEventRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<UsersEvents>> GetUsersInEvent(int eventId)
            => await _dbContext.UsersEvents.Where(x => x.EventId == eventId).ToListAsync();

        public async Task<IEnumerable<UsersEvents>> GetUsersInEvent(string eventName)
            => await _dbContext.UsersEvents.Where(x => x.Event.Name == eventName).ToListAsync();
        
        public async Task AddAsync(UsersEvents userEvent)
        {
            _dbContext.UsersEvents.Add(userEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsersEvents userEvent)
        {
            _dbContext.UsersEvents.Update(userEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(UsersEvents userEvent)
        {
            _dbContext.UsersEvents.Remove(userEvent);
            await _dbContext.SaveChangesAsync();
        }
    }
}