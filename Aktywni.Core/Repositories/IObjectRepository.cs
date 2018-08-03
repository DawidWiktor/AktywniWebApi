using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IObjectRepository
    {
         Task<Objects> GetAsync(int objectID);
         Task<Objects> GetAsync(string objectName);
         Task<IEnumerable<Objects>> GetAllAsync(); 
         Task<IEnumerable<Objects>> GetFromTextAsync(string textInput); 
         Task<IEnumerable<Objects>> GetObjectInCity(string name, string city);
         Task AddAsync(Objects obj);
         Task UpdateAsync(Objects obj);
         Task DeleteAsync(int objectID);
    }
}