using System;

namespace Aktywni.Infrastructure.Commands.User
{
    public class Register
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}