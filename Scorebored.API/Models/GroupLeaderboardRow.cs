using System;

namespace Scorebored.API.Models
{
    public class GroupLeaderboardRow
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long Wins { get; set; }
        public long Losses { get; set; }
        public decimal WinPercentage { get; set; }
        public string CurrentStreak { get; set; }
    }
}
