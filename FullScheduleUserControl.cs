using LeagueSimulation.Models;

namespace LeagueSimulation
{
    public partial class FullScheduleUserControl : UserControl
    {
        public League? league;
        public bool addPhrases = false;
        public string? CommentatorPhrase;
        public string? ScoreAfterPhrase;
        public string? CurrentTime;
        public List<Label> GameTeamLabels;
        public List<Panel> Panels;
        public List<Button> WatchGameButtons;
        public List<Button> SimGameButtons;
        public List<Button> ViewGameResultsButtons;

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

            // fill the lists
            {
                GameTeamLabels = new List<Label>()
                {
                    game1TeamsLabel,
                    game2TeamsLabel,
                    game3TeamsLabel,
                    game4TeamsLabel,
                    game5TeamsLabel,
                    game6TeamsLabel,
                    game7TeamsLabel,
                    game8TeamsLabel,
                    game9TeamsLabel,
                    game10TeamsLabel,
                    game11TeamsLabel,
                    game12TeamsLabel,
                    game13TeamsLabel,
                };
                Panels = new List<Panel>()
                {
                    panel1,
                    panel2,
                    panel3,
                    panel4,
                    panel5,
                    panel6,
                    panel7,
                    panel8,
                    panel9,
                    panel10,
                    panel11,
                    panel12,
                    panel13
                };
                WatchGameButtons = new List<Button>()
                {
                    game1WatchGameButton,
                    game2WatchGameButton,
                    game3WatchGameButton,
                    game4WatchGameButton,
                    game5WatchGameButton,
                    game6WatchGameButton,
                    game7WatchGameButton,
                    game8WatchGameButton,
                    game9WatchGameButton,
                    game10WatchGameButton,
                    game11WatchGameButton,
                    game12WatchGameButton,
                    game13WatchGameButton
                };
                SimGameButtons = new List<Button>()
                {
                    game1SimGameButton,
                    game2SimGameButton,
                    game3SimGameButton,
                    game4SimGameButton,
                    game5SimGameButton,
                    game6SimGameButton,
                    game7SimGameButton,
                    game8SimGameButton,
                    game9SimGameButton,
                    game10SimGameButton,
                    game11SimGameButton,
                    game12SimGameButton,
                    game13SimGameButton
                };
                ViewGameResultsButtons = new List<Button>()
                {
                    game1ViewGameResults,
                    game2ViewGameResults,
                    game3ViewGameResults,
                    game4ViewGameResults,
                    game5ViewGameResults,
                    game6ViewGameResults,
                    game7ViewGameResults,
                    game8ViewGameResults,
                    game9ViewGameResults,
                    game10ViewGameResults,
                    game11ViewGameResults,
                    game12ViewGameResults,
                    game13ViewGameResults,
                };
            }

            FillLabels();
        }

        private void FillLabels()
        {
            this.Hide();
            int currentDayShown = (int)currentDayShownNum.Value;
            List<string> currentDayGames = new List<string>();
            if (!league.Playoffs) currentDayGames = league.GetGamesForDay(currentDayShown);
            else currentDayGames = league.GetPlayoffGamesForDay(currentDayShown);

            foreach (Label gameTeamLabel in GameTeamLabels)
            {
                if (gameTeamLabel.Font.Bold)
                {
                    gameTeamLabel.Font = new Font(gameTeamLabel.Font, FontStyle.Regular);
                }
            }
            {
                if (game1TeamsLabel.Font.Bold)
                {
                    game1TeamsLabel.Font = new Font(game1TeamsLabel.Font, FontStyle.Regular);
                }

                if (game2TeamsLabel.Font.Bold)
                {
                    game2TeamsLabel.Font = new Font(game2TeamsLabel.Font, FontStyle.Regular);
                }

                if (game3TeamsLabel.Font.Bold)
                {
                    game3TeamsLabel.Font = new Font(game3TeamsLabel.Font, FontStyle.Regular);
                }

                if (game4TeamsLabel.Font.Bold)
                {
                    game4TeamsLabel.Font = new Font(game4TeamsLabel.Font, FontStyle.Regular);
                }

                if (game5TeamsLabel.Font.Bold)
                {
                    game5TeamsLabel.Font = new Font(game5TeamsLabel.Font, FontStyle.Regular);
                }

                if (game6TeamsLabel.Font.Bold)
                {
                    game6TeamsLabel.Font = new Font(game6TeamsLabel.Font, FontStyle.Regular);
                }

                if (game7TeamsLabel.Font.Bold)
                {
                    game7TeamsLabel.Font = new Font(game7TeamsLabel.Font, FontStyle.Regular);
                }

                if (game8TeamsLabel.Font.Bold)
                {
                    game8TeamsLabel.Font = new Font(game8TeamsLabel.Font, FontStyle.Regular);
                }

                if (game9TeamsLabel.Font.Bold)
                {
                    game9TeamsLabel.Font = new Font(game9TeamsLabel.Font, FontStyle.Regular);
                }

                if (game10TeamsLabel.Font.Bold)
                {
                    game10TeamsLabel.Font = new Font(game10TeamsLabel.Font, FontStyle.Regular);
                }

                if (game11TeamsLabel.Font.Bold)
                {
                    game11TeamsLabel.Font = new Font(game11TeamsLabel.Font, FontStyle.Regular);
                }

                if (game12TeamsLabel.Font.Bold)
                {
                    game12TeamsLabel.Font = new Font(game12TeamsLabel.Font, FontStyle.Regular);
                }

                if (game13TeamsLabel.Font.Bold)
                {
                    game13TeamsLabel.Font = new Font(game13TeamsLabel.Font, FontStyle.Regular);
                }

            }

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

                    if (game1TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game1TeamsLabel.Font = new Font(game1TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game2TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game2TeamsLabel.Font = new Font(game2TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game3TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game3TeamsLabel.Font = new Font(game3TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game4TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game4TeamsLabel.Font = new Font(game4TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game5TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game5TeamsLabel.Font = new Font(game5TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game6TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game6TeamsLabel.Font = new Font(game6TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game7TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game7TeamsLabel.Font = new Font(game7TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game8TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game8TeamsLabel.Font = new Font(game8TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game9TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game9TeamsLabel.Font = new Font(game9TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game10TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game10TeamsLabel.Font = new Font(game10TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game11TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game11TeamsLabel.Font = new Font(game11TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game12TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game12TeamsLabel.Font = new Font(game12TeamsLabel.Font, FontStyle.Bold);
                    }
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

                    if (game13TeamsLabel.Text.Contains(league.UserTeamName))
                    {
                        game13TeamsLabel.Font = new Font(game13TeamsLabel.Font, FontStyle.Bold);
                    }
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

        private void SimGameFunction(int gameNumber)
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
                league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][gameNumber], currentDay, true);
                string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][gameNumber].Split(',');
                teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);

                if (gameNumber == 0) UpdateGame1Panel(teamsPlaying);
                else if (gameNumber == 1) UpdateGame2Panel(teamsPlaying);
                else if (gameNumber == 2) UpdateGame3Panel(teamsPlaying);
                else if (gameNumber == 3) UpdateGame4Panel(teamsPlaying);
                else if (gameNumber == 4) UpdateGame5Panel(teamsPlaying);
                else if (gameNumber == 5) UpdateGame6Panel(teamsPlaying);
                else if (gameNumber == 6) UpdateGame7Panel(teamsPlaying);
                else if (gameNumber == 7) UpdateGame8Panel(teamsPlaying);
                else if (gameNumber == 8) UpdateGame9Panel(teamsPlaying);
                else if (gameNumber == 9) UpdateGame10Panel(teamsPlaying);
                else if (gameNumber == 10) UpdateGame11Panel(teamsPlaying);
                else if (gameNumber == 11) UpdateGame12Panel(teamsPlaying);
                else if (gameNumber == 12) UpdateGame13Panel(teamsPlaying);

                if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
                else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
                simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
                if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
            }

        }

        private void game1SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(0);

        private void UpdateGamePanel(int index, string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            if (league.Playoffs)
            {
                team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[0]).ToString(), league.GetIdFromTeamName(teamsPlaying[1]).ToString());
                team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(teamsPlaying[1]).ToString(), league.GetIdFromTeamName(teamsPlaying[0]).ToString());
            }
            GameTeamLabels[index].Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            GameTeamLabels[index].Refresh();
            WatchGameButtons[index].Hide();
            SimGameButtons[index].Hide();
            ViewGameResultsButtons[index].Show();
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

        private void game2SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(1);

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

        private void game3SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(2);

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

        private void game4SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(3);

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

        private void game5SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(4);

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

        private void game6SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(5);

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

        private void game7SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(6);

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

        private void game8SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(7);

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

        private void game9SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(8);

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
        private void game10SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(9);

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
        private void game11SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(10);

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

        private void game12SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(11);

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

        private void game13SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(12);

        private void game1WatchGameButton_Click(object sender, EventArgs e)
        {
            if ((int)currentDayShownNum.Value != league.CurrentDay)
            {
                MessageBox.Show(text: "You can only simulate the current day of the season. Simulate each day before this one before simulating games here.");
            }
            else WatchGameFunction(0);

        }

        public void LoadViewGameResultsUserControl(ViewGameResultsUserControl userControl)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.FormClosed += new FormClosedEventHandler(MenuForm_FormClosed);
            menuForm.menuFormLayoutPanel.Width = userControl.Width + 10;
            menuForm.Width = userControl.Width + 40;
            menuForm.menuFormLayoutPanel.Height += 140;
            menuForm.Height += 140;
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
            scheduleDisplayPanel.Size = new Size(1100, 13000);
            userControl.panel2.Show();
            userControl.label3.Show();
            userControl.commentatorPhrasesLabel.Show();
            scheduleDisplayPanel.Refresh();

            int commentatorCounter = 0;
            while (commentatorCounter < userControl.CommentatorPhrases.Count)
            {
                string commentatorPhrase = userControl.CommentatorPhrases[commentatorCounter];
                string currentScore = userControl.ScoreAfterEachPhrase[commentatorCounter];
                string currentTime = userControl.GameTimestamps[commentatorCounter];
                if (commentatorPhrase.Contains("substituted"))
                {
                    userControl.commentatorPhrasesLabel.Text = $"{commentatorPhrase}\n\n" + userControl.commentatorPhrasesLabel.Text;
                    userControl.scoreLabel.Text = currentScore;
                    commentatorCounter++;
                    addPhrases = false;
                }
                else
                {
                    await WaitToPrintPhrase((int)userControl.playbackSpeed.Value, commentatorPhrase, currentScore, currentTime);
                    if (addPhrases)
                    {
                        userControl.commentatorPhrasesLabel.Text = $"{CommentatorPhrase}\n\n" + userControl.commentatorPhrasesLabel.Text;
                        userControl.scoreLabel.Text = ScoreAfterPhrase;
                        userControl.timeLabel.Text = currentTime;
                        commentatorCounter++;
                        addPhrases = false;
                    }
                }
                userControl.panel2.Refresh();
                userControl.label3.Refresh();
                userControl.commentatorPhrasesLabel.Refresh();

            }

        }

        private async Task WaitToPrintPhrase(int playbackSpeed, string commentatorPhrase, string scoreAfterPhrase, string currentTime)
        {
            await Task.Delay((int)(5000 / playbackSpeed));
            PrintCommentatorPhrases(commentatorPhrase, scoreAfterPhrase, currentTime);
        }

        private void PrintCommentatorPhrases(string commentatorPhrase, string scoreAfterPhrase, string currentTime)
        {
            addPhrases = true;
            CommentatorPhrase = commentatorPhrase;
            ScoreAfterPhrase = scoreAfterPhrase;
            CurrentTime = currentTime;
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

        private void WatchGameFunction(int gameNumber)
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
                string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][gameNumber].Split(',');
                (List<string>, List<string>, List<string>) phrases = league.WatchGame(schedule[(int)currentDayShownNum.Value - 1][gameNumber], (int)currentDayShownNum.Value);
                teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);

                if (gameNumber == 0) UpdateGame1Panel(teamsPlaying);
                else if (gameNumber == 1) UpdateGame2Panel(teamsPlaying);
                else if (gameNumber == 2) UpdateGame3Panel(teamsPlaying);
                else if (gameNumber == 3) UpdateGame4Panel(teamsPlaying);
                else if (gameNumber == 4) UpdateGame5Panel(teamsPlaying);
                else if (gameNumber == 5) UpdateGame6Panel(teamsPlaying);
                else if (gameNumber == 6) UpdateGame7Panel(teamsPlaying);
                else if (gameNumber == 7) UpdateGame8Panel(teamsPlaying);
                else if (gameNumber == 8) UpdateGame9Panel(teamsPlaying);
                else if (gameNumber == 9) UpdateGame10Panel(teamsPlaying);
                else if (gameNumber == 10) UpdateGame11Panel(teamsPlaying);
                else if (gameNumber == 11) UpdateGame12Panel(teamsPlaying);
                else if (gameNumber == 12) UpdateGame13Panel(teamsPlaying);

                LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, phrases.Item3, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
                if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
                else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
                simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
                if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
            }

        }

        private void game2WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(1);

        private void game3WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(2);

        private void game4WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(3);

        private void game5WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(4);

        private void game6WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(5);

        private void game7WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(6);

        private void game8WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(7);

        private void game9WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(8);

        private void game10WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(9);

        private void game11WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(10);

        private void game12WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(11);

        private void game13WatchGameButton_Click(object sender, EventArgs e) => WatchGameFunction(12);

        private void simToEndButton_Click(object sender, EventArgs e)
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

        }

        private void ViewGameResultsFunction(int gameNumber)
        {
            string teamsPlaying = "";
            int gameId = 0;
            if (league.Playoffs)
            {
                teamsPlaying = league.CurrentPlayoffsSchedule[(int)currentDayShownNum.Value - 1][gameNumber];
                gameId = league.GetPlayoffGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            else
            {
                teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][gameNumber];
                gameId = league.GetGameId(teamsPlaying, (int)currentDayShownNum.Value);
            }
            LoadViewGameResultsUserControl(new ViewGameResultsUserControl(league, gameId));
        }

        private void game1ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(0);

        private void game2ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(1);

        private void game3ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(2);

        private void game4ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(3);
        private void game5ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(4);

        private void game6ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(5);

        private void game7ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(6);

        private void game8ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(7);

        private void game9ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(8);

        private void game10ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(9);

        private void game11ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(10);

        private void game12ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(11);

        private void game13ViewGameResults_Click(object sender, EventArgs e) => ViewGameResultsFunction(12);

    }
}
