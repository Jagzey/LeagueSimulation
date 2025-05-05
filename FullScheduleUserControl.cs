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

            // if the day changes, 'un-bold' a label if the user's team is not displayed in that label
            foreach (Label gameTeamLabel in GameTeamLabels)
            {
                if (gameTeamLabel.Font.Bold)
                {
                    gameTeamLabel.Font = new Font(gameTeamLabel.Font, FontStyle.Regular);
                }
            }

            // we update the teamsLabels in the game panels
            for (int currentGame = 0; currentGame < currentDayGames.Count; currentGame++)
            {
                string[] teamsPlaying = currentDayGames[currentGame].Split(',');
                string team1 = league.GetTeamNameFromId(teamsPlaying[0]);
                string team2 = league.GetTeamNameFromId(teamsPlaying[1]);
                string team1Record = league.GetTeamRecord(team1);
                string team2Record = league.GetTeamRecord(team2);
                Label gameTeamLabel = GameTeamLabels[currentGame];
                Button watchGameButton = WatchGameButtons[currentGame];
                Button simGameButton = SimGameButtons[currentGame];
                Button viewGameResultsButton = ViewGameResultsButtons[currentGame];

                if (league.Playoffs)
                {
                    team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(team1).ToString(), league.GetIdFromTeamName(team2).ToString());
                    team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(team2).ToString(), league.GetIdFromTeamName(team1).ToString());
                }
                gameTeamLabel.Text = $"{team1} {team1Record} vs. {team2} {team2Record}";
                if (gameTeamLabel.Text.Contains(league.UserTeamName))
                {
                    gameTeamLabel.Font = new Font(gameTeamLabel.Font, FontStyle.Bold);
                }

                bool gameComplete = false;
                if (league.Playoffs) gameComplete = league.CheckIfPlayoffGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                else gameComplete = league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]);
                if (gameComplete)
                {
                    watchGameButton.Hide();
                    simGameButton.Hide();
                }
                else viewGameResultsButton.Hide();
            }

            int firstDayThatIsNull = currentDayGames.Count;
            for (int i = 0; i < Panels.Count; i++)
            {
                Panel currentPanel = Panels[i];
                if (i < firstDayThatIsNull) currentPanel.Show();
                else currentPanel.Hide();
            }
            this.Show();
        }

        private void currentDayShownNum_ValueChanged(object sender, EventArgs e)
        {
            // we show all the controls again, then remove the ones we don't want
            foreach (Panel panel in Panels)
            {
                foreach (Control control in panel.Controls)
                {
                    control.Show();
                }
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

        private void SimGameFunction(int gameNumber)
        {
            if ((int)currentDayShownNum.Value != league.CurrentDay)
            {
                MessageBox.Show(text: "You can only simulate the current day of the season. Simulate each day before this one before simulating games here.");
            }
            else
            {
                // fill data needed to sim a game
                int currentSeason = league.CurrentSeason;
                List<List<string>> schedule = new List<List<string>>();
                if (league.Playoffs) schedule = league.CurrentPlayoffsSchedule;
                else schedule = league.CurrentSchedule;
                int currentDay = (int)currentDayShownNum.Value;
                int currentGameSeason = league.CurrentSeason;
                string currentPlayoffsRound = "";
                if (league.Playoffs) currentPlayoffsRound = league.PlayoffsRound;

                // simulate the game
                league.SimulateGame(schedule[(int)currentDayShownNum.Value - 1][gameNumber], currentDay, true);
                string[] teamsPlaying = schedule[(int)currentDayShownNum.Value - 1][gameNumber].Split(',');
                teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);

                // update the panels and the max value of the sim days button
                UpdateGamePanel(gameNumber, teamsPlaying);
                if (league.Playoffs) currentDayShownNum.Maximum = league.CurrentPlayoffsSchedule.Count;
                else currentDayShownNum.Maximum = league.CurrentSchedule.Count;
                simToValue.Maximum = currentDayShownNum.Maximum - league.CurrentDay + 1;
                if (league.CurrentSeason > currentSeason) LoadSeasonSummary(new SeasonSummaryUserControl(league));
            }

        }

        private void game1SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(0);
        private void game2SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(1);
        private void game3SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(2);
        private void game4SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(3);
        private void game5SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(4);
        private void game6SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(5);
        private void game7SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(6);
        private void game8SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(7);
        private void game9SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(8);
        private void game10SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(9);
        private void game11SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(10);
        private void game12SimGameButton_Click(object sender, EventArgs e) => SimGameFunction(11);
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

                UpdateGamePanel(gameNumber, teamsPlaying);
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
                    for (int i = 0; i < gamesInDay.Count;i++)
                    {
                        string gameInDay = gamesInDay[i];
                        string[] teamsPlaying = gameInDay.Split(",");
                        teamsPlaying[0] = league.GetTeamNameFromId(teamsPlaying[0]);
                        teamsPlaying[1] = league.GetTeamNameFromId(teamsPlaying[1]);
                        UpdateGamePanel(i, teamsPlaying);
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
