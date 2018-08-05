using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly AktywniDBContext _dbContext;
        public FriendRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Friends> GetAsync(int myId, int FriendId)
            => await _dbContext.Friends.FirstOrDefaultAsync(x => (x.FriendFrom == myId && x.FriendTo == FriendId) || (x.FriendFrom == FriendId && x.FriendTo == myId));

        public async Task<Friends> GetAsync(string myLogin, string friendLogin)
        {
            throw new System.NotImplementedException();
            //   => await _dbContext.Friends.SingleOrDefaultAsync(x => x.Login == login);
        }

        public async Task<IEnumerable<Friends>> GetAllAsync(int myId)
            => await _dbContext.Friends.Where(x => x.FriendTo == myId || x.FriendFrom == myId).ToListAsync();

        public async Task<IEnumerable<Friends>> GetFromTextAsync(int myId, string textInput)
        {
            //TODO: var friends = _dbContext.Friends.Where(x=> x.FriendTo == myId || x.FriendFrom == myId).Where()
            throw new System.NotImplementedException();
        }
        public async Task AddAsync(Friends friend)
        {
            _dbContext.Friends.Add(friend);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Friends friend)
        {
            _dbContext.Friends.Update(friend);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Friends friend)
        {
            _dbContext.Friends.Remove(friend);
            await _dbContext.SaveChangesAsync();
        }
    }
}