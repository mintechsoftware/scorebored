using System;

namespace Scorebored.API.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public long? CurrentGroupId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
