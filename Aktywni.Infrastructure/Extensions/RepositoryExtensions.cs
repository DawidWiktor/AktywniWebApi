using System;
using System.Threading.Tasks;
using Aktywni.Core.Domain;
using Aktywni.Core.Repositories;

namespace Aktywni.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailasync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if(user == null)
            {
                throw new Exception($"Użytkownik o id: {id} nie istnieje");
            }
            return user;
        }
    }
}