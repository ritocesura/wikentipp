using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wm22App.Data;
using Wm22App.Dtos;

namespace Wm22App.Controllers
{
    [EnableCors("OpenCORSPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchdaysController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public MatchdaysController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchdayDto>>> GetMatches()
        {
            var list = await _context.Matchdays.ToListAsync();
            return _mapper.Map<List<MatchdayDto>>(list);
        }
    }
}