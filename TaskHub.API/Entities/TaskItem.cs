using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskHub.API.Enums;
using TaskHub.API.ParentEntities;

namespace TaskHub.API.Entities
{
    public class TaskItem : IdEntity
    {
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public TaskEnum Status { get; set; }
        // ToDo | InProgress | Cancelled |Completed

        // Foreign Key
        public int? AssignedUserId { get; set; }

        // Navigation Property
        [ForeignKey("AssignedUserId")]
        public User? AssignedUser { get; set; }
    }
}