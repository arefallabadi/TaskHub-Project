using System.Security;
using TaskHub.API.DTOs.Pagination;
using TaskHub.API.DTOs.Task;
using TaskHub.API.Entities;
using TaskHub.API.Enums;
using TaskHub.API.Repositories;
using TaskHub.API.Services.Interfaces;

namespace TaskHub.API.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskItem> _taskRepository;
        private readonly IRepository<User> _userRepository;

        public TaskService(IRepository<TaskItem> taskRepository, IRepository<User> userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        // Get all tasks with pagination - Admin sees all, User sees only assigned
        public List<TaskDto> GetAll(PaginationParams pagination, int userId, string userRole)
        {
            var allTasks = _taskRepository.GetAll();

            // User role: only show tasks assigned to them
            if (userRole != "Admin")
            {
                allTasks = allTasks.Where(t => t.AssignedUserId == userId).ToList();
            }

            var tasks = allTasks
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            // Get all unique user IDs
            var userIds = tasks.Where(t => t.AssignedUserId.HasValue)
                .Select(t => t.AssignedUserId.Value)
                .Distinct()
                .ToList();

            // Get users in one call
            var users = _userRepository.GetAll()
                .Where(u => userIds.Contains(u.Id))
                .ToDictionary(u => u.Id, u => u.Username);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                AssignedUserId = t.AssignedUserId ?? 0,
                AssignedUser = t.AssignedUserId.HasValue && users.ContainsKey(t.AssignedUserId.Value)
                    ? users[t.AssignedUserId.Value]
                    : null
            }).ToList();
        }

        // Get task by ID - Admin can see any, User can only see assigned
        public TaskDto? GetById(int id, int userId, string userRole)
        {
            var task = _taskRepository.GetById(id);
            if (task == null) return null;

            // User role: can only view tasks assigned to them
            if (userRole != "Admin" && task.AssignedUserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to view this task.");
            }

            string? assignedUsername = null;
            if (task.AssignedUserId.HasValue)
            {
                var user = _userRepository.GetById(task.AssignedUserId.Value);
                assignedUsername = user?.Username;
            }

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                AssignedUserId = task.AssignedUserId ?? 0,
                AssignedUser = assignedUsername
            };
        }

        // Create new task - Only Admin can create
        public void Create(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status, 
                AssignedUserId = dto.AssignedUserId
            };

            _taskRepository.Add(task);
        }

        // Update existing task - Admin can update any, User can only update assigned
        public void Update(int id, UpdateTaskDto dto, int userId, string userRole)
        {
            var task = _taskRepository.GetById(id);
            if (task == null) return;

            // User role: can only update tasks assigned to them
            if (userRole != "Admin" && task.AssignedUserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this task.");
            }

            if (!string.IsNullOrEmpty(dto.Title))
                task.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Description))
                task.Description = dto.Description;

            if (dto.Status != default(TaskEnum))
                task.Status = dto.Status;

            // Only Admin can change task assignee
            if (dto.AssignedUserId.HasValue)
            {
                if (userRole != "Admin")
                {
                    throw new UnauthorizedAccessException("Only Admin can change task assignee.");
                }

                // Validate that the assigned user exists
                var assignedUser = _userRepository.GetById(dto.AssignedUserId.Value);
                if (assignedUser == null)
                {
                    throw new System.ArgumentException("Assigned user not found.");
                }

                task.AssignedUserId = dto.AssignedUserId.Value;
            }

            _taskRepository.Update(task);
        }

        // Delete task - Admin can delete any, User can only delete assigned
        public bool Delete(int id, int userId, string userRole)
        {
            var task = _taskRepository.GetById(id);
            if (task == null) return false;

            // User role: can only delete tasks assigned to them
            if (userRole != "Admin" && task.AssignedUserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this task.");
            }

            _taskRepository.Delete(task);
            return true;
        }

        // Change task status - Admin can change any, User can change assigned
        public void ChangeStatus(int id, TaskEnum status, int userId, string userRole)
        {
            var task = _taskRepository.GetById(id);
            if (task == null)
                throw new Exception("Task not found");

            // User role: can only change status of tasks assigned to them
            if (userRole != "Admin" && task.AssignedUserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to change the status of this task.");
            }

            task.Status = status;
            _taskRepository.Update(task);
        }
    }
}
