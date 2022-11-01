using System.ComponentModel.DataAnnotations.Schema;

namespace Wm22App.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime MatchTimestamp { get; set; }
        public string Group { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }

        [InverseProperty("HomeMatches")]
        public Team HomeTeam { get; set; }

        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }

        [InverseProperty("AwayMatches")]
        public Team AwayTeam { get; set; }

        [ForeignKey("Matchday")]
        public int MatchdayId { get; set; }

        public Matchday Matchday { get; set; }

        public ICollection<Tipp> Tipps { get; set; }
        public ICollection<Overview> Overviews { get; set; }
    }
}