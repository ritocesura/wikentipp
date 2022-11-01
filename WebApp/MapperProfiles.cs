using AutoMapper;
using Wm22App.Dtos;
using Wm22App.Models;

namespace Wm22App
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Match, MatchDto>();
            CreateMap<Tipp, TippDto>();
            CreateMap<Match, MatchDto>();
            CreateMap<Team, TeamDto>();
            CreateMap<Matchday, MatchdayDto>();
        }
    }
}