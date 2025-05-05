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
    public partial class LeagueStandingsUserControl : UserControl
    {
        public League? league;
        public List<Label> PositionLabels;
        public LeagueStandingsUserControl(League league)
        {
            InitializeComponent();
            PositionLabels = new List<Label>()
            {
                eastFirstPositionLabel,
                eastSecondPositionLabel,
                eastThirdPositionLabel,
                eastFourthPositionLabel,
                eastFifthPositionLabel,
                eastSixthPositionLabel,
                eastSeventhPositionLabel,
                eastEighthPositionLabel,
                eastNinthPositionLabel,
                eastTenthPositionLabel,
                eastEleventhPositionLabel,
                eastTwelfthPositionLabel,
                eastThirteenthPositionLabel,
                eastFourteenthPositionLabel,
                eastFifteenthPositionLabel,

                westFirstPositionLabel,
                westSecondPositionLabel,
                westThirdPositionLabel,
                westFourthPositionLabel,
                westFifthPositionLabel,
                westSixthPositionLabel,
                westSeventhPositionLabel,
                westEighthPositionLabel,
                westNinthPositionLabel,
                westTenthPositionLabel,
                westEleventhPositionLabel,
                westTwelfthPositionLabel,
                westThirteenthPositionLabel,
                westFourteenthPositionLabel,
                westFifteenthPositionLabel,
            };
            this.league = league;
            FillPanels();
        }

        public void FillPanels()
        {
            int userConferenceId = league.GetUserConferenceId();
            // we work out the eastern conference standings in the dashboard
            {
                List<string> userConferenceTeams = league.GetConferenceTeams(1);
                for (int i = 0; i < userConferenceTeams.Count; i++)
                {
                    Label positionLabel = PositionLabels[i];
                    positionLabel.Text = $"{i + 1}. {userConferenceTeams[i]} {league.GetTeamRecord(userConferenceTeams[i])}";
                    if (userConferenceId == 1 && positionLabel.Text.Contains(league.UserTeamName))
                    {
                        positionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                }

            }

            // we work out the western conference standings in the dashboard
            {
                List<string> userConferenceTeams = league.GetConferenceTeams(2);
                for (int i = 0; i < userConferenceTeams.Count; i++)
                {
                    Label positionLabel = PositionLabels[i + 15];
                    positionLabel.Text = $"{i + 1}. {userConferenceTeams[i]} {league.GetTeamRecord(userConferenceTeams[i])}";
                    if (userConferenceId == 2 && positionLabel.Text.Contains(league.UserTeamName))
                    {
                        positionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                }
            }
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the main form again when second form is closed 
        }

        private void PositionLabelClick(int index, int conference)
        {
            int addition = 0;
            if (conference == 2) addition = 15;
            Label positionLabel = PositionLabels[index + addition];
            string teamName = GetTeamNameFromRecordLabel(positionLabel.Text);
            RosterUserControl rosterUserControl = new RosterUserControl(league);
            rosterUserControl.currentTeamRoster.Text = teamName;
            MenuForm menuForm = new MenuForm();
            menuForm.FormClosed += new FormClosedEventHandler(MenuForm_FormClosed);
            menuForm.menuFormLayoutPanel.Size = rosterUserControl.Size += new Size(50, 50);
            menuForm.Size = menuForm.menuFormLayoutPanel.Size + new Size(40, 40);
            menuForm.menuFormLayoutPanel.Controls.Add(rosterUserControl);
            this.Hide();
            menuForm.Show();
        }

        private void eastFirstPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(0, 1);
        private void eastSecondPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(1, 1);
        private void eastThirdPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(2, 1);
        private void eastFourthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(3, 1);
        private void eastFifthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(4, 1);
        private void eastSixthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(5, 1);
        private void eastSeventhPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(6, 1);
        private void eastEighthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(7, 1);
        private void eastNinthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(8, 1);
        private void eastTenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(9, 1);
        private void eastEleventhPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(10, 1);
        private void eastTwelfthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(11, 1);
        private void eastThirteenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(12, 1);
        private void eastFourteenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(13, 1);
        private void eastFifteenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(14, 1);

        private void westFirstPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(0, 2);
        private void westSecondPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(1, 2);
        private void westThirdPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(2, 2);
        private void westFourthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(3, 2);
        private void westFifthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(4, 2);
        private void westSixthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(5, 2);
        private void westSeventhPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(6, 2);
        private void westEighthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(7, 2);
        private void westNinthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(8, 2);
        private void westTenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(9, 2);
        private void westEleventhPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(10, 2);
        private void westTwelfthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(11, 2);
        private void westThirteenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(12, 2);
        private void westFourteenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(13, 2);
        private void westFifteenthPositionLabel_Click(object sender, EventArgs e) => PositionLabelClick(14, 2);

        public string GetTeamNameFromRecordLabel(string recordLabel)
        {
            string output = "";
            foreach (char c in recordLabel)
            {
                if (Char.IsUpper(c) && output != "") output += " ";
                if (Char.IsLetter(c)) output += c;
            }
            return output;
        }
    }
}
