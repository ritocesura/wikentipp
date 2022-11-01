using System.ComponentModel.DataAnnotations.Schema;

namespace Wm22App.Models
{
    public class Overview
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [ForeignKey("Tipp")]
        public int TippId { get; set; }

        public Tipp Tipp { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }

        public Match Match { get; set; }

        [ForeignKey("Matchday")]
        public int MatchdayId { get; set; }

        public Matchday Matchday { get; set; }

        public int Points { get; set; }
    }
}