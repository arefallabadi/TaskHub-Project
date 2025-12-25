using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskHub.API.Data;
using TaskHub.API.DTOs.Auth;
using TaskHub.API.Services.Interfaces;

namespace TaskHub.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly TaskHubDbContext _context;

        public AuthService(IConfiguration configuration, TaskHubDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public bool ValidateSystemCredentials(string username, string password)
        {
            var sysUser = _configuration["SystemAuth:Username"];
            var sysPass = _configuration["SystemAuth:Password"];

            return username == sysUser && password == sysPass;
        }

        public string? GenerateToken(string username, string role, int userId)
        {
            var key = _configuration["JwtKey"];
            if (string.IsNullOrEmpty(key)) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthResponseDto? Login(LoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var token = GenerateToken(user.Username, user.Role.Name, user.Id);
            if (token == null) return null;

            return new AuthResponseDto
            {
                Token = token,
                Role = user.Role.Name
            };
        }

        public AuthResponseDto? Register(RegisterDto dto)
        {
            // Check if username already exists
            if (_context.Users.Any(u => u.Username == dto.Username))
                return null;

            // Get User role (query through Users to find existing role, or use Set<Role>)
            Entities.Role? role = null;
            var existingUserWithUserRole = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Role.Name == "User");
            
            if (existingUserWithUserRole != null && existingUserWithUserRole.Role != null)
            {
                role = existingUserWithUserRole.Role;
            }
            else
            {
                // Try to find role using Set<Role> (works even if not in DbContext as DbSet)
                role = _context.Set<Entities.Role>()
                    .FirstOrDefault(r => r.Name == "User");
                
                if (role == null)
                {
                    // Role doesn't exist, create it
                    role = new Entities.Role { Name = "User" };
                    _context.Set<Entities.Role>().Add(role);
                    _context.SaveChanges();
                }
            }

            var user = new Entities.User
            {
                Username = dto.Username,
                Name = dto.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = role.Id
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Reload user with role to get role name
            _context.Entry(user).Reference(u => u.Role).Load();
            var roleName = user.Role?.Name ?? "User";

            var token = GenerateToken(user.Username, roleName, user.Id);
            if (token == null) return null;

            return new AuthResponseDto
            {
                Token = token,
                Role = roleName
            };
        }
    }
}
