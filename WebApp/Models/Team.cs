namespace Wm22App.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public ICollection<Match> HomeMatches { get; set; }
        public ICollection<Match> AwayMatches { get; set; }
        public ICollection<Bonustipp> Bonustipps { get; set; }
    }
}
