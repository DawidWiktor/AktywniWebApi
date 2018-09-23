using System;
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

        public async Task<UsersEvents> GetUserInEvent(int eventId, int userId)
            => await _dbContext.UsersEvents.Where(x => x.EventId == eventId)
                                        .Where(x => x.UserId == userId).FirstOrDefaultAsync();
        public async Task<bool> CheckUserInEvent(int eventId, int userId) // sprawdzenie czy użytkownik należy do wydarzenia
             => await _dbContext.UsersEvents.Where(x => x.EventId == eventId)
                                        .Where(x => x.UserId == userId).AnyAsync();

        // id wydarzenia, nazwa wydarzenia, data
        public async Task<IEnumerable<Tuple<int, string, DateTime, bool>>> GetEventsInUser(int myId)
            => await _dbContext.UsersEvents.Where(x => x.UserId == myId)
                                    .Select(z => new Tuple<int, string, DateTime, bool>(z.EventId,
                                                            z.Event.Name, (DateTime)z.Event.Date, (bool)z.IsAccepted))
                                    .ToListAsync();

        // id wydarzenia, nazwa wydarzenia, data
        public async Task<IEnumerable<Tuple<int, string, DateTime>>> GetMyInvitationsEvent(int myId)
            => await _dbContext.UsersEvents.Where(x => x.UserId == myId)
                                            .Where(x => x.IsAccepted == false)
                                    .Select(z => new Tuple<int, string, DateTime>(z.EventId,
                                                            z.Event.Name, (DateTime)z.Event.Date))
                                    .ToListAsync();

        public async Task<IEnumerable<Tuple<int, string, DateTime, decimal, decimal, int, string>>> GetHistoryEvents(int myId)
            => await _dbContext.UsersEvents.Where(x => x.UserId == myId)
                                            .Where(x => x.IsAccepted == true)
                                            .Where(x => x.Event.Date < DateTime.Now)
                                    .Select(z => new Tuple<int, string, DateTime, decimal, decimal, int, string>(z.EventId,
                                                            z.Event.Name, (DateTime)z.Event.Date, (decimal)z.Event.Latitude, (decimal)z.Event.Longitude, (int)z.Event.DisciplineId, z.Event.Description))
                                    .ToListAsync();

        public async Task<bool> IsAdminInEvent(int eventId, int userId)
            => await _dbContext.Events.Where(x => x.EventId == eventId)
                                      .AnyAsync(x => x.Admin == userId);

        public async Task<bool> IsUserInEvent(int eventId, int userId)
            => await _dbContext.UsersEvents.Where(x => x.EventId == eventId)
                                           .Where(x => x.UserId == userId)
                                           .AnyAsync(x => x.IsAccepted == true);
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