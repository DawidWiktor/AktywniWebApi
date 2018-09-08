using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetAsync(int Id);       // pobierz użytkownika o danym Id
        Task<Users> GetAsync(string login); // pobierz użytkownika o danym loginie
        Task<IEnumerable<Users>> GetAllAsync(int myId);  // pobierz wszystkich użytkowników
        Task<IEnumerable<Users>> GetAllAsync(int myId, string fragmentLogin); // pobierz użytkowników, którzy w loginie mają fragmentLogin
        Task<string> GetLogin(int userId); // uzyskanie loginu
        Task AddAsync(Users user);     // dodaj użytkownika
        Task UpdateAsync(Users user);  // zaktualizuj danego użytkownika
        Task DeleteAsync(int Id);      // dezaktywuj użytkownika
    }
}