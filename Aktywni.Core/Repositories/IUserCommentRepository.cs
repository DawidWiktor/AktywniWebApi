using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;

namespace Aktywni.Core.Repositories
{
    public interface IUserCommentRepository
    {
        Task<UserComments> GetComment(int myId, int userId);
        // id uzytkownika, który oceniał, jego login, id użytkownika ocenianego, jego login, ocena, opis
        Task<List<Tuple<int, string, int, string, int, string>>> GetUserComments(int userId);
         // id uzytkownika, który oceniał, jego login, id użytkownika ocenianego, jego login, ocena, opis
        Task<List<Tuple<int, string, int, string, int, string>>> GetMyComments(int myId);
        Task AddAsync(UserComments UserComments);     
        Task UpdateAsync(UserComments UserComments);  
        Task DeleteAsync(UserComments UserComments);     
    }
}