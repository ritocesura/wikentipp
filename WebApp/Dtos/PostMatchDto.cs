namespace Wm22App.Dtos
{
    public class PostMatchDto
    {
        public DateTime MatchTimestamp { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int MatchdayId { get; set; }
    }
}