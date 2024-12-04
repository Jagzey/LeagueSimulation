using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueSimulation
{
    public partial class WatchGameUserControl : UserControl
    {
        public League league;
        public List<string> CommentatorPhrases;
        public List<string> ScoreAfterEachPhrase;
        public int CurrentDayOfGame;
        public string HomeTeam;
        public string AwayTeam;
        public WatchGameUserControl(League league, List<string> commentatorPhrases, List<string> scoreAfterEachPhrase, int currentDayOfGame, string homeTeam, string awayTeam)
        {
            InitializeComponent();
            this.league = league;
            this.CommentatorPhrases = commentatorPhrases;
            this.ScoreAfterEachPhrase = scoreAfterEachPhrase;
            this.CurrentDayOfGame = currentDayOfGame;
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            FillStartingLabels();
        }

        private void FillStartingLabels()
        {
            string[] teamsPlaying = league.CurrentSchedule[CurrentDayOfGame - 1][0].Split(',');
            teamsPlayingLabel.Text = $"{league.GetTeamNameFromId(teamsPlaying[0])} vs. {league.GetTeamNameFromId(teamsPlaying[1])}";
            seasonDayLabel.Text += CurrentDayOfGame;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void simToEndButton_Click(object sender, EventArgs e)
        {
            playbackSpeed.Value = playbackSpeed.Maximum;
        }

        private void game1WatchGameButton_Click(object sender, EventArgs e)
        {
            playbackSpeed.Value = playbackSpeed.Minimum;
        }

        private void teamsPlayingLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
