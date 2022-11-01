using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wm22App.Data;
using Wm22App.Dtos;
using Wm22App.Models;
using Wm22App.Services;

namespace Wm22App.Controllers
{
    [EnableCors("OpenCORSPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MatchesController : ControllerBase
    {
        private IUpdateOverviewService UpdateOverviewService { get; }
        private readonly DatabaseContext Context;

        public MatchesController(DatabaseContext context, IUpdateOverviewService updateOverviewService)
        {
            UpdateOverviewService = updateOverviewService ?? throw new ArgumentNullException(nameof(updateOverviewService));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetMatches()
        {
            return await Context.Matches
                .Include(x => x.Matchday)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Select(i => new
                {
                    Id = i.Id,
                    MatchTimestamp = i.MatchTimestamp,
                    Group = i.Group,
                    HomeScore = i.HomeScore,
                    AwayScore = i.AwayScore,
                    HomeTeamName = i.HomeTeam.Name,
                    AwayTeamName = i.AwayTeam.Name,
                    HomeTeamIcon = i.HomeTeam.Icon,
                    AwayTeamIcon = i.AwayTeam.Icon,
                    MatchdayName = i.Matchday.Name
                })
                .OrderBy(i => i.MatchTimestamp)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PostMatchDto>> PostMatch(PostMatchDto matchDto)
        {
            var match = new Match()
            {
                MatchdayId = matchDto.MatchdayId,
                HomeTeamId = matchDto.HomeTeamId,
                AwayTeamId = matchDto.AwayTeamId,
                MatchTimestamp = matchDto.MatchTimestamp,
            };
            Context.Matches.Add(match);
            await Context.SaveChangesAsync();

            return CreatedAtAction("PostMatch", -1);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostMatchDto>> DeleteMatch(int id)
        {
            var obj = Context.Matches.Find(id);

            var tipps = await Context.Tipps.Where(i => i.MatchId == obj.Id).ToListAsync();
            if (tipps.Any())
            {
                Context.Tipps.RemoveRange(tipps);
            }

            var overviews = await Context.Overview.Where(i => i.MatchId == obj.Id).ToListAsync();
            if (overviews.Any())
            {
                Context.Overview.RemoveRange(overviews);
            }

            Context.Matches.Remove(obj);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("scores")]
        public async Task<ActionResult<IEnumerable<object>>> PutMatchScores(IEnumerable<PostMatchScoreDto> matches)
        {
            foreach (var match in matches)
            {
                var entity = await Context.Matches.Where(x => x.Id == match.Id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return Problem();
                }

                entity.AwayScore = match.AwayScore;
                entity.HomeScore = match.HomeScore;
                Context.Matches.Update(entity);
            }

            await Context.SaveChangesAsync();

            // Punktetabellen aktualisieren
            await UpdateOverviewService.UpdateAsync();

            return CreatedAtAction("PutMatchScores", -1);
        }
    }
}