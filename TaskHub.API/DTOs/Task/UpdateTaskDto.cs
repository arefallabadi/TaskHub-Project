using TaskHub.API.Enums;

namespace TaskHub.API.DTOs.Task
{
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskEnum Status { get; set; }
        public int? AssignedUserId { get; set; }
    }
}