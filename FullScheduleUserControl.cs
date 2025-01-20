using System.Data.Entity.Core.Mapping;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using LeagueSimulation.Models;

namespace LeagueSimulation
{
    public partial class FullScheduleUserControl : UserControl
    {
        public League? league;
        public bool addPhrases = false;
        public string? CommentatorPhrase;
        public string? ScoreAfterPhrase;
        public FullScheduleUserControl(League? league)
        {
            this.league = league;
            InitializeComponent();
            if (league.Playoffs)
            {
                currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
                simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            }
            else
            {
                currentDayShownNum.Maximum = league.CurrentSchedule.Count;
                simToValue.Maximum = league.CurrentSchedule.Count - league.CurrentDay + 1;
            }
            currentDayShownNum.Value = league.CurrentDay;

            FillLabels();
        }

        private void FillLabels()
        {
            this.Hide();
            int currentDayShown = (int)currentDayShownNum.Value;
            List<string> currentDayGames = new List<string>();
            if (!league.Playoffs) currentDayGames = league.GetGamesForDay(currentDayShown);
            else currentDayGames = league.GetPlayoffGamesForDay(currentDayShown);
            // we update the teamsLabels in the game panels
            {
                int currentGameCounter = 0;
                string team1Record = "";
                string team2Record = "";
                if (currentGameCounter == 0 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game1TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 1 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game2TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 2 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game3TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 3 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game4TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 4 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game5TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 5 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game6TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 6 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game7TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 7 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game8TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 8 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game9TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 9 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game10TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 10 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game11TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 11 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game12TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 12 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    if (league.Playoffs)
                    {
                        team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                        team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
                    }
                    game13TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
            }

            // we get rid of the buttons that should not be used
            {
                int currentGameCounter = 0;
                if (currentGameCounter == 0 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game1WatchGameButton.Hide();
                        game1SimGameButton.Hide();
                    }
                    else game1ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 1 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game2WatchGameButton.Hide();
                        game2SimGameButton.Hide();
                    }
                    else game2ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 2 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game3WatchGameButton.Hide();
                        game3SimGameButton.Hide();
                    }
                    else game3ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 3 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game4WatchGameButton.Hide();
                        game4SimGameButton.Hide();
                    }
                    else game4ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 4 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game5WatchGameButton.Hide();
                        game5SimGameButton.Hide();
                    }
                    else game5ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 5 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game6WatchGameButton.Hide();
                        game6SimGameButton.Hide();
                    }
                    else game6ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 6 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game7WatchGameButton.Hide();
                        game7SimGameButton.Hide();
                    }
                    else game7ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 7 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game8WatchGameButton.Hide();
                        game8SimGameButton.Hide();
                    }
                    else game8ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 8 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game9WatchGameButton.Hide();
                        game9SimGameButton.Hide();
                    }
                    else game9ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 9 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game10WatchGameButton.Hide();
                        game10SimGameButton.Hide();
                    }
                    else game10ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 10 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game11WatchGameButton.Hide();
                        game11SimGameButton.Hide();
                    }
                    else game11ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 11 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game12WatchGameButton.Hide();
                        game12SimGameButton.Hide();
                    }
                    else game12ViewGameResults.Hide();
                    currentGameCounter++;
                }
                if (currentGameCounter == 12 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    bool gameComplete = false;
                    if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                    if (gameComplete)
                    {
                        game13WatchGameButton.Hide();
                        game13SimGameButton.Hide();
                    }
                    else game13ViewGameResults.Hide();
                    currentGameCounter++;
                }
            }

            // here we get rid of the panels that are not needed for that day
            {
                // we initally hide all the panels, then show them again if a game exists
                panel2.Hide();
                panel3.Hide();
                panel4.Hide();
                panel5.Hide();
                panel6.Hide();
                panel7.Hide();
                panel8.Hide();
                panel9.Hide();
                panel10.Hide();
                panel11.Hide();
                panel12.Hide();
                panel13.Hide();
                // we show all the games before the game at that number becomes null
                int firstDayThatIsNull = currentDayGames.Count + 1;
                if (firstDayThatIsNull > 2)
                {
                    panel2.Show();
                }
                if (firstDayThatIsNull > 3)
                {
                    panel3.Show();
                }
                if (firstDayThatIsNull > 4)
                {
                    panel4.Show();
                }
                if (firstDayThatIsNull > 5)
                {
                    panel5.Show();
                }
                if (firstDayThatIsNull > 6)
                {
                    panel6.Show();
                }
                if (firstDayThatIsNull > 7)
                {
                    panel7.Show();
                }
                if (firstDayThatIsNull > 8)
                {
                    panel8.Show();
                }
                if (firstDayThatIsNull > 9)
                {
                    panel9.Show();
                }
                if (firstDayThatIsNull > 10)
                {
                    panel10.Show();
                }
                if (firstDayThatIsNull > 11)
                {
                    panel11.Show();
                }
                if (firstDayThatIsNull > 12)
                {
                    panel12.Show();
                }
                if (firstDayThatIsNull > 13)
                {
                    panel13.Show();
                }
            }
            this.Show();
        }

        private void currentDayShownNum_ValueChanged(object sender, EventArgs e)
        {
            // we show all the controls again, then remove the ones we don't want
            foreach (Control control in panel1.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel2.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel3.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel4.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel5.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel6.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel7.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel8.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel9.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel10.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel11.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel12.Controls)
            {
                control.Show();
            }
            foreach (Control control in panel13.Controls)
            {
                control.Show();
            }
            FillLabels();
        }

        public void LoadSeasonSummary(SeasonSummaryUserControl userControl)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.FormClosed += new FormClosedEventHandler(MenuForm_FormClosed);
            menuForm.menuFormLayoutPanel.Width = userControl.Width + 10;
            menuForm.Width = userControl.Width + 40;
            menuForm.menuFormLayoutPanel.Height = userControl.Height + 10;
            menuForm.Height = userControl.Height + 40;
            menuForm.menuFormLayoutPanel.Controls.Add(userControl);
            this.Hide();
            menuForm.Show();
        }

        private void game1SimGameButton_Click(object sender, EventArgs e)
        {
            if ((int)currentDayShownNum.Value != league.CurrentDay)
            {
                MessageBox.Show(text: "You can only simulate the current day of the season. Simulate each day before this one before simulating games here.");
            }
            else
            {
                int currentSeason = league.CurrentSeason;
                List<List<string>> schedule = new List<List<string>>();
                if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
                else schedule = league.CurrentSchedule;
                int currentDay = (int)currentDayShownNum.Value;
                int currentGameSeason = league.CurrentSeason;
                string currentPlayoffsRound = "";
                if (league.Playoffs) currentPlayoffsRound = league.PlayoffsRound;
                league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][0], currentDay, true);
                string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][0].Split(',');
                teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                UpdateGame1Panel(teamsPlaying);
                if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
                else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
                simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
                if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
            }

        }

        private void UpdateGame1Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game1TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game1TeamsLabel.Refresh();
            game1WatchGameButton.Hide();
            game1SimGameButton.Hide();
            game1ViewGameResults.Show();
        }

        private void game2SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][1], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][1].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame2Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame2Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game2TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game2TeamsLabel.Refresh();
            game2WatchGameButton.Hide();
            game2SimGameButton.Hide();
            game2ViewGameResults.Show();
        }

        private void game3SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][2], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][2].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame3Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame3Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game3TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game3TeamsLabel.Refresh();
            game3WatchGameButton.Hide();
            game3SimGameButton.Hide();
            game3ViewGameResults.Show();
        }

        private void game4SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][3], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][3].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame4Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame4Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game4TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game4TeamsLabel.Refresh();
            game4WatchGameButton.Hide();
            game4SimGameButton.Hide();
            game4ViewGameResults.Show();
        }

        private void game5SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][4], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][4].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame5Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame5Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game5TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game5TeamsLabel.Refresh();
            game5WatchGameButton.Hide();
            game5SimGameButton.Hide();
            game5ViewGameResults.Show();
        }

        private void game6SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][5], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][5].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame6Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame6Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game6TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game6TeamsLabel.Refresh();
            game6WatchGameButton.Hide();
            game6SimGameButton.Hide();
            game6ViewGameResults.Show();
        }

        private void game7SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][6], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][6].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame7Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame7Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game7TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game7TeamsLabel.Refresh();
            game7WatchGameButton.Hide();
            game7SimGameButton.Hide();
            game7ViewGameResults.Show();
        }

        private void game8SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][7], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][7].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame8Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame8Panel(string[] teamsPlaying)
        {

            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            game8TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game8TeamsLabel.Refresh();
            game8WatchGameButton.Hide();
            game8SimGameButton.Hide();
            game8ViewGameResults.Show();
        }

        private void game9SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][8], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][8].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame9Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame9Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game9TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game9TeamsLabel.Refresh();
            game9WatchGameButton.Hide();
            game9SimGameButton.Hide();
            game9ViewGameResults.Show();
        }
        private void game10SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][9], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][9].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame10Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame10Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game10TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game10TeamsLabel.Refresh();
            game10WatchGameButton.Hide();
            game10SimGameButton.Hide();
            game10ViewGameResults.Show();
        }
        private void game11SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][10], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][10].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame11Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame11Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game11TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game11TeamsLabel.Refresh();
            game11WatchGameButton.Hide();
            game11SimGameButton.Hide();
            game11ViewGameResults.Show();
        }

        private void game12SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][11], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][11].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame12Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void UpdateGame12Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game12TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game12TeamsLabel.Refresh();
            game12WatchGameButton.Hide();
            game12SimGameButton.Hide();
            game12ViewGameResults.Show();
        }

        private void game13SimGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][12], (int)currentDayShownNum.Value, true);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][12].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame13Panel(teamsPlaying);
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game1WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][0].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][0], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame1Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        public void LoadViewGameResultsUserControl(ViewGameResultsUserControl userControl)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.FormClosed += new FormClosedEventHandler(MenuForm_FormClosed);
            menuForm.menuFormLayoutPanel.Width = userControl.Width + 10;
            menuForm.Width = userControl.Width + 40;
            menuForm.menuFormLayoutPanel.Controls.Add(userControl);
            this.Hide();
            menuForm.Show();
            // we hide the contents held in the full schedule currently
            //scheduleDisplayPanel.Controls.Clear();
            //scheduleDisplayPanel.Controls.Add(userControl);
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the main form again when second form is closed 
        }

        private async void LoadWatchGameUserControl(WatchGameUserControl userControl)
        {
            // we hide the contents held in the full schedule currently
            scheduleDisplayPanel.Controls.Clear();

            scheduleDisplayPanel.Controls.Add(userControl.panel2);
            scheduleDisplayPanel.Controls.Add(userControl.label3);
            scheduleDisplayPanel.Controls.Add(userControl.commentatorPhrasesLabel);
            scheduleDisplayPanel.Size = new Size(650, 40000);
            userControl.panel2.Show();
            userControl.label3.Show();
            userControl.commentatorPhrasesLabel.Show();
            scheduleDisplayPanel.Refresh();

            int commentatorCounter = 0;
            while (commentatorCounter < userControl.CommentatorPhrases.Count)
            {
                string commentatorPhrase = userControl.CommentatorPhrases[commentatorCounter];
                string currentScore = userControl.ScoreAfterEachPhrase[commentatorCounter];
                if (commentatorPhrase.Contains("substituted"))
                {
                    userControl.commentatorPhrasesLabel.Text = $"{commentatorPhrase}\n\n" + userControl.commentatorPhrasesLabel.Text;
                    userControl.scoreLabel.Text = currentScore;
                    commentatorCounter++;
                    addPhrases = false;
                }
                else
                {
                    await WaitToPrintPhrase((int)userControl.playbackSpeed.Value, commentatorPhrase, currentScore);
                    if (addPhrases)
                    {
                        userControl.commentatorPhrasesLabel.Text = $"{CommentatorPhrase}\n\n" + userControl.commentatorPhrasesLabel.Text;
                        userControl.scoreLabel.Text = ScoreAfterPhrase;
                        commentatorCounter++;
                        addPhrases = false;
                    }
                }
                userControl.panel2.Refresh();
                userControl.label3.Refresh();
                userControl.commentatorPhrasesLabel.Refresh();

            }

        }

        private async Task WaitToPrintPhrase(int playbackSpeed, string commentatorPhrase, string scoreAfterPhrase)
        {
            await Task.Delay((int)(5000 / playbackSpeed));
            PrintCommentatorPhrases(commentatorPhrase, scoreAfterPhrase);
        }

        private void PrintCommentatorPhrases(string commentatorPhrase, string scoreAfterPhrase)
        {
            addPhrases = true;
            CommentatorPhrase = commentatorPhrase;
            ScoreAfterPhrase = scoreAfterPhrase;
        }

        private void UpdateGame13Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game13TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game13TeamsLabel.Refresh();
            game13WatchGameButton.Hide();
            game13SimGameButton.Hide();
            game13ViewGameResults.Show();
        }

        private void simDayButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            league.SimulateDay(schedule[(int)currentDayShownNum.Value - 1], (int)currentDayShownNum.Value, league.Playoffs);

            // now we update all the buttons on the screen
            {
                List<string> gamesInDay = schedule[(int)currentDayShownNum.Value - 1];
                if (gamesInDay.Count > 0)
                {
                    string[] teamsPlaying = gamesInDay[0].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame1Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 1)
                {
                    string[] teamsPlaying = gamesInDay[1].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame2Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 2)
                {
                    string[] teamsPlaying = gamesInDay[2].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame3Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 3)
                {
                    string[] teamsPlaying = gamesInDay[3].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame4Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 4)
                {
                    string[] teamsPlaying = gamesInDay[4].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame5Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 5)
                {
                    string[] teamsPlaying = gamesInDay[5].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame6Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 6)
                {
                    string[] teamsPlaying = gamesInDay[6].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame7Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 7)
                {
                    string[] teamsPlaying = gamesInDay[7].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame8Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 8)
                {
                    string[] teamsPlaying = gamesInDay[8].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame9Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 9)
                {
                    string[] teamsPlaying = gamesInDay[9].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame10Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 10)
                {
                    string[] teamsPlaying = gamesInDay[10].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame11Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 11)
                {
                    string[] teamsPlaying = gamesInDay[11].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame12Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 12)
                {
                    string[] teamsPlaying = gamesInDay[12].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame13Panel(teamsPlaying);
                }
            }
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        // this button simulates a month within a season
        private void button1_Click(object sender, EventArgs e)
        {
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            int simEnd = 30;
            if (simEnd + league.CurrentDay > schedule.Count) simEnd = schedule.Count;
            for (int i = 0; i < simEnd; i++)
            {
                league.SimulateDay(schedule[(int)currentDayShownNum.Value - 1 + i], (int)currentDayShownNum.Value + i, league.Playoffs);
            }
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;

            // now we update all the buttons on the screen
            {
                currentDayShownNum.Value = league.CurrentDay;
                List<string> gamesInDay = schedule[(int)currentDayShownNum.Value - 1];
                if (gamesInDay.Count > 0)
                {
                    string[] teamsPlaying = gamesInDay[0].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame1Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 1)
                {
                    string[] teamsPlaying = gamesInDay[1].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame2Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 2)
                {
                    string[] teamsPlaying = gamesInDay[2].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame3Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 3)
                {
                    string[] teamsPlaying = gamesInDay[3].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame4Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 4)
                {
                    string[] teamsPlaying = gamesInDay[4].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame5Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 5)
                {
                    string[] teamsPlaying = gamesInDay[5].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame6Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 6)
                {
                    string[] teamsPlaying = gamesInDay[6].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame7Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 7)
                {
                    string[] teamsPlaying = gamesInDay[7].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame8Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 8)
                {
                    string[] teamsPlaying = gamesInDay[8].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame9Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 9)
                {
                    string[] teamsPlaying = gamesInDay[9].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame10Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 10)
                {
                    string[] teamsPlaying = gamesInDay[10].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame11Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 11)
                {
                    string[] teamsPlaying = gamesInDay[11].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame12Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 12)
                {
                    string[] teamsPlaying = gamesInDay[12].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame13Panel(teamsPlaying);
                }
            }
        }
        
        private void game2WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][1].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][1], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame2Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game3WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][2].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][2], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame3Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game4WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][3].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][3], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame4Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game5WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][4].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][4], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame5Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game6WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][5].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][5], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame6Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game7WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][6].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][6], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame7Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game8WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][7].Split(',');
            (List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][7], (int)currentDayShownNum.Value);
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame8Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game9WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            (List<string>, List<string>) phrases = league.WatchGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][8], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][8].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame9Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game10WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            (List<string>, List<string>) phrases = league.WatchGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][9], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][9].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame10Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game11WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            (List<string>, List<string>) phrases = league.WatchGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][10], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][10].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame11Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game12WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            (List<string>, List<string>) phrases = league.WatchGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][11], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][11].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame12Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game13WatchGameButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            (List<string>, List<string>) phrases = league.WatchGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][12], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][12].Split(',');
            teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
            teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
            UpdateGame13Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void simToEndButton_Click(object sender, EventArgs e)
        {
            int currentSeason = league.CurrentSeason;
            List<List<string>> schedule = new List<List<string>>();
            if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
            else schedule = league.CurrentSchedule;
            for (int i = 0; i < simToValue.Value; i++)
            {
                if (i >= schedule.Count) break;
                league.SimulateDay(schedule[(int)currentDayShownNum.Value - 1 + i], (int)currentDayShownNum.Value + i, league.Playoffs);
            }


            // now we update all the buttons on the screen
            {
                List<string> gamesInDay = schedule[(int)currentDayShownNum.Value - 1];
                if (gamesInDay.Count > 0)
                {
                    string[] teamsPlaying = gamesInDay[0].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame1Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 1)
                {
                    string[] teamsPlaying = gamesInDay[1].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame2Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 2)
                {
                    string[] teamsPlaying = gamesInDay[2].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame3Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 3)
                {
                    string[] teamsPlaying = gamesInDay[3].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame4Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 4)
                {
                    string[] teamsPlaying = gamesInDay[4].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame5Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 5)
                {
                    string[] teamsPlaying = gamesInDay[5].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame6Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 6)
                {
                    string[] teamsPlaying = gamesInDay[6].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame7Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 7)
                {
                    string[] teamsPlaying = gamesInDay[7].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame8Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 8)
                {
                    string[] teamsPlaying = gamesInDay[8].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame9Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 9)
                {
                    string[] teamsPlaying = gamesInDay[9].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame10Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 10)
                {
                    string[] teamsPlaying = gamesInDay[10].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame11Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 11)
                {
                    string[] teamsPlaying = gamesInDay[11].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame12Panel(teamsPlaying);
                }
                if (gamesInDay.Count > 12)
                {
                    string[] teamsPlaying = gamesInDay[12].Split(",");
                    teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                    teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                    UpdateGame13Panel(teamsPlaying);
                }
            }

            if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
            else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
            simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
            if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
        }

        private void game1ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][0];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][0];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game2ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][1];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][1];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game3ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][2];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][2];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game4ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][3];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][3];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game5ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][4];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][4];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game6ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][5];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][5];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game7ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][6];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][6];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game8ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][7];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][7];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game9ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][8];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][8];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game10ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][9];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][9];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game11ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][10];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][10];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game12ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][11];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][11];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game13ViewGameResults_Click(object sender, EventArgs e)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][12];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][12];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

    }
}
