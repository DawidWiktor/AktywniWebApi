using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IDisciplineRepository
    {
         Task<Disciplines> GetAsync(int Id);
         Task<Disciplines> GetAsync(string name);
         Task<IEnumerable<Disciplines>> GetAllAsync(); 
         Task<IEnumerable<Disciplines>> GetFromTextAsync(string textInput); 
         Task AddAsync(Disciplines discipline);
         Task UpdateAsync(Disciplines discipline);
        
    }
}