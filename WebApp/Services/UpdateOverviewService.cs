using Microsoft.EntityFrameworkCore;
using Wm22App.Data;
using Wm22App.Models;

namespace Wm22App.Services
{
    public class UpdateOverviewService : IUpdateOverviewService
    {
        private DatabaseContext Context { get; }

        public UpdateOverviewService(DatabaseContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task UpdateAsync()
        {
            // Hole alle Matches
            var matches = await Context.Matches
                .Include(i => i.Matchday)
                .Select(i => new
                {
                    MatchId = i.Id,
                    MatchdayId = i.MatchdayId,
                    HomeScore = i.HomeScore,
                    AwayScore = i.AwayScore,
                })
                .ToListAsync();

            // Hole alle Benutzer
            var userNames = await Context.Users
                .Select(i => i.UserName)
                .ToListAsync();

            // Für jeden Benutzer die Overview-Einträge berechnen
            var overviewList = new List<Overview>();
            foreach (var userName in userNames)
            {
                // Alle Tipps für den Benutzer ermitteln
                var userTipps = await Context.Tipps.Where(i => i.UserName == userName).ToListAsync();

                // Tipps aktualisieren
                foreach (var match in matches)
                {
                    if (match.HomeScore.HasValue && match.AwayScore.HasValue)
                    {
                        var tipp = userTipps.FirstOrDefault(i => i.MatchId == match.MatchId);
                        if (tipp != null)
                        {
                            overviewList.Add(new Overview()
                            {
                                UserName = userName,
                                TippId = tipp.Id,
                                MatchId = match.MatchId,
                                MatchdayId = match.MatchdayId,
                                Points = GetPoints(match.HomeScore.Value, match.AwayScore.Value, tipp.HomeScore, tipp.AwayScore)
                            });
                        }
                    }
                }
            }

            // Overview Tabelle aktualisieren
            foreach (var item in overviewList)
            {
                var entity = await Context.Overview.Where(i => i.UserName == item.UserName && i.MatchId == item.MatchId && i.TippId == item.TippId).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Points = item.Points;
                    Context.Overview.Update(entity);
                }
                else
                {
                    Context.Overview.Add(item);
                }
            }

            await Context.SaveChangesAsync();
        }

        private int GetPoints(int expectedHomeScore, int expectedAwayScore, int actualHomeScore, int actualAwayScore)
        {
            if (actualHomeScore == expectedHomeScore && actualAwayScore == expectedAwayScore)
            {
                // Ergebnis richtig
                return 4;
            }
            else if (actualHomeScore == actualAwayScore && expectedHomeScore == expectedAwayScore)
            {
                // TEndenz richtig (unentschieden)
                return 2;
            }
            else if (actualHomeScore - actualAwayScore == expectedHomeScore - expectedAwayScore)
            {
                // Tordiff richtig (gab Sieger)
                return 3;
            }
            else if (actualHomeScore > actualAwayScore && expectedHomeScore > expectedAwayScore)
            {
                // Tendenz richtig (home Sieger)
                return 2;
            }
            else if (actualHomeScore < actualAwayScore && expectedHomeScore < expectedAwayScore)
            {
                // Tendenz richtig (away Sieger)
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }
}