using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueSimulation
{
    public partial class Form2 : Form
    {
        private int saveState = 0;
        public int SaveState { get; set; }
        private League currentLeague;
        public League CurrentLeague { get; }
        public Form2(int saveState, League loadGame)
        {
            this.SaveState = saveState;
            InitializeComponent();
            this.CurrentLeague = loadGame;
            if (!CurrentLeague.Playoffs)
            {
                LoadDashboard(new DashboardUserControl(loadGame));
                //if (CurrentLeague.GamesPlayed >= 82) CurrentLeague.GeneratePlayoffsSchedule();
            }
        }

        private void LoadDashboard(UserControl dashboardUserControl)
        {
            saveStateLabel.Text = $"Current League save state: {SaveState}";
            // clear current data in the displayPanel
            displayPanel.Controls.Clear();
            // add the dashboard user control to the display panel
            displayPanel.Controls.Add(dashboardUserControl);
        }

        private void LoadFullSchedule(FullScheduleUserControl fullScheduleUserControl)
        {
            // clear current data in the displayPanel
            displayPanel.Controls.Clear();
            // add the full schedule user control to the display panel
            displayPanel.Controls.Add(fullScheduleUserControl);
        }

        private void LoadLeagueStandings(LeagueStandingsUserControl leagueStandingsUserControl)
        {
            // clear current data in the displayPanel
            displayPanel.Controls.Clear();
            // add the full league standings control to the display panel
            displayPanel.Controls.Add(leagueStandingsUserControl);
        }

        private void LoadRoster(RosterUserControl rosterUserControl)
        {
            // clear current data in the display panel
            displayPanel.Controls.Clear();
            // add the roster control to the display panel
            displayPanel.Controls.Add(rosterUserControl);
        }

        private void LoadLeagueLeaders(LeagueLeadersUserControl leagueLeadersUserControl)
        {
            // clear current data in the display panel
            displayPanel.Controls.Clear();
            // add the league leader control to the display panel
            displayPanel.Controls.Add(leagueLeadersUserControl);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void quitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit?", "Quit Game?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // when the 'Dashboard' button is pressed, we load it's displayPage
        private void dashboardMenuItem_Click(object sender, EventArgs e)
        {
            LoadDashboard(new DashboardUserControl(CurrentLeague));
        }

        private void fullScheduleMenuItem_Click(object sender, EventArgs e)
        {
            LoadFullSchedule(new FullScheduleUserControl(CurrentLeague));
        }

        private void leagueStandingsMenuItem_Click(object sender, EventArgs e)
        {
            LoadLeagueStandings(new LeagueStandingsUserControl(CurrentLeague));
        }

        private void rosterMenuItem_Click(object sender, EventArgs e)
        {
            LoadRoster(new RosterUserControl(CurrentLeague));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void leagueLeadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLeagueLeaders(new LeagueLeadersUserControl(CurrentLeague));
        }
    }
}
