using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly AktywniDBContext _dbContext;
        public ObjectRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Objects> GetAsync(int objectID)
            => await _dbContext.Objects.Where(x => x.Visitability != Objects.TypeOfVisible.NIKT.ToString())
                                        .FirstOrDefaultAsync(x => x.ObjectId == objectID);

        public async Task<Objects> GetAsync(string objectName)
            => await _dbContext.Objects.Where(x => x.Visitability != Objects.TypeOfVisible.NIKT.ToString())
                                        .FirstOrDefaultAsync(x => x.Name == objectName);

        public async Task<IEnumerable<Objects>> GetAllAsync()
            => await _dbContext.Objects.Where(x => x.Visitability != Objects.TypeOfVisible.NIKT.ToString()).ToListAsync();

        public async Task<IEnumerable<Objects>> GetFromTextAsync(string textInput)
            => await _dbContext.Objects.Where(x => x.Visitability != Objects.TypeOfVisible.NIKT.ToString())
                                        .Where(x => x.Name.Contains(textInput)).ToListAsync();

        public async Task<IEnumerable<Objects>> GetObjectInCity(string name, string city)
            => await _dbContext.Objects.Where(x => x.Visitability != Objects.TypeOfVisible.NIKT.ToString()).
                                        Where(x => x.Name.Contains(name) && x.City.Contains(city)).ToListAsync();
        public async Task AddAsync(Objects obj)
        {
            _dbContext.Objects.Add(obj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Objects obj)
        {
            _dbContext.Objects.Update(obj);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int objectID)
        {
            Objects obj = await GetAsync(objectID);
            _dbContext.Objects.Remove(obj);
            await _dbContext.SaveChangesAsync();
        }
    }
}