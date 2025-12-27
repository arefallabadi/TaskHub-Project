using TaskHub.API.ParentEntities;

namespace TaskHub.API.Entities
{
    public class Role : IdEntity
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
