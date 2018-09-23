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
        Task<UsersEvents> GetUserInEvent(int eventId, int userId);  // uzyskanie UsersEvents z danego wydarzenia, aby usunąć uczestnictwo użytkownika
        Task<bool> CheckUserInEvent(int eventId, int userId);

        // id wydarzenia, nazwa wydarzenia, data, czy zaakceptowano uczestnictwo
        Task<IEnumerable<Tuple<int, string, DateTime, bool>>> GetEventsInUser(int MyId); // uzyskanie listy wydarzeń gdzie należy użytkownik, id wydarzenia, nazwa wydarzenia, data wydarzenia
        Task<IEnumerable<Tuple<int, string, DateTime>>> GetMyInvitationsEvent(int myId); // uzyskanie wydarzeń gdzie jesteśmy zaproszeni, id wydarzenia, nazwa wydarzenia, data wydarzenia
        Task<IEnumerable<Tuple<int, string, DateTime, decimal, decimal>>> GetHistoryEvents(int myId); // uzyskanie wydarzeń zakończonych, gdzie braliśmy udziasł, latitude, longitude,
        Task<bool> IsAdminInEvent(int eventId, int userId); // sprawdzenie czy użytkownik jest adminem wydarzenia
        Task<bool> IsUserInEvent(int eventId, int userId); // sprawdzenie czy użytkownik brał udział w wydarzeniu
        Task AddAsync(UsersEvents userEvent);
        Task UpdateAsync(UsersEvents userEvent);
        Task DeleteAsync(UsersEvents userEvent);
    }
}