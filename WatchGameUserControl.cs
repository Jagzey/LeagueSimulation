using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeagueSimulation.Models;

namespace LeagueSimulation
{
    public partial class WatchGameUserControl : UserControl
    {
        public League league;
        public List<string> CommentatorPhrases;
        public List<string> ScoreAfterEachPhrase;
        public List<string> GameTimestamps;
        public int CurrentDayOfGame;
        public string HomeTeam;
        public string AwayTeam;
        public WatchGameUserControl(League league, List<string> commentatorPhrases, List<string> scoreAfterEachPhrase, List<string> gameTimestamps, int currentDayOfGame, string homeTeam, string awayTeam)
        {
            InitializeComponent();
            this.league = league;
            this.CommentatorPhrases = commentatorPhrases;
            this.ScoreAfterEachPhrase = scoreAfterEachPhrase;
            this.GameTimestamps = gameTimestamps;
            this.CurrentDayOfGame = currentDayOfGame;
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            FillStartingLabels();
        }

        private void FillStartingLabels()
        {
            string[] teamsPlaying = league.CurrentSchedule[CurrentDayOfGame - 1].Where(x => x.Contains(league.GetIdFromTeamName(HomeTeam).ToString())).ToList()[0].Split(",");
            if (league.Playoffs) teamsPlaying = league.CurrentPlayoffsSchedule[CurrentDayOfGame - 1].Where(x => x.Contains(league.GetIdFromTeamName(HomeTeam).ToString())).ToList()[0].Split(",");
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

        private void WatchGameUserControl_Load(object sender, EventArgs e)
        {

        }

        private void commentatorPhrasesLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
