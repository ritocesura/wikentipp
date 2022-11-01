namespace Wm22App.Dtos
{
    public class TippDto
    {
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public MatchDto Match { get; set; }
    }
}
