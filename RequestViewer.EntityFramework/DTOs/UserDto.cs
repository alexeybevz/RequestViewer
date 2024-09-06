using System;

namespace RequestViewer.EntityFramework.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string ActiveDirectoryCN { get; set; }
        public string Email { get; set; }
    }
}
