using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IFriendRepository
    {
         Task<Friends> GetAsync(int myId, int Id);
         Task<Friends> GetAsync(string myLogin, string friendLogin);
         Task<IEnumerable<Friends>> GetInvitations(int myId); // uzyskanie listy zaproszeń
         Task<IEnumerable<Friends>> GetAllAsync(int myId); 
         Task<IEnumerable<Friends>> GetFromTextAsync(int myId, string textInput); 
         Task AddAsync(Friends user);
         Task UpdateAsync(Friends user);
         Task DeleteAsync(Friends friend);
    }
}