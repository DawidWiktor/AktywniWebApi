using System;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDTO CreateToken(Guid userID, string role);
    }
}