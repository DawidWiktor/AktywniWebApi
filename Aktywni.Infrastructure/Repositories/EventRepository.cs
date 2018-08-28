using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AktywniDBContext _dbContext;
        public EventRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Events> GetEventAsync(int eventID)
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                    .FirstOrDefaultAsync(x => x.EventId == eventID);

        public async Task<Events> GetEventAsync(string eventName)
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                    .FirstOrDefaultAsync(x => x.Name == eventName);

        public async Task<IEnumerable<Events>> GetAllEventsAsync()
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                    .Where(x=>x.Date > DateTime.Now)
                                    .OrderBy(x=>x.Commerce).ToListAsync();
        public async Task<IEnumerable<Events>> GetAllMyEventsAsync(int userID)
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                    .Where(x=>x.Admin == userID)
                                    .Where(x => x.WhoCreatedId == userID)
                                    .OrderBy(x=>x.Commerce).ToListAsync();

        public async Task<IEnumerable<Events>> GetFromTextAsync(string textInput)
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                        .Where(x=>x.Date > DateTime.Now)
                                        .Where(x => x.Name.Contains(textInput))
                                        .OrderBy(x=>x.Commerce).ToListAsync();
        public async Task<IEnumerable<Events>> GetFromTextAndDisciplineAsync(string textInput, int disciplineID)
            => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                        .Where(x=>x.Date > DateTime.Now)
                                        .Where(x => x.Name.Contains(textInput))
                                        .Where(x => x.DisciplineId == disciplineID)
                                        .OrderBy(x=>x.Commerce).ToListAsync();


        public async Task<IEnumerable<Events>> GetFromTextAndDisciplineAndDistanceAsync(string textInput, int disciplineID, double distance)
        {
            throw new System.NotImplementedException();
            //TODO: ogarnac wyliczanie odleglosci
        }

        public async Task AddAsync(Events obj)
        {
            _dbContext.Events.Add(obj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Events obj)
        {
            _dbContext.Events.Update(obj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int eventID)
        {
            Events obj = await GetEventAsync(eventID);
            _dbContext.Events.Remove(obj);
            await _dbContext.SaveChangesAsync();
        }
    }
}