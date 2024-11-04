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
            InitializeComponent();
            this.league = league;
            currentDayShownNum.Value = league.CurrentDay;
            FillLabels();
        }

        private void FillLabels()
        {
            this.Hide();
            int currentDayShown = (int)currentDayShownNum.Value;
            List<string> currentDayGames = league.GetGamesForDay(currentDayShown);
            // we update the teamsLabels in the game panels
            {
                int currentGameCounter = 0;
                string team1Record = "";
                string team2Record = "";
                if (currentGameCounter == 0 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game1TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 1 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game2TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 2 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game3TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 3 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game4TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 4 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game5TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 5 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game6TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 6 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game7TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 7 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game8TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 8 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game9TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 9 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game10TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 10 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game11TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 11 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
                    game12TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
                    currentGameCounter++;
                }
                if (currentGameCounter == 12 && currentGameCounter < currentDayGames.Count)
                {
                    string[] teamsPlaying = currentDayGames[currentGameCounter].Split(',');
                    team1Record = league.GetTeamRecord(teamsPlaying[0]);
                    team2Record = league.GetTeamRecord(teamsPlaying[1]);
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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
                    if (league.CheckIfGameCompleted(currentDayShown, teamsPlaying[0], teamsPlaying[1]))
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

        private void game1SimGameButton_Click(object sender, EventArgs e)
        {
            if ((int)currentDayShownNum.Value != league.CurrentDay)
            {
                MessageBox.Show(text: "You can only simulate the current day of the season. Simulate each day before this one before simulating games here.");
            }
            else
            {
                league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][0], (int)currentDayShownNum.Value);
                string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][0].Split(',');
                UpdateGame1Panel(teamsPlaying);
            }
            
        }

        private void UpdateGame1Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game1TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game1TeamsLabel.Refresh();
            game1WatchGameButton.Hide();
            game1SimGameButton.Hide();
            game1ViewGameResults.Show();
        }

        private void game2SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][1], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][1].Split(',');
            UpdateGame2Panel(teamsPlaying);
        }

        private void UpdateGame2Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game2TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game2TeamsLabel.Refresh();
            game2WatchGameButton.Hide();
            game2SimGameButton.Hide();
            game2ViewGameResults.Show();
        }

        private void game3SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][2], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][2].Split(',');
            UpdateGame3Panel(teamsPlaying);
        }

        private void UpdateGame3Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game3TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game3TeamsLabel.Refresh();
            game3WatchGameButton.Hide();
            game3SimGameButton.Hide();
            game3ViewGameResults.Show();
        }

        private void game4SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][3], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][3].Split(',');
            UpdateGame4Panel(teamsPlaying);
        }

        private void UpdateGame4Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game4TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game4TeamsLabel.Refresh();
            game4WatchGameButton.Hide();
            game4SimGameButton.Hide();
            game4ViewGameResults.Show();
        }

        private void game5SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][4], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][4].Split(',');
            UpdateGame5Panel(teamsPlaying);
        }

        private void UpdateGame5Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game5TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game5TeamsLabel.Refresh();
            game5WatchGameButton.Hide();
            game5SimGameButton.Hide();
            game5ViewGameResults.Show();
        }

        private void game6SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][5], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][5].Split(',');
            UpdateGame6Panel(teamsPlaying);
        }

        private void UpdateGame6Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game6TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game6TeamsLabel.Refresh();
            game6WatchGameButton.Hide();
            game6SimGameButton.Hide();
            game6ViewGameResults.Show();
        }

        private void game7SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][6], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][6].Split(',');
            UpdateGame7Panel(teamsPlaying);
        }

        private void UpdateGame7Panel(string[] teamsPlaying)
        {
            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game7TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game7TeamsLabel.Refresh();
            game7WatchGameButton.Hide();
            game7SimGameButton.Hide();
            game7ViewGameResults.Show();
        }

        private void game8SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][7], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][7].Split(',');
            UpdateGame8Panel(teamsPlaying);
        }

        private void UpdateGame8Panel(string[] teamsPlaying)
        {

            string team1Record = league.GetTeamRecord(teamsPlaying[0]);
            string team2Record = league.GetTeamRecord(teamsPlaying[1]);
            game8TeamsLabel.Text = $"{teamsPlaying[0]} {team1Record} vs. {teamsPlaying[1]} {team2Record}";
            game8TeamsLabel.Refresh();
            game8WatchGameButton.Hide();
            game8SimGameButton.Hide();
            game8ViewGameResults.Show();
        }

        private void game9SimGameButton_Click(object sender, EventArgs e)
        {
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][8], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][8].Split(',');
            UpdateGame9Panel(teamsPlaying);
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
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][9], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][9].Split(',');
            UpdateGame10Panel(teamsPlaying);
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
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][10], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][10].Split(',');
            UpdateGame11Panel(teamsPlaying);
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
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][11], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][11].Split(',');
            UpdateGame12Panel(teamsPlaying);
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
            league.SimulateGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][12], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][12].Split(',');
            UpdateGame13Panel(teamsPlaying);
        }

        private void game1WatchGameButton_Click(object sender, EventArgs e)
        {
            (List<string>, List<string>) phrases = league.WatchGame(league.CurrentSchedule[(int)currentDayShownNum.Value - 1][0], (int)currentDayShownNum.Value);
            string[] teamsPlaying = league.CurrentSchedule[(int)currentDayShownNum.Value - 1][0].Split(',');
            UpdateGame1Panel(teamsPlaying);
            LoadWatchGameUserControl(new WatchGameUserControl(league, phrases.Item1, phrases.Item2, (int)currentDayShownNum.Value, teamsPlaying[0], teamsPlaying[1]));
        }

        private async void LoadWatchGameUserControl(WatchGameUserControl userControl)
        {
            // we hide the contents held in the full schedule currently
            scheduleDisplayPanel.Controls.Clear();

            scheduleDisplayPanel.Controls.Add(userControl.panel2);
            scheduleDisplayPanel.Controls.Add(userControl.panel1);
            userControl.panel2.Show();
            userControl.panel1.Show();
            scheduleDisplayPanel.Refresh();

            int commentatorCounter = 0;
            while (commentatorCounter < userControl.CommentatorPhrases.Count)
            {
                string commentatorPhrase = userControl.CommentatorPhrases[commentatorCounter];
                string currentScore = userControl.ScoreAfterEachPhrase[commentatorCounter];
                await WaitToPrintPhrase((int)userControl.playbackSpeed.Value, commentatorPhrase, currentScore);
                if (addPhrases)
                {
                    userControl.commentatorPhrasesLabel.Text = $"{CommentatorPhrase}\n\n" + userControl.commentatorPhrasesLabel.Text;
                    userControl.scoreLabel.Text = ScoreAfterPhrase;
                    commentatorCounter++;
                    addPhrases = false;
                }
                userControl.panel2.Refresh();
                userControl.panel1.Refresh();

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
            league.SimulateDay(league.CurrentSchedule[(int)currentDayShownNum.Value - 1], (int)currentDayShownNum.Value);

            // now we update all the buttons on the screen
            {
                List<string> gamesInDay = league.CurrentSchedule[(int)currentDayShownNum.Value - 1];
                if (gamesInDay.Count > 0) UpdateGame1Panel(gamesInDay[0].Split(","));
                if (gamesInDay.Count > 1) UpdateGame2Panel(gamesInDay[1].Split(","));
                if (gamesInDay.Count > 2) UpdateGame3Panel(gamesInDay[2].Split(","));
                if (gamesInDay.Count > 3) UpdateGame4Panel(gamesInDay[3].Split(","));
                if (gamesInDay.Count > 4) UpdateGame5Panel(gamesInDay[4].Split(","));
                if (gamesInDay.Count > 5) UpdateGame6Panel(gamesInDay[5].Split(","));
                if (gamesInDay.Count > 6) UpdateGame7Panel(gamesInDay[6].Split(","));
                if (gamesInDay.Count > 7) UpdateGame8Panel(gamesInDay[7].Split(","));
                if (gamesInDay.Count > 8) UpdateGame9Panel(gamesInDay[8].Split(","));
                if (gamesInDay.Count > 9) UpdateGame10Panel(gamesInDay[9].Split(","));
                if (gamesInDay.Count > 10) UpdateGame11Panel(gamesInDay[10].Split(","));
                if (gamesInDay.Count > 11) UpdateGame12Panel(gamesInDay[11].Split(","));
                if (gamesInDay.Count > 12) UpdateGame13Panel(gamesInDay[12].Split(","));
            }
        }

        // this button simulates a month within a season
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                if (i >= league.CurrentSchedule.Count) break;
                league.SimulateDay(league.CurrentSchedule[(int)currentDayShownNum.Value - 1 + i], (int)currentDayShownNum.Value + i);
            }


            // now we update all the buttons on the screen
            {
                List<string> gamesInDay = league.CurrentSchedule[(int)currentDayShownNum.Value - 1];
                if (gamesInDay.Count > 0) UpdateGame1Panel(gamesInDay[0].Split(","));
                if (gamesInDay.Count > 1) UpdateGame2Panel(gamesInDay[1].Split(","));
                if (gamesInDay.Count > 2) UpdateGame3Panel(gamesInDay[2].Split(","));
                if (gamesInDay.Count > 3) UpdateGame4Panel(gamesInDay[3].Split(","));
                if (gamesInDay.Count > 4) UpdateGame5Panel(gamesInDay[4].Split(","));
                if (gamesInDay.Count > 5) UpdateGame6Panel(gamesInDay[5].Split(","));
                if (gamesInDay.Count > 6) UpdateGame7Panel(gamesInDay[6].Split(","));
                if (gamesInDay.Count > 7) UpdateGame8Panel(gamesInDay[7].Split(","));
                if (gamesInDay.Count > 8) UpdateGame9Panel(gamesInDay[8].Split(","));
                if (gamesInDay.Count > 9) UpdateGame10Panel(gamesInDay[9].Split(","));
                if (gamesInDay.Count > 10) UpdateGame11Panel(gamesInDay[10].Split(","));
                if (gamesInDay.Count > 11) UpdateGame12Panel(gamesInDay[11].Split(","));
                if (gamesInDay.Count > 12) UpdateGame13Panel(gamesInDay[12].Split(","));
            }
        }
    }
}
