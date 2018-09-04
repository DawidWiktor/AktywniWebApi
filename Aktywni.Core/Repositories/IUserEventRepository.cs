using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UsersEvents>> GetUsersInEvent(int eventId);
        Task<IEnumerable<UsersEvents>> GetUsersInEvent(string eventName);
        Task<UsersEvents> GetUserInEvent(int eventId, int userId);  // uzyskanie UsersEvents z danego wydarenia, aby usunąć uczestnictwo użytkownika
        Task<bool> CheckUserInEvent(int eventId, int userId);
        Task<IEnumerable<Tuple<int, string, DateTime>>> GetEventsInUser(int userId); // uzyskanie listy wydarzeń gdzie należy użytkownik, id wydarzenia, nazwa wydarzenia, data wydarzenia
        Task AddAsync(UsersEvents userEvent);
        Task UpdateAsync(UsersEvents userEvent);
        Task DeleteAsync(UsersEvents userEvent);
    }
}