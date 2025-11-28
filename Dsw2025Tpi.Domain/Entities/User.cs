using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class User : EntityBase
    {
        private User() { }

        public User (string username, string email, string passwordHash, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username vacío", nameof(username));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email vacío", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password inválido", nameof(passwordHash));

            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
    }
}
