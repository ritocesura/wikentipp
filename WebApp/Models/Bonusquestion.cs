namespace Wm22App.Models
{
    public class Bonusquestion
    {
        public int Id { get; set; }
        public string Question { get; set; }

        public ICollection<Bonustipp> Bonustipps { get; set; }
    }
}
