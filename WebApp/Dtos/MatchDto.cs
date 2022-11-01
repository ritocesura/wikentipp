namespace Wm22App.Dtos
{
    public class MatchDto
    {
        public int Id { get; set; }
        public DateTime MatchTimestamp { get; set; }
        public string Group { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }
        public MatchdayDto Matchday { get; set; }
    }
}