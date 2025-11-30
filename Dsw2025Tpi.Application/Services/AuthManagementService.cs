using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dsw2025Tpi.Application.Services
{
    public class AuthManagementService
    {
        private readonly IRepository _repo;
        private readonly IConfiguration _config;

        public AuthManagementService(IRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // =============================
        // LOGIN
        // =============================
        public async Task<LoginResponse> LoginAsync(LoginRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Credenciales inválidas");

            var users = await _repo.GetAll<User>();
            var user = users?.FirstOrDefault(u => u.Username == dto.Username);

            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            if (user.Password != dto.Password)
                throw new UnauthorizedAccessException("Contraseña incorrecta");

            string token = GenerateToken(user);

            return new LoginResponse(user.Id, user.Username, user.Role, token);
        }

        // =============================
        // GET ALL USERS
        // =============================
        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _repo.GetAll<User>();

            if (users == null)
                return new List<UserResponse>();

            return users.Select(u =>
                new UserResponse(u.Id, u.Username, u.Role)
            );
        }

        // =============================
        // GET USER BY ID
        // =============================
        public async Task<UserResponse> GetUserByIdAsync(Guid id)
        {
            var u = await _repo.GetById<User>(id);

            if (u == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            return new UserResponse(u.Id, u.Username, u.Role);
        }

        // =============================
        // REGISTER
        // =============================
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("El usuario y contraseña son obligatorios");

            if (dto.Role != "Admin" && dto.Role != "User")
                throw new ArgumentException("El rol debe ser 'Admin' o 'User'");

            var existing = await _repo.First<User>(u => u.Username == dto.Username);
            if (existing != null)
                throw new ArgumentException("El nombre de usuario ya existe");

            var user = new User(dto.Username, dto.Password, dto.Role);
            await _repo.Add<User>(user);

            return new RegisterResponse(user.Id, user.Username, user.Role);
        }

        // =============================
        // TOKEN GENERATION
        // =============================
        private string GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
