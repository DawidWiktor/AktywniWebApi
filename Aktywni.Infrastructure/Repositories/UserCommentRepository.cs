using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class UserCommentRepository : IUserCommentRepository
    {
        private readonly AktywniDBContext _dbContext;
        public UserCommentRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

         public async Task<IEnumerable<Tuple<int, string, DateTime, bool>>> GetEventsInUser(int myId)
            => await _dbContext.UsersEvents.Where(x => x.UserId == myId)
                                    .Select(z => new Tuple<int, string, DateTime, bool>(z.EventId, 
                                                            z.Event.Name, (DateTime)z.Event.Date, (bool)z.IsAccepted))
                                    .ToListAsync();

        // id uzytkownika, który oceniał, jego login, id użytkownika ocenianego, jego login, ocena, opis
        public async Task<IEnumerable<Tuple<int, string, int, string, int, string>>> GetUserComments(int userId)
            => await _dbContext.UserComments.Where(x => x.UserIdRated == userId)
                                            .Select(z=> new Tuple<int,string,int,string,int,string>
                                            (z.UserIdWhoComment, z.UserIdWhoCommentNavigation.Login, z.UserIdRated, z.UserIdRatedNavigation.Login,
                                            z.Rate, z.Describe))
                                            .ToListAsync();

        public async Task<IEnumerable<UserComments>> GetMyComments(int myId)
            => await _dbContext.UserComments.Where(x => x.UserIdWhoComment == myId)
                                            .ToListAsync();

        public async Task AddAsync(UserComments userComments)
        {
            _dbContext.UserComments.Add(userComments);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserComments userComments)
        {
            _dbContext.UserComments.Update(userComments);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserComments userComments)
        {
            _dbContext.UserComments.Remove(userComments);
            await _dbContext.SaveChangesAsync();
        }
    }
}