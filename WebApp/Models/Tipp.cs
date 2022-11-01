using System.ComponentModel.DataAnnotations.Schema;

namespace Wm22App.Models
{
    public class Tipp
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public Match Match { get; set; }
    }
}
