using System.Data;
using System.Security.Permissions;
using LeagueSimulation.Models;

namespace LeagueSimulation
{
    public partial class PlayoffsUserControl : UserControl
    {
        public League? CurrentLeague;
        public List<Label> FirstRoundLabels;
        public List<Label> SecondRoundLabels;
        public List<Label> ConferenceFinalsLabels;
        public PlayoffsUserControl(League league)
        {
            InitializeComponent();
            FirstRoundLabels = new List<Label>()
            {
                team1game1EastFirstRound,
                team2game1EastFirstRound,
                team1game2EastFirstRound,
                team2game2EastFirstRound,
                team1game3EastFirstRound,
                team2game3EastFirstRound,
                team1game4EastFirstRound,
                team2game4EastFirstRound,

                team1game1WestFirstRound,
                team2game1WestFirstRound,
                team1game2WestFirstRound,
                team2game2WestFirstRound,
                team1game3WestFirstRound,
                team2game3WestFirstRound,
                team1game4WestFirstRound,
                team2game4WestFirstRound
            };
            SecondRoundLabels = new List<Label>()
            {
                team1game1EastSecondRound,
                team2game1EastSecondRound,
                team1game2EastSecondRound,
                team2game2EastSecondRound,

                team1game1WestSecondRound,
                team2game1WestSecondRound,
                team1game2WestSecondRound,
                team2game2WestSecondRound
            };
            ConferenceFinalsLabels = new List<Label>()
            {
                team1game1EastSecondRound,
                team2game1EastSecondRound,

                team1game1WestSecondRound,
                team2game1WestSecondRound
            };
            this.CurrentLeague = league;
            FillPanels();
        }

        public void FillPanels()
        {
            string round = "First Round";
            List<string> firstRoundGames = CurrentLeague.GetPlayoffGamesByRound(round);
            List<string> eastFirstRoundGames = firstRoundGames.Where(x => x.Split(",")[2] == "1").ToList();
            List<string> westFirstRoundGames = firstRoundGames.Where(x => x.Split(",")[2] == "2").ToList();

            List<string> eastTeamsInPositionOrder = CurrentLeague.GetConferenceTeamsByWinPct(1);
            List<string> westTeamsInPositionOrder = CurrentLeague.GetConferenceTeamsByWinPct(2);

            for (int i = 0; i < 4; i++)
            {
                string[] game1EastGameTeamsPlaying = eastFirstRoundGames[i].Split(",");
                int team1Id = int.Parse(game1EastGameTeamsPlaying[0]);
                int team2Id = int.Parse(game1EastGameTeamsPlaying[1]);
                int conferenceId = int.Parse(game1EastGameTeamsPlaying[2]);
                Label team1EastFirstRoundLabel = FirstRoundLabels[i * 2];
                Label team2EastFirstRoundLabel = FirstRoundLabels[i * 2 + 1];
                team1EastFirstRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team1Id.ToString(), team2Id.ToString()).Split("-")[0]}";
                team2EastFirstRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team2Id.ToString(), team1Id.ToString()).Split("-")[0]}";

                string[] game1WestGameTeamsPlaying = westFirstRoundGames[i].Split(",");
                team1Id = int.Parse(game1WestGameTeamsPlaying[0]);
                team2Id = int.Parse(game1WestGameTeamsPlaying[1]);
                conferenceId = int.Parse(game1WestGameTeamsPlaying[2]);
                Label team1WestFirstRoundLabel = FirstRoundLabels[i * 2 + 8];
                Label team2WestFirstRoundLabel = FirstRoundLabels[i * 2 + 9];
                team1WestFirstRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team1Id.ToString(), team2Id.ToString()).Split("-")[0]}";
                team2WestFirstRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team2Id.ToString(), team1Id.ToString()).Split("-")[0]}";
            }

            // here we fill in the secound round data, if needed
            if (CurrentLeague.PlayoffsRound == "Second Round" || CurrentLeague.PlayoffsRound == "Conference Finals" || CurrentLeague.PlayoffsRound == "Finals")
            {
                round = "Second Round";
                List<string> secondRoundGames = CurrentLeague.GetPlayoffGamesByRound(round);
                List<string> eastSecondRoundGames = secondRoundGames.Where(x => x.Split(",")[2] == "1").ToList();
                List<string> westSecondRoundGames = secondRoundGames.Where(x => x.Split(",")[2] == "2").ToList();

                for (int i = 0; i < 2; i++)
                {
                    string[] game1EastGameTeamsPlaying = eastSecondRoundGames[i].Split(",");
                    int team1Id = int.Parse(game1EastGameTeamsPlaying[0]);
                    int team2Id = int.Parse(game1EastGameTeamsPlaying[1]);
                    int conferenceId = int.Parse(game1EastGameTeamsPlaying[2]);
                    Label team1EastSecondRoundLabel = SecondRoundLabels[i * 2];
                    Label team2EastSecondRoundLabel = SecondRoundLabels[i * 2 + 1];
                    team1EastSecondRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team1Id.ToString(), team2Id.ToString()).Split("-")[0]}";
                    team2EastSecondRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team2Id.ToString(), team1Id.ToString()).Split("-")[0]}";

                    string[] game1WestGameTeamsPlaying = westSecondRoundGames[i].Split(",");
                    team1Id = int.Parse(game1WestGameTeamsPlaying[0]);
                    team2Id = int.Parse(game1WestGameTeamsPlaying[1]);
                    conferenceId = int.Parse(game1WestGameTeamsPlaying[2]);
                    Label team1WestSecondRoundLabel = SecondRoundLabels[i * 2 + 4];
                    Label team2WestSecondRoundLabel = SecondRoundLabels[i * 2 + 5];
                    team1WestSecondRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team1Id.ToString(), team2Id.ToString()).Split("-")[0]}";
                    team2WestSecondRoundLabel.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team2Id.ToString(), team1Id.ToString()).Split("-")[0]}";
                }
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
                team1game1EastConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team1Id.ToString(), team2Id.ToString()).Split("-")[0]}";
                team2game1EastConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team2Id.ToString(), team1Id.ToString()).Split("-")[0]}";

                string[] game2TeamsPlaying = confFinalsGames.Where(x => x.Split(",")[2] == "2").ToList()[0].Split(',');
                team1Id = int.Parse(game2TeamsPlaying[0]);
                team2Id = int.Parse(game2TeamsPlaying[1]);
                conferenceId = int.Parse(game2TeamsPlaying[2]);
                team1game1WestConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team1Id.ToString(), team2Id.ToString()).Split("-")[0]}";
                team2game1WestConfFinals.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team2Id.ToString(), team1Id.ToString()).Split("-")[0]}";

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
                team1Finals.Text = $"{CurrentLeague.GetCityNameFromId(team1Id.ToString())} ({eastTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team1Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team1Id.ToString(), team2Id.ToString()).Split("-")[0]}";
                team2Finals.Text = $"{CurrentLeague.GetCityNameFromId(team2Id.ToString())} ({westTeamsInPositionOrder.IndexOf(CurrentLeague.GetTeamNameFromId(team2Id.ToString())) + 1}) {CurrentLeague.GetSeriesRecordToDisplay(team2Id.ToString(), team1Id.ToString()).Split("-")[0]}";
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
