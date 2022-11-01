namespace Wm22App.Models
{
    public class Matchday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }

        public ICollection<Match> Matches { get; set; }
        public ICollection<Overview> Overview { get; set; }
    }
}