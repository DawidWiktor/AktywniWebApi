using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserCommentService
    {
        Task<ReturnResponse> GetUserComments(int userId);
        Task<ReturnResponse> GetMyComments(int myId);
        Task<ReturnResponse> AddComment(int myId, int userIdRated, int eventId, int rate, string describe);
        Task<ReturnResponse> UpdateComment(int myId, int userIdRated, int eventId, int rate, string describe);
        Task<ReturnResponse> RemoveComment(int myId, int userIdRated, int eventId);
    }
}