using Wm22App.Enums;
using Wm22App.Models;

namespace Wm22App.Data
{
    public static class SeedService
    {
        public static IEnumerable<Team> GetTeams()
        {
            return new List<Team>() {
                new Team() { Id = 1, Name = "Argentinien", Icon="ar.svg" },
                new Team() { Id = 2, Name = "Australien", Icon="au.svg" },
                new Team() { Id = 3, Name = "Belgien", Icon="be.svg" },
                new Team() { Id = 4, Name = "Brasilien", Icon="br.svg" },
                new Team() { Id = 5, Name = "Costa Rica", Icon="cr.svg" },
                new Team() { Id = 6, Name = "Dänemark", Icon="dk.svg" },
                new Team() { Id = 7, Name = "Wales", Icon="gb-wls.svg" },
                new Team() { Id = 8, Name = "Deutschland", Icon="de.svg" },
                new Team() { Id = 9, Name = "Ecuador", Icon="ec.svg" },
                new Team() { Id = 10, Name = "England", Icon="gb.svg" },
                new Team() { Id = 11, Name = "Frankreich", Icon="fr.svg" },
                new Team() { Id = 12, Name = "Ghana", Icon="gh.svg" },
                new Team() { Id = 13, Name = "Iran", Icon="ir.svg" },
                new Team() { Id = 14, Name = "Japan", Icon="jp.svg" },
                new Team() { Id = 15, Name = "Kamerun", Icon="cm.svg" },
                new Team() { Id = 16, Name = "Kanada", Icon="ca.svg" },
                new Team() { Id = 17, Name = "Katar", Icon="qa.svg" },
                new Team() { Id = 18, Name = "Kroatien", Icon="hr.svg" },
                new Team() { Id = 19, Name = "Marokko", Icon="ma.svg" },
                new Team() { Id = 20, Name = "Mexiko", Icon="mx.svg" },
                new Team() { Id = 21, Name = "Niderlande", Icon="nl.svg" },
                new Team() { Id = 22, Name = "Polen", Icon="pl.svg" },
                new Team() { Id = 23, Name = "Portugal", Icon="pt.svg" },
                new Team() { Id = 24, Name = "Saudi-Arabien", Icon="sa.svg" },
                new Team() { Id = 25, Name = "Schweiz", Icon="ch.svg" },
                new Team() { Id = 26, Name = "Senegal", Icon="sn.svg" },
                new Team() { Id = 27, Name = "Serbien", Icon="rs.svg" },
                new Team() { Id = 28, Name = "Spanien", Icon="es.svg" },
                new Team() { Id = 29, Name = "Südkorea", Icon="kr.svg" },
                new Team() { Id = 30, Name = "Tunesien", Icon="tn.svg" },
                new Team() { Id = 31, Name = "Uruguay", Icon="uy.svg" },
                new Team() { Id = 32, Name = "USA", Icon="us.svg" },
            };
        }

        public static IEnumerable<Match> GetMatches()
        {
            return new List<Match>() {
                new Match() { Id = 1, HomeTeamId = 17, AwayTeamId = 9, MatchTimestamp = new DateTime(2022,11,20,17,0,0), Group = GroupType.A.ToString(), MatchdayId = 1 },

                new Match() { Id = 2, HomeTeamId = 10, AwayTeamId = 13, MatchTimestamp = new DateTime(2022,11,21,14,0,0), Group = GroupType.B.ToString(), MatchdayId = 1 },
                new Match() { Id = 3, HomeTeamId = 26, AwayTeamId = 21, MatchTimestamp = new DateTime(2022,11,21,17,0,0), Group = GroupType.A.ToString(), MatchdayId = 1 },
                new Match() { Id = 4, HomeTeamId = 32, AwayTeamId = 7, MatchTimestamp = new DateTime(2022,11,21,20,0,0), Group = GroupType.B.ToString(), MatchdayId = 1 },

                new Match() { Id = 5, HomeTeamId = 1, AwayTeamId = 24, MatchTimestamp = new DateTime(2022,11,22,11,0,0), Group = GroupType.C.ToString(), MatchdayId = 1 },
                new Match() { Id = 6, HomeTeamId = 6, AwayTeamId = 30, MatchTimestamp = new DateTime(2022,11,22,14,0,0), Group = GroupType.D.ToString(), MatchdayId = 1 },
                new Match() { Id = 7, HomeTeamId = 20, AwayTeamId = 22, MatchTimestamp = new DateTime(2022,11,22,17,0,0), Group = GroupType.C.ToString(), MatchdayId = 1 },
                new Match() { Id = 8, HomeTeamId = 11, AwayTeamId = 2, MatchTimestamp = new DateTime(2022,11,22,20,0,0), Group = GroupType.D.ToString(), MatchdayId = 1 },

                new Match() { Id = 9, HomeTeamId = 19, AwayTeamId = 18, MatchTimestamp = new DateTime(2022,11,23,11,0,0), Group = GroupType.F.ToString(), MatchdayId = 2 },
                new Match() { Id = 10, HomeTeamId = 8, AwayTeamId = 14, MatchTimestamp = new DateTime(2022,11,23,14,0,0), Group = GroupType.E.ToString(), MatchdayId = 2 },
                new Match() { Id = 11, HomeTeamId = 28, AwayTeamId = 5, MatchTimestamp = new DateTime(2022,11,23,17,0,0), Group = GroupType.E.ToString(), MatchdayId = 2 },
                new Match() { Id = 12, HomeTeamId = 3, AwayTeamId = 16, MatchTimestamp = new DateTime(2022,11,23,20,0,0), Group = GroupType.F.ToString(), MatchdayId = 2 },

                new Match() { Id = 13, HomeTeamId = 25, AwayTeamId = 15, MatchTimestamp = new DateTime(2022,11,24,11,0,0), Group = GroupType.G.ToString(), MatchdayId = 2 },
                new Match() { Id = 14, HomeTeamId = 31, AwayTeamId = 29, MatchTimestamp = new DateTime(2022,11,24,14,0,0), Group = GroupType.H.ToString(), MatchdayId = 2 },
                new Match() { Id = 15, HomeTeamId = 23, AwayTeamId = 12, MatchTimestamp = new DateTime(2022,11,24,17,0,0), Group = GroupType.H.ToString(), MatchdayId = 2 },
                new Match() { Id = 16, HomeTeamId = 4, AwayTeamId = 27, MatchTimestamp = new DateTime(2022,11,24,20,0,0), Group = GroupType.G.ToString(), MatchdayId = 2 },

                new Match() { Id = 17, HomeTeamId = 7, AwayTeamId = 13, MatchTimestamp = new DateTime(2022,11,25,11,0,0), Group = GroupType.B.ToString(), MatchdayId = 3 },
                new Match() { Id = 18, HomeTeamId = 17, AwayTeamId = 26, MatchTimestamp = new DateTime(2022,11,25,14,0,0), Group = GroupType.A.ToString(), MatchdayId = 3 },
                new Match() { Id = 19, HomeTeamId = 21, AwayTeamId = 9, MatchTimestamp = new DateTime(2022,11,25,17,0,0), Group = GroupType.A.ToString(), MatchdayId = 3 },
                new Match() { Id = 20, HomeTeamId = 10, AwayTeamId = 32, MatchTimestamp = new DateTime(2022,11,25,20,0,0), Group = GroupType.B.ToString(), MatchdayId = 3 },

                new Match() { Id = 21, HomeTeamId = 30, AwayTeamId = 2, MatchTimestamp = new DateTime(2022,11,26,11,0,0), Group = GroupType.D.ToString(), MatchdayId = 3 },
                new Match() { Id = 22, HomeTeamId = 22, AwayTeamId = 24, MatchTimestamp = new DateTime(2022,11,26,14,0,0), Group = GroupType.C.ToString(), MatchdayId = 3 },
                new Match() { Id = 23, HomeTeamId = 11, AwayTeamId = 6, MatchTimestamp = new DateTime(2022,11,26,17,0,0), Group = GroupType.D.ToString(), MatchdayId = 3 },
                new Match() { Id = 24, HomeTeamId = 1, AwayTeamId = 20, MatchTimestamp = new DateTime(2022,11,26,20,0,0), Group = GroupType.C.ToString(), MatchdayId = 3 },

                new Match() { Id = 25, HomeTeamId = 14, AwayTeamId = 5, MatchTimestamp = new DateTime(2022,11,27,11,0,0), Group = GroupType.E.ToString(), MatchdayId = 4 },
                new Match() { Id = 26, HomeTeamId = 3, AwayTeamId = 19, MatchTimestamp = new DateTime(2022,11,27,14,0,0), Group = GroupType.F.ToString(), MatchdayId = 4 },
                new Match() { Id = 27, HomeTeamId = 18, AwayTeamId = 16, MatchTimestamp = new DateTime(2022,11,27,17,0,0), Group = GroupType.F.ToString(), MatchdayId = 4 },
                new Match() { Id = 28, HomeTeamId = 28, AwayTeamId = 8, MatchTimestamp = new DateTime(2022,11,27,20,0,0), Group = GroupType.E.ToString(), MatchdayId = 4 },

                new Match() { Id = 29, HomeTeamId = 15, AwayTeamId = 27, MatchTimestamp = new DateTime(2022,11,28,11,0,0), Group = GroupType.G.ToString(), MatchdayId = 4 },
                new Match() { Id = 30, HomeTeamId = 29, AwayTeamId = 12, MatchTimestamp = new DateTime(2022,11,28,14,0,0), Group = GroupType.H.ToString(), MatchdayId = 4 },
                new Match() { Id = 31, HomeTeamId = 4, AwayTeamId = 25, MatchTimestamp = new DateTime(2022,11,28,17,0,0), Group = GroupType.G.ToString(), MatchdayId = 4 },
                new Match() { Id = 32, HomeTeamId = 23, AwayTeamId = 31, MatchTimestamp = new DateTime(2022,11,28,20,0,0), Group = GroupType.H.ToString(), MatchdayId = 4 },

                new Match() { Id = 33, HomeTeamId = 21, AwayTeamId = 17, MatchTimestamp = new DateTime(2022,11,29,16,0,0), Group = GroupType.A.ToString(), MatchdayId = 5 },
                new Match() { Id = 34, HomeTeamId = 9, AwayTeamId = 26, MatchTimestamp = new DateTime(2022,11,29,16,0,0), Group = GroupType.A.ToString(), MatchdayId = 5 },
                new Match() { Id = 35, HomeTeamId = 13, AwayTeamId = 32, MatchTimestamp = new DateTime(2022,11,29,20,0,0), Group = GroupType.B.ToString(), MatchdayId = 5 },
                new Match() { Id = 36, HomeTeamId = 7, AwayTeamId = 10, MatchTimestamp = new DateTime(2022,11,29,20,0,0), Group = GroupType.B.ToString(), MatchdayId = 5 },

                new Match() { Id = 37, HomeTeamId = 30, AwayTeamId = 11, MatchTimestamp = new DateTime(2022,11,30,16,0,0), Group = GroupType.D.ToString(), MatchdayId = 5 },
                new Match() { Id = 38, HomeTeamId = 2, AwayTeamId = 6, MatchTimestamp = new DateTime(2022,11,30,16,0,0), Group = GroupType.D.ToString(), MatchdayId = 5 },
                new Match() { Id = 39, HomeTeamId = 24, AwayTeamId = 20, MatchTimestamp = new DateTime(2022,11,30,20,0,0), Group = GroupType.C.ToString(), MatchdayId = 5 },
                new Match() { Id = 40, HomeTeamId = 22, AwayTeamId = 1, MatchTimestamp = new DateTime(2022,11,30,20,0,0), Group = GroupType.C.ToString(), MatchdayId = 5 },

                new Match() { Id = 41, HomeTeamId = 16, AwayTeamId = 19, MatchTimestamp = new DateTime(2022,12,01,16,0,0), Group = GroupType.F.ToString(), MatchdayId = 6 },
                new Match() { Id = 42, HomeTeamId = 18, AwayTeamId = 3, MatchTimestamp = new DateTime(2022,12,01,16,0,0), Group = GroupType.F.ToString(), MatchdayId = 6 },
                new Match() { Id = 43, HomeTeamId = 5, AwayTeamId = 8, MatchTimestamp = new DateTime(2022,12,01,20,0,0), Group = GroupType.E.ToString(), MatchdayId = 6 },
                new Match() { Id = 44, HomeTeamId = 14, AwayTeamId = 28, MatchTimestamp = new DateTime(2022,12,01,20,0,0), Group = GroupType.E.ToString(), MatchdayId = 6 },

                new Match() { Id = 45, HomeTeamId = 12, AwayTeamId = 31, MatchTimestamp = new DateTime(2022,12,02,16,0,0), Group = GroupType.H.ToString(), MatchdayId = 6 },
                new Match() { Id = 46, HomeTeamId = 29, AwayTeamId = 23, MatchTimestamp = new DateTime(2022,12,02,16,0,0), Group = GroupType.H.ToString(), MatchdayId = 6 },
                new Match() { Id = 47, HomeTeamId = 27, AwayTeamId = 25, MatchTimestamp = new DateTime(2022,12,02,20,0,0), Group = GroupType.G.ToString(), MatchdayId = 6 },
                new Match() { Id = 48, HomeTeamId = 15, AwayTeamId = 4, MatchTimestamp = new DateTime(2022,12,02,20,0,0), Group = GroupType.G.ToString(), MatchdayId = 6 },
            };
        }

        public static IEnumerable<Matchday> GetMatchdays()
        {
            return new List<Matchday>() {
                new Matchday() { Id = 1, Name = "Spieltag 1", Date = new DateTime(2022,11,20) },
                new Matchday() { Id = 2, Name = "Spieltag 2", Date = new DateTime(2022,11,23) },
                new Matchday() { Id = 3, Name = "Spieltag 3", Date = new DateTime(2022,11,25) },
                new Matchday() { Id = 4, Name = "Spieltag 4", Date = new DateTime(2022,11,27) },
                new Matchday() { Id = 5, Name = "Spieltag 5", Date = new DateTime(2022,11,29) },
                new Matchday() { Id = 6, Name = "Spieltag 6", Date = new DateTime(2022,12,1) },
                new Matchday() { Id = 7, Name = "Achtelfinale", Date = new DateTime(2022,12,3) },
                new Matchday() { Id = 8, Name = "Viertelfinale", Date = new DateTime(2022,12,9) },
                new Matchday() { Id = 9, Name = "Halbfinale", Date = new DateTime(2022,12,13) },
                new Matchday() { Id = 10, Name = "Finale", Date = new DateTime(2022,12,17) },
            };
        }
    }
}