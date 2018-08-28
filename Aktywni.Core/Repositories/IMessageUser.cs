using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IMessageUser
    {
        Task<MessageUser> GetAsync(int myId, int Id);
        Task<MessageUser> GetAsync(string myLogin, string friendLogin);
        Task<IEnumerable<MessageUser>> GetAllAsync(int myId);
        Task<IEnumerable<MessageUser>> GetFromTextAsync(int myId, string textInput);
        Task AddAsync(MessageUser user);
        Task UpdateAsync(MessageUser user);
        Task DeleteAsync(MessageUser friend);
    }
}