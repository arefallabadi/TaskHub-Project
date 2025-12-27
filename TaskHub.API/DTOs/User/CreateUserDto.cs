namespace TaskHub.API.DTOs.User
{
    public class CreateUserDto
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
