using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskHub.API.ParentEntities;

namespace TaskHub.API.Entities
{
    public class Comment : IdEntity
    {
        [MaxLength(500)]
        public string Content { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int TaskItemId { get; set; }
        [ForeignKey("TaskItemId")]
        public TaskItem Task { get; set; }
    }
}
