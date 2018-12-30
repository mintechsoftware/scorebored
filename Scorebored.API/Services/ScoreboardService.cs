using System.Collections.Generic;
using System.Linq;
using Scorebored.API.Models;

namespace Scorebored.API.Services
{
    public interface IScoreboardService
    {
        IEnumerable<GroupLeaderboardRow> GetGroupLeaderboard(long groupId);
    }

    public class ScoreboardService : IScoreboardService
    {
        private readonly ScoreboredContext _dbContext;

        public ScoreboardService(ScoreboredContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<GroupLeaderboardRow> GetGroupLeaderboard(long groupId)
        {
            var groupMembers = _dbContext.GroupMemberships.Where(gm => gm.GroupId == groupId);

            foreach (var groupMember in groupMembers)
            {
                var user = _dbContext.Users.Where(u => u.UserId == groupMember.UserId).FirstOrDefault();

                if (user != null)
                {
                    yield return new GroupLeaderboardRow
                    {
                        Name = user.Name,
                        Email = user.Email
                    };
                }
            }
        }
    }
}
