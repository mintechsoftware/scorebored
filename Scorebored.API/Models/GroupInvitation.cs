using System;
using System.ComponentModel.DataAnnotations;

namespace Scorebored.API.Models
{
    public class GroupInvitation
    {
        public long GroupId { get; set; }
        public long RequestedById { get; set; }
        [Key]
        public string Code { get; set; }
        public Boolean Accepted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
