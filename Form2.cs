using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeagueSimulation.Models;

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
            LoadDashboard(new DashboardUserControl(loadGame));
            FileInfo fileInfo = new FileInfo(Main.GetLeagueFileName(saveState));
            DateTime dt = fileInfo.LastWriteTime;
            dateLastModifiedLabel.Text += $"{dt.Day}{GetDaySuffix(dt.Day)} {dt:MMMM yyyy HH:mm}";
        }

        private static string GetDaySuffix(int day)
        {
            if (day > 3) return "th";
            else if (day > 2) return "rd";
            else if (day > 1) return "nd";
            else return "st";
        }

        private void LoadDashboard(DashboardUserControl dashboardUserControl)
        {
            saveStateLabel.Text = $"Current save state: {SaveState}";
            label1.Text = $"User Team: {CurrentLeague.UserTeamName}";
            // clear current data in the displayPanel
            displayPanel.Controls.Clear();
            // add the dashboard user control to the display panel
            displayPanel.Controls.Add(dashboardUserControl);
        }

        private void LoadUserControl(UserControl userControl)
        {
            displayPanel.Controls.Clear();
            displayPanel.Controls.Add(userControl);
        }

        private void LoadSeperateUserControl(UserControl userControl)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.FormClosed += new FormClosedEventHandler(MenuForm_FormClosed);
            menuForm.menuFormLayoutPanel.Size = userControl.Size + new Size(10, 10);
            menuForm.Size = userControl.Size + new Size(40, 40);
            menuForm.menuFormLayoutPanel.Controls.Add(userControl);
            this.Hide();
            menuForm.Show();
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
            LoadUserControl(new FullScheduleUserControl(CurrentLeague));
        }

        private void leagueStandingsMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new LeagueStandingsUserControl(CurrentLeague));
        }

        private void rosterMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new RosterUserControl(CurrentLeague));
        }

        private void leagueLeadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new LeagueLeadersUserControl(CurrentLeague));
        }

        // playoffs menu click
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (CurrentLeague.Playoffs == false)
            {
                MessageBox.Show(text: "The playoffs haven't started yet. Come back when the regular season finishes");
            }
            else LoadUserControl(new PlayoffsUserControl(CurrentLeague));
        }

        private void playerStatsMenuItem_Click(object sender, EventArgs e)
        {
            LoadSeperateUserControl(new PlayerFinderUserControl(CurrentLeague));
        }

        private void seasonSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentLeague.CurrentSeason > 1) LoadSeperateUserControl(new SeasonSummaryUserControl(CurrentLeague));
            else MessageBox.Show(text: "No season has finished yet. Come back when a full season finishes");
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the main form again when second form is closed 
        }

        private void tradeProposalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSeperateUserControl(new TradeProposalUserControl(CurrentLeague));
        }

        private void teamStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSeperateUserControl(new TeamStatsUserControl(CurrentLeague));
        }
    }
}
