using System.Collections.Generic;
using Scorebored.API.Models;

namespace Scorebored.API.Responses
{
    public class GetUserGroupsResponse
    {
        public long UserId { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
