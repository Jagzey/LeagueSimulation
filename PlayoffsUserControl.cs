using System.Data;
using LeagueSimulation.Models;

namespace LeagueSimulation
{
    public partial class PlayoffsUserControl : UserControl
    {
        public League? CurrentLeague;
        public PlayoffsUserControl(League league)
        {
            this.CurrentLeague = league;
            InitializeComponent();
            FillPanels();
        }

        public void FillPanels()
        {
            string round = "First Round";
            List<string> firstRoundGames = CurrentLeague.GetPlayoffGamesByRound(round);
            List<string> eastTeamsInPositionOrder = CurrentLeague.GetConferenceTeamsByWinPct(1);
            List<string> westTeamsInPositionOrder = CurrentLeague.GetConferenceTeamsByWinPct(2);
            // here we fill in the first round game data
            {
                string[] game1TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "1").ToList()[0].Split(',');
                int team1Id = int.Parse(game1TeamsPlaying[0]);
                int team2Id = int.Parse(game1TeamsPlaying[1]);
                int conferenceId = int.Parse(game1TeamsPlaying[2]);
                team1game1EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game1EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game2TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "1").ToList()[1].Split(',');
                team1Id = int.Parse(game2TeamsPlaying[0]);
                team2Id = int.Parse(game2TeamsPlaying[1]);
                conferenceId = int.Parse(game2TeamsPlaying[2]);
                team1game2EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game2EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game3TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "1").ToList()[2].Split(',');
                team1Id = int.Parse(game3TeamsPlaying[0]);
                team2Id = int.Parse(game3TeamsPlaying[1]);
                conferenceId = int.Parse(game3TeamsPlaying[2]);
                team1game3EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game3EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game4TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "1").ToList()[3].Split(',');
                team1Id = int.Parse(game4TeamsPlaying[0]);
                team2Id = int.Parse(game4TeamsPlaying[1]);
                conferenceId = int.Parse(game4TeamsPlaying[2]);
                team1game4EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game4EastFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game5TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "2").ToList()[0].Split(',');
                team1Id = int.Parse(game5TeamsPlaying[0]);
                team2Id = int.Parse(game5TeamsPlaying[1]);
                conferenceId = int.Parse(game5TeamsPlaying[2]);
                team1game1WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game1WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game6TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "2").ToList()[1].Split(',');
                team1Id = int.Parse(game6TeamsPlaying[0]);
                team2Id = int.Parse(game6TeamsPlaying[1]);
                conferenceId = int.Parse(game6TeamsPlaying[2]);
                team1game2WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game2WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game7TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "2").ToList()[2].Split(',');
                team1Id = int.Parse(game7TeamsPlaying[0]);
                team2Id = int.Parse(game7TeamsPlaying[1]);
                conferenceId = int.Parse(game7TeamsPlaying[2]);
                team1game3WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game3WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game8TeamsPlaying = firstRoundGames.Where(x => x.Split(",")[2] == "2").ToList()[3].Split(',');
                team1Id = int.Parse(game8TeamsPlaying[0]);
                team2Id = int.Parse(game8TeamsPlaying[1]);
                conferenceId = int.Parse(game8TeamsPlaying[2]);
                team1game4WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game4WestFirstRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";
            }

            // here we fill in the secound round data, if needed
            if (CurrentLeague.PlayoffsRound == "Second Round" || CurrentLeague.PlayoffsRound == "Conference Finals" || CurrentLeague.PlayoffsRound == "Finals")
            {
                round = "Second Round";
                List<string> secondRoundGames = CurrentLeague.GetPlayoffGamesByRound(round);
                string[] game1TeamsPlaying = secondRoundGames.Where(x => x.Split(",")[2] == "1").ToList()[0].Split(',');
                int team1Id = int.Parse(game1TeamsPlaying[0]);
                int team2Id = int.Parse(game1TeamsPlaying[1]);
                int conferenceId = int.Parse(game1TeamsPlaying[2]);
                team1game1EastSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game1EastSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game2TeamsPlaying = secondRoundGames.Where(x => x.Split(",")[2] == "1").ToList()[1].Split(',');
                team1Id = int.Parse(game2TeamsPlaying[0]);
                team2Id = int.Parse(game2TeamsPlaying[1]);
                conferenceId = int.Parse(game2TeamsPlaying[2]);
                team1game2EastSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game2EastSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game3TeamsPlaying = secondRoundGames.Where(x => x.Split(",")[2] == "2").ToList()[0].Split(',');
                team1Id = int.Parse(game3TeamsPlaying[0]);
                team2Id = int.Parse(game3TeamsPlaying[1]);
                conferenceId = int.Parse(game3TeamsPlaying[2]);
                team1game1WestSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game1WestSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game4TeamsPlaying = secondRoundGames.Where(x => x.Split(",")[2] == "2").ToList()[1].Split(',');
                team1Id = int.Parse(game4TeamsPlaying[0]);
                team2Id = int.Parse(game4TeamsPlaying[1]);
                conferenceId = int.Parse(game4TeamsPlaying[2]);
                team1game2WestSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game2WestSecondRound.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";
            }

            // here we fill in the conference finals data, if needed
            if (CurrentLeague.PlayoffsRound == "Conference Finals" || CurrentLeague.PlayoffsRound == "Finals")
            {
                round = "Conference Finals";
                List<string> confFinalsGames = CurrentLeague.GetPlayoffGamesByRound(round);
                string[] game1TeamsPlaying = confFinalsGames.Where(x => x.Split(",")[2] == "1").ToList()[0].Split(',');
                int team1Id = int.Parse(game1TeamsPlaying[0]);
                int team2Id = int.Parse(game1TeamsPlaying[1]);
                int conferenceId = int.Parse(game1TeamsPlaying[2]);
                team1game1EastConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game1EastConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

                string[] game2TeamsPlaying = confFinalsGames.Where(x => x.Split(",")[2] == "2").ToList()[0].Split(',');
                team1Id = int.Parse(game2TeamsPlaying[0]);
                team2Id = int.Parse(game2TeamsPlaying[1]);
                conferenceId = int.Parse(game2TeamsPlaying[2]);
                team1game1WestConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2game1WestConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";

            }

            // here we fill in the finals data, if needed
            if (CurrentLeague.PlayoffsRound == "Finals")
            {
                round = "Finals";
                List<string> finalsGames = CurrentLeague.GetPlayoffGamesByRound(round);
                string[] game1TeamsPlaying = finalsGames[0].Split(',');
                int team1Id = int.Parse(game1TeamsPlaying[0]);
                int team2Id = int.Parse(game1TeamsPlaying[1]);
                int conferenceId = int.Parse(game1TeamsPlaying[2]);
                team1Finals.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team1Id.ToString(), team2Id.ToString(), round).Split("-")[0]}";
                team2Finals.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordByGivenRound(team2Id.ToString(), team1Id.ToString(), round).Split("-")[0]}";
            }

            foreach (Control control in bottomPanel.Controls)
            {

                if (control is Panel)
                {
                    foreach (Control control2 in control.Controls)
                    {
                        if (control2 is Label)
                        {
                            if (control2.Text.Contains(Team.GetCityFromTeamName(CurrentLeague.UserTeamName)) && !control2.Font.Style.HasFlag(FontStyle.Bold))
                            {
                                control2.Font = new Font(control2.Font.FontFamily, control2.Font.Size - 1, FontStyle.Bold);
                            }
                            else if (!control2.Text.Contains(Team.GetCityFromTeamName(CurrentLeague.UserTeamName)) && control2.Font.Style.HasFlag(FontStyle.Bold) && !control2.Text.Contains(":"))
                            {
                                control2.Font = new Font(control2.Font.FontFamily, control2.Font.Size + 1);
                            }
                        }
                    }
                }
                if (control is Label)
                {
                    if (control.Text.Contains(Team.GetCityFromTeamName(CurrentLeague.UserTeamName)) && !control.Font.Style.HasFlag(FontStyle.Bold))
                    {
                        control.Font = new Font(control.Font.FontFamily, control.Font.Size - 1, FontStyle.Bold);
                    }
                    else if (!control.Text.Contains(Team.GetCityFromTeamName(CurrentLeague.UserTeamName)) && control.Font.Style.HasFlag(FontStyle.Bold) && !control.Text.Contains(":"))
                    {
                        control.Font = new Font(control.Font.FontFamily, control.Font.Size + 1);
                    }
                }
            }
        }
    }
}
