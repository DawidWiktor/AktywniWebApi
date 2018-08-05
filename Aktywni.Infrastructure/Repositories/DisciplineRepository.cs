using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aktywni.Infrastructure.Repositories
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly AktywniDBContext _dbContext;
        public DisciplineRepository(AktywniDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Disciplines> GetAsync(int Id)
            => await _dbContext.Disciplines.FirstOrDefaultAsync(x => x.DisciplineId == Id);

        public async Task<Disciplines> GetAsync(string name)
            => await _dbContext.Disciplines.FirstOrDefaultAsync(x => x.Name == name);

        public async Task<IEnumerable<Disciplines>> GetAllAsync()
            => await _dbContext.Disciplines.ToListAsync();

        public async Task<IEnumerable<Disciplines>> GetFromTextAsync(string textInput)
            => await _dbContext.Disciplines.Where(x => x.Name.Contains(textInput)).ToListAsync();

        public async Task AddAsync(Disciplines discipline)
        {
            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Disciplines discipline)
        {
            _dbContext.Disciplines.Update(discipline);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Disciplines discipline)
        {
            throw new System.NotImplementedException();
        }
    }
}