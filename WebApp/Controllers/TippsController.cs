using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using Wm22App.Data;
using Wm22App.Dtos;
using Wm22App.Models;

namespace Wm22App.Controllers
{
    [EnableCors("OpenCORSPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Tippexperte,Admin")]
    public class TippsController : ControllerBase
    {
        public DatabaseContext Context { get; }
        public UserManager<User> UserManager { get; }
        public IConfiguration Configuration { get; }

        public TippsController(DatabaseContext context, UserManager<User> userManager, IConfiguration configuration)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            Configuration = configuration??throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet("own/{matchdayId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetMyTipps(int matchdayId)
        {
            var curDateTime = GetDateTimeNow();

            // Alle Tipps vom eingeloggten Benutzer
            var userName = User.Identity?.Name;
            var tippList = await Context.Tipps
                .Include(x => x.Match)
                .Include(x => x.Match.HomeTeam)
                .Include(x => x.Match.AwayTeam)
                .Where(x => x.UserName == userName && x.Match.Matchday.Id == matchdayId)
                .Select(i => new
                {
                    TippId = (int?)i.Id,
                    MatchId = i.Match.Id,
                    MatchTimestamp = i.Match.MatchTimestamp,
                    HomeScoreTipp = (int?)i.HomeScore,
                    AwayScoreTipp = (int?)i.AwayScore,
                    HomeTeamName = i.Match.HomeTeam.Name,
                    AwayTeamName = i.Match.AwayTeam.Name,
                    HomeTeamIcon = i.Match.HomeTeam.Icon,
                    AwayTeamIcon = i.Match.AwayTeam.Icon,
                    Disabled = curDateTime >= i.Match.MatchTimestamp
                })
                .ToListAsync();

            var matchList = await Context.Matches
                    .Include(x => x.Matchday)
                    .Include(x => x.HomeTeam)
                    .Include(x => x.AwayTeam)
                    .Where(x => x.Matchday.Id == matchdayId)
                    .Select(i => new
                    {
                        TippId = (int?)null,
                        MatchId = i.Id,
                        MatchTimestamp = i.MatchTimestamp,
                        HomeScoreTipp = (int?)null,
                        AwayScoreTipp = (int?)null,
                        HomeTeamName = i.HomeTeam.Name,
                        AwayTeamName = i.AwayTeam.Name,
                        HomeTeamIcon = i.HomeTeam.Icon,
                        AwayTeamIcon = i.AwayTeam.Icon,
                        Disabled = curDateTime >= i.MatchTimestamp
                    })
                    .ToListAsync();

            if (tippList.Count != matchList.Count)
            {
                foreach(var match in matchList)
                {
                    if (!tippList.Any(x => x.MatchId == match.MatchId))
                    {
                        tippList.Add(match);
                    }
                }
            }

            return tippList.OrderBy(i => i.MatchTimestamp).ToList();
        }

        [HttpPost]
        [Authorize(Roles = "Tippexperte")]
        public async Task<IActionResult> PostTipps(IList<PostTippDto> tipps)
        {
            var userName = User.Identity?.Name;
            foreach (var tipp in tipps)
            {
                var entity = await Context.Tipps
                    .Include(i => i.Match)
                    .Where(x => x.UserName == userName && x.MatchId == tipp.MatchId)
                    .FirstOrDefaultAsync();

                // Das Match muss in der Zukunft liegen
                var match = await Context.Matches.Where(i => i.Id == tipp.MatchId).FirstOrDefaultAsync();
                if (match == null || match.MatchTimestamp <= GetDateTimeNow())
                {
                    continue;
                }

                // Beide Werte müssen gesetzt sein
                if (!tipp.AwayScoreTipp.HasValue || !tipp.HomeScoreTipp.HasValue)
                {
                    continue;
                }

                // Update oder speichern
                if (entity != null)
                {
                    entity.AwayScore = tipp.AwayScoreTipp.Value;
                    entity.HomeScore = tipp.HomeScoreTipp.Value;
                    Context.Tipps.Update(entity);
                }
                else
                {
                    Context.Tipps.Add(new Tipp()
                    {
                        AwayScore = tipp.AwayScoreTipp.Value,
                        HomeScore = tipp.HomeScoreTipp.Value,
                        UserName = userName,
                        MatchId = tipp.MatchId,
                    });
                }
            }

            await Context.SaveChangesAsync();

            return CreatedAtAction("PostTipps", -1);
        }

        [HttpGet("overview")]
        public async Task<ActionResult<object>> GetOverview()
        {
            var matchdays = await Context.Matchdays.Select(i => new
            {
                i.Id,
                i.Name
            }).ToListAsync();

            var pointListPerUser = Context.Overview
                .AsEnumerable()
                .GroupBy(i => i.UserName)
                .ToList();

            List<dynamic> pointList = new List<dynamic>();
            foreach (var item in pointListPerUser)
            {
                var matchdayPoints = matchdays.Select(i => new
                {
                    MatchdayId = i.Id,
                    Points = item.Where(m => m.MatchdayId == i.Id).Select(m => m.Points).Sum()
                });
                var user = await UserManager.FindByNameAsync(item.Key);
                pointList.Add(new
                {
                    UserName = item.Key,
                    UserIcon = user?.UserIcon,
                    IsMyUser = item.Key == User.Identity?.Name,
                    IsAi = item.Key.ToLower() == Configuration.GetValue<string>("Credentials:AiUsername").ToLower(),
                    PointList = matchdayPoints.OrderBy(i => i.MatchdayId),
                    TotalPoints = item.Select(i => i.Points).Sum()
                });
            }

            return new
            {
                Header = matchdays,
                Content = pointList.OrderByDescending(i => i.TotalPoints)
            };
        }

        [HttpGet("overviewPerMatchday")]
        public async Task<ActionResult<object>> GetOverviewPerMatchday()
        {
            var currDateTime = GetDateTimeNow();
            var matchList = Context.Matches
                .Include(i => i.Matchday)
                .Include(i => i.HomeTeam)
                .Include(i => i.AwayTeam)
                .AsEnumerable()
                .GroupBy(x => x.MatchdayId)
                .SelectMany(g =>
                    g.Select(i => new
                    {
                        Matchday = i.Matchday.Name,
                        MatchId = i.Id,
                        HomeTeamShortName = new string(i.HomeTeam.Name.ToUpper().Take(3).ToArray()),
                        AwayTeamShortName = new string(i.AwayTeam.Name.ToUpper().Take(3).ToArray()),
                        HomeScore = i.HomeScore,
                        AwayScore = i.AwayScore,
                        HomeTeamIcon = i.HomeTeam.Icon,
                        AwayTeamIcon = i.AwayTeam.Icon,
                        MatchTimestamp = i.MatchTimestamp,
                    }))
                .OrderBy(x => x.Matchday)
                .ToList();

            var pointListPerUser = Context.Overview
                .Include(i => i.Tipp)
                .AsEnumerable()
                .GroupBy(i => i.UserName)
                .ToList();

            List<dynamic> pointList = new List<dynamic>();
            foreach (var grouping in pointListPerUser)
            {
                var totalPoints = 0;
                List<object> userMatches = new List<object>();
                foreach (var match in matchList)
                {
                    var userTipp = grouping.FirstOrDefault(i => i.MatchId == match.MatchId);
                    if (userTipp == null || match.MatchTimestamp > currDateTime)
                    {
                        userMatches.Add(new
                        {
                            MatchId = match.MatchId,
                            Matchday = match.Matchday,
                            HomeScoreTipp = (int?)null,
                            AwayScoreTipp = (int?)null,
                            Points = (int?)null
                        });
                    }
                    else
                    {
                        totalPoints += userTipp.Points;
                        userMatches.Add(new
                        {
                            MatchId = match.MatchId,
                            Matchday = match.Matchday,
                            HomeScoreTipp = userTipp.Tipp.HomeScore,
                            AwayScoreTipp = userTipp.Tipp.AwayScore,
                            Points = userTipp.Points
                        });
                    }
                }

                var user = await UserManager.FindByNameAsync(grouping.Key);
                pointList.Add(new
                {
                    UserName = grouping.Key,
                    UserIcon = user?.UserIcon,
                    IsMyUser = grouping.Key == User.Identity?.Name,
                    IsAi = grouping.Key.ToLower() == Configuration.GetValue<string>("Credentials:AiUsername").ToLower(),
                    Matches = userMatches,
                    TotalPoints = totalPoints,
                });
            }

            return new
            {
                Header = matchList,
                Content = pointList.OrderByDescending(i => i.TotalPoints)
            };
        }

        private DateTime GetDateTimeNow()
        {
            var specificDateTime = Configuration.GetValue<string>("DateTimeNow");
            //specificDateTime = "25.11.2022 01:01";
            if (DateTime.TryParse(specificDateTime, new CultureInfo("de-DE"), DateTimeStyles.None, out DateTime now))
            {
                return now;
            }

            return DateTime.Now;
        }
    }
}