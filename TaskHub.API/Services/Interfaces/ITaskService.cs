using TaskHub.API.DTOs.Pagination;
using TaskHub.API.DTOs.Task;
using TaskHub.API.Enums;

namespace TaskHub.API.Services.Interfaces
{
    public interface ITaskService
    {
        List<TaskDto> GetAll(PaginationParams pagination, int userId, string userRole);
        TaskDto? GetById(int id, int userId, string userRole);
        void Create(CreateTaskDto dto);
        void Update(int id, UpdateTaskDto dto, int userId, string userRole);
        bool Delete(int id, int userId, string userRole);
        void ChangeStatus(int id, TaskEnum status, int userId, string userRole);
    }
}