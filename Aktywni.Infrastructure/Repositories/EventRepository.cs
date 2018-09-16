using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using GeoCoordinatePortable;
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

        public async Task<int> GetIdEvent(string eventName)
            => await _dbContext.Events.Where(x => x.Name == eventName)
                                      .Select(x => x.EventId)
                                      .FirstOrDefaultAsync();
        public async Task<IEnumerable<Events>> GetAllEventsAsync()
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                    .Where(x => x.Date > DateTime.Now)
                                    .OrderBy(x => x.Commerce).ToListAsync();
        public async Task<IEnumerable<Events>> GetAllMyEventsAsync(int userID)
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                    .Where(x => x.Admin == userID)
                                    .Where(x => x.WhoCreatedId == userID)
                                    .OrderBy(x => x.Commerce).ToListAsync();

        public async Task<IEnumerable<Events>> GetFromTextAsync(string textInput)
         => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                        .Where(x => x.Date > DateTime.Now)
                                        .Where(x => x.Name.Contains(textInput))
                                        .OrderBy(x => x.Commerce).ToListAsync();
        public async Task<IEnumerable<Events>> GetFromTextAndDisciplineAsync(string textInput, int disciplineID)
            => await _dbContext.Events.Where(x => x.Visibility != Events.TypeOfVisible.U.ToString())
                                        .Where(x => x.Date > DateTime.Now)
                                        .Where(x => x.Name.Contains(textInput))
                                        .Where(x => x.DisciplineId == disciplineID)
                                        .OrderBy(x => x.Commerce).ToListAsync();


        public async Task<IEnumerable<Events>> GetFromTextAndDisciplineAndDistanceAsync(string textInput, int disciplineID, double distance, double latitude, double longitude)
        {
            var coord = new GeoCoordinate(latitude, longitude);
            List<GeoCoordinate> nearestEvents = new List<GeoCoordinate>();
            if(string.IsNullOrEmpty(textInput))
                nearestEvents = await _dbContext.Events
                                   .Where(x => x.DisciplineId == disciplineID)
                                   .Select(x => new GeoCoordinate((double)x.Latitude, (double)x.Longitude))
                                   .OrderBy(x => x.GetDistanceTo(coord))
                                   .ToListAsync();
            else                       
                nearestEvents = await _dbContext.Events
                                   .Where(x => x.Name.Contains(textInput))
                                   .Where(x => x.DisciplineId == disciplineID)
                                   .Select(x => new GeoCoordinate((double)x.Latitude, (double)x.Longitude))
                                   .OrderBy(x => x.GetDistanceTo(coord))
                                   .ToListAsync();

            List<Events> events = new List<Events>();
            foreach (var item in nearestEvents)
            {
                if (coord.GetDistanceTo(new GeoCoordinate((double)item.Latitude, (double)item.Longitude)) / 1000 > distance)
                    continue;

                Events tempEvent = await _dbContext.Events.OrderByDescending(x => x.EventId)
                                                          .FirstOrDefaultAsync(x => (double)x.Latitude == item.Latitude
                                                                                && (double)x.Longitude == item.Longitude);
                events.Add(tempEvent);
            }
            return events;
        }

        public async Task<IEnumerable<Events>> GetNearestEvents(double latitude, double longitude)
        {
            var coord = new GeoCoordinate(latitude, longitude);
            var nearestEvents = await _dbContext.Events
                                   .Select(x => new GeoCoordinate((double)x.Latitude, (double)x.Longitude))
                                   .OrderBy(x => x.GetDistanceTo(coord))
                                   .Take(20)
                                   .ToListAsync();

            List<Events> events = new List<Events>();
            foreach (var item in nearestEvents)
            {
                Events tempEvent = await _dbContext.Events.OrderByDescending(x => x.EventId)
                                                          .FirstOrDefaultAsync(x => (double)x.Latitude == item.Latitude
                                                                                && (double)x.Longitude == item.Longitude);
                events.Add(tempEvent);
            }
            return events;
        }

        public async Task<IEnumerable<Events>> GetEventInDisciplineAsync(int disciplineID)
            => await _dbContext.Events.Where(x => x.DisciplineId == disciplineID)
                                      .ToListAsync();

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