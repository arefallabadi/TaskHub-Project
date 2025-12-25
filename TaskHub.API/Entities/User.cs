using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using TaskHub.API.ParentEntities;

namespace TaskHub.API.Entities
{
    [Index(nameof(Username), IsUnique = true)] 
    public class User : IdEntity
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; } // Admin || User

        public List<TaskItem>? AssignedTasks { get; set; } 
    }
}
