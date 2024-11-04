using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSimulation
{
    public class TeamGameStats
    {
        private int gameId;
        private int teamId;
        private string teamName;
        private string opponentName;
        private string location;
        private string result;
        private string score;
        private double fgpct;
        private double tfgpct;
        private int pts;
        private int reb;
        private int ast;
        private int stl;
        private int blk;
        private int tov;

        public List<PlayerInGame> homeTeam;
        public List<PlayerInGame> awayTeam;

        public int GameId { get; set; }
        public int TeamId { get; set; }

        public string TeamName { get; set; }
        public string OpponentName { get; set; }
        public string Location { get; set; }
        public string Result { get; set; }
        public string Score { get; set; }
        public double Fgpct { get; set; }
        public double Tfgpct { get; set; }
        public int Pts { get; set; }
        public int Reb { get; set; }
        public int Ast { get; set; }
        public int Stl { get; set; }
        public int Blk { get; set; }
        public int Tov { get; set; }

        public TeamGameStats(List<PlayerInGame> homeTeam, List<PlayerInGame> awayTeam, int gameId, int mainTeam)
        {

            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            PlayerInGame examplePlayer = homeTeam[0];
            (int, int) scores = CalculateScore(homeTeam, awayTeam);
            // calculate data for the away team
            if (mainTeam == 1)
            {
                examplePlayer = awayTeam[0];
                Location = "Away";
                Fgpct = Math.Round(CalculateFieldGoalPercentage(awayTeam), 1);
                Tfgpct = Math.Round(Calculate3FieldGoalPercentage(awayTeam), 1);
                // we calculate the result of the game for the away team, based on the score
                if (scores.Item1 > scores.Item2) Result = "L";
                else if (scores.Item1 == scores.Item2) Result = "D";
                else Result = "W";
                TeamName = examplePlayer.playerStats.teamName;
                OpponentName = homeTeam[0].playerStats.teamName;
                AddInGameStats(awayTeam);
            }
            // calculate data for the home team
            else
            {
                Location = "Home";
                Fgpct = Math.Round(CalculateFieldGoalPercentage(homeTeam), 1);
                Tfgpct = Math.Round(Calculate3FieldGoalPercentage(homeTeam), 1);
                TeamName = examplePlayer.playerStats.teamName;
                OpponentName = awayTeam[0].playerStats.teamName;
                // we calculate the result of the game for the home team, based on the score
                if (scores.Item1 > scores.Item2) Result = "W";
                else if (scores.Item1 == scores.Item2) Result = "D";
                else Result = "L";
                AddInGameStats(homeTeam);
            }


            // set attributes
            GameId = gameId;
            TeamId = examplePlayer.playerStats.TeamId;
            Score = PrintScore(scores);

        }

        public void AddInGameStats(List<PlayerInGame> team)
        {
            foreach (PlayerInGame player in team)
            {
                Pts += player.Points;
                Reb += player.Rebounds;
                Ast += player.Assists;
                Stl += player.Steals;
                Blk += player.Blocks;
                Tov += player.Turnovers;
            }
        }

        public static (int, int) CalculateScore(List<PlayerInGame> homeTeamPlayers, List<PlayerInGame> awayTeamPlayers)
        {
            // find sum of home team's points
            int homeScore = 0;
            foreach (PlayerInGame player in homeTeamPlayers)
            {
                homeScore += player.Points;
            }
            int awayScore = 0;
            foreach (PlayerInGame player in awayTeamPlayers)
            {
                awayScore += player.Points;
            }
            return (homeScore, awayScore);
        }

        public string PrintScore((int, int) scores) => ($"{scores.Item1}-{scores.Item2}");


        public double CalculateFieldGoalPercentage(List<PlayerInGame> team)
        {
            int FGM = 0;
            int FGA = 0;
            foreach (PlayerInGame player in team)
            {
                FGM += player.FieldGoalMade;
                FGA += player.FieldGoalAttempted;
            }
            return 100 * (double)FGM / (double)FGA;
        }

        public double Calculate3FieldGoalPercentage(List<PlayerInGame> team)
        {
            int threePM = 0;
            int threePA = 0;
            foreach (PlayerInGame player in team)
            {
                threePM += player.ThreePointMade;
                threePA += player.ThreePointAttempted;
            }
            return 100 * (double)threePM / (double)threePA;
        }
    }
}
