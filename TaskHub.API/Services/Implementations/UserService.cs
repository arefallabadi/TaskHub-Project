using TaskHub.API.DTOs.Pagination;
using TaskHub.API.DTOs.User;
using TaskHub.API.Entities;
using TaskHub.API.Repositories;
using TaskHub.API.Services.Interfaces;

namespace TaskHub.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public UserService(
            IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public List<UserDto> GetAll(PaginationParams pagination)
        {
            return _userRepository.GetAll()
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Name = u.Name,
                    Role = u.Role.Name
                })
                .ToList();
        }

        public UserDto? GetById(int id, int currentUserId, string currentUserRole)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return null;

            // User role: can only view their own profile
            if (currentUserRole != "Admin" && user.Id != currentUserId)
            {
                throw new System.UnauthorizedAccessException("You do not have permission to view this user.");
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                Role = user.Role.Name
            };
        }

        public void Create(CreateUserDto dto)
        {
            var role = _roleRepository.GetAll()
                .FirstOrDefault(r => r.Name == dto.Role);

            if (role == null)
                throw new Exception("Role not found");

            var user = new User
            {
                Username = dto.Username,
                Name = dto.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = role.Id
            };

            _userRepository.Add(user);
        }

        public bool Update(int id, UpdateUserDto dto, int currentUserId, string currentUserRole)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return false;

            // User role: can only update their own profile
            if (currentUserRole != "Admin" && user.Id != currentUserId)
            {
                throw new System.UnauthorizedAccessException("You do not have permission to update this user.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
                user.Name = dto.Name;

            // Only Admin can change roles, and cannot change their own role
            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                if (currentUserRole != "Admin")
                {
                    throw new System.UnauthorizedAccessException("Only Admin can change user roles.");
                }

                if (user.Id == currentUserId)
                {
                    throw new System.ArgumentException("Admin cannot change their own role.");
                }

                // Validate role value
                if (dto.Role != "Admin" && dto.Role != "User")
                {
                    throw new System.ArgumentException("Invalid role. Role must be 'Admin' or 'User'.");
                }

                var role = _roleRepository.GetAll()
                    .FirstOrDefault(r => r.Name == dto.Role);

                if (role == null)
                {
                    throw new System.ArgumentException("Role not found.");
                }

                user.RoleId = role.Id;
            }

            _userRepository.Update(user);
            return true;
        }

        public bool Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            return true;
        }
    }
}
