using System.ComponentModel.DataAnnotations.Schema;

namespace Wm22App.Models
{
    public class Bonustipp
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public Team Team { get; set; }

        [ForeignKey("Bonusquestion")]
        public int BonusquestionId { get; set; }

        public Bonusquestion Bonusquestion { get; set; }
    }
}