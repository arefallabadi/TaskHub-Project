using TaskHub.API.Enums;

namespace TaskHub.API.DTOs.Task
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskEnum Status { get; set; }
        public int AssignedUserId { get; set; }
        public string AssignedUser { get; set; }
    }
}
