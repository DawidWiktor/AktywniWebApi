using System.Threading.Tasks;
using Aktywni.Infrastructure.Commands;

namespace Aktywni.Infrastructure.Services
{
    public interface IUserCommentService
    {
        Task<ReturnResponse> GetUserComments(int userId);
        Task<ReturnResponse> GetMyComments(int myId);
        Task<ReturnResponse> AddComment(int myId, int userIdRated, int rate, int describe);
        Task<ReturnResponse> UpdateComment(int myId, int userIdRated, int rate, int describe);
        Task<ReturnResponse> RemoveComment(int myId, int userIdRated);
    }
}