using System;

namespace Dsw2025Tpi.Domain.Entities
{
    public class User : EntityBase
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        // Constructor requerido por EF
        private User() { }

        public User(string username, string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario es obligatorio");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña es obligatoria");

            if (role != "Admin" && role != "User")
                throw new ArgumentException("El rol debe ser 'Admin' o 'User'");


            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
