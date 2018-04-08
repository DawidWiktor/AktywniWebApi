using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aktywni.Core.Domain;
using Aktywni.Core.Repositories;

namespace Aktywni.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>();
          public async Task<User> GetAsync(Guid Id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == Id));

        public async Task<User> GetAsync(string login)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Login == login));

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }

      

    }
}