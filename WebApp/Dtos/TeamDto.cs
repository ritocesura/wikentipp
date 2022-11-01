using System.ComponentModel.DataAnnotations.Schema;
using Wm22App.Models;

namespace Wm22App.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
