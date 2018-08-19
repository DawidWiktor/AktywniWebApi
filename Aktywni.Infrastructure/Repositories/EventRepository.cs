using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;

namespace Aktywni.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AktywniDBContext _dbContext;
        public EventRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public Task<Events> GetEventAsync(int eventID)
        {
            throw new System.NotImplementedException();
        }

        public Task<Events> GetEventAsync(string eventName)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Events>> GetAllEventsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Events>> GetAllMyEventsAsync(int userID)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Events>> GetFromTextAsync(string textInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Events>> GetFromTextAndDisciplineAsync(string textInput, int disciplineID)
        {
            throw new System.NotImplementedException();
        }


        public Task<IEnumerable<Events>> GetFromTextAndDisciplineAndDistanceAsync(string textInput, int disciplineID, double distance)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(Events obj)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Events obj)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int eventID)
        {
            throw new System.NotImplementedException();
        }
    }
}