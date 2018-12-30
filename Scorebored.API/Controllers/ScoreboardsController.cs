using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scorebored.API.Models;
using Scorebored.API.Services;

namespace Scorebored.API.Controllers
{
    [Authorize]
    [ApiController]
    public class ScoreboardsController : ControllerBase
    {
        private IScoreboardService _scoreboardService;

        public ScoreboardsController(IScoreboardService scoreboardService)
        {
            _scoreboardService = scoreboardService;
        }

        [HttpGet("/scoreboards/group_leaderboard/{groupId}")]
        public IEnumerable<GroupLeaderboardRow> GetGroupLeaderboard(long groupId)
        {
            return _scoreboardService.GetGroupLeaderboard(groupId);
        }
    }
}
