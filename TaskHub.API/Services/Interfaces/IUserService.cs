using TaskHub.API.DTOs.Pagination;
using TaskHub.API.DTOs.User;

namespace TaskHub.API.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAll(PaginationParams pagination);
        UserDto? GetById(int id, int currentUserId, string currentUserRole);
        void Create(CreateUserDto dto);
        bool Update(int id, UpdateUserDto dto, int currentUserId, string currentUserRole);
        bool Delete(int id);
    }
}