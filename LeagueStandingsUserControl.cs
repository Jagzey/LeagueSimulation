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
        public LeagueStandingsUserControl(League league)
        {
            InitializeComponent();
            this.league = league;
            FillPanels();
        }

        public void FillPanels()
        {
            int userConferenceId = league.GetUserConferenceId();
            // we work out the eastern conference standings in the dashboard
            {
                List<string> userConferenceTeams = league.GetConferenceTeams(1);

                eastFirstPositionLabel.Text = $"1. {userConferenceTeams[0]} {league.GetTeamRecord(userConferenceTeams[0])}";
                eastSecondPositionLabel.Text = $"2. {userConferenceTeams[1]} {league.GetTeamRecord(userConferenceTeams[1])}";
                eastThirdPositionLabel.Text = $"3. {userConferenceTeams[2]} {league.GetTeamRecord(userConferenceTeams[2])}";
                eastFourthPositionLabel.Text = $"4. {userConferenceTeams[3]} {league.GetTeamRecord(userConferenceTeams[3])}";
                eastFifthPositionLabel.Text = $"5. {userConferenceTeams[4]} {league.GetTeamRecord(userConferenceTeams[4])}";
                eastSixthPositionLabel.Text = $"6. {userConferenceTeams[5]} {league.GetTeamRecord(userConferenceTeams[5])}";
                eastSeventhPositionLabel.Text = $"7. {userConferenceTeams[6]} {league.GetTeamRecord(userConferenceTeams[6])}";
                eastEighthPositionLabel.Text = $"8. {userConferenceTeams[7]} {league.GetTeamRecord(userConferenceTeams[7])}";
                eastNinthPositionLabel.Text = $"9. {userConferenceTeams[8]} {league.GetTeamRecord(userConferenceTeams[8])}";
                eastTenthPositionLabel.Text = $"10. {userConferenceTeams[9]} {league.GetTeamRecord(userConferenceTeams[9])}";
                eastEleventhPositionLabel.Text = $"11. {userConferenceTeams[10]} {league.GetTeamRecord(userConferenceTeams[10])}";
                eastTwelfthPositionLabel.Text = $"12. {userConferenceTeams[11]} {league.GetTeamRecord(userConferenceTeams[11])}";
                eastThirteenthPositionLabel.Text = $"13. {userConferenceTeams[12]} {league.GetTeamRecord(userConferenceTeams[12])}";
                eastFourteenthPositionLabel.Text = $"14. {userConferenceTeams[13]} {league.GetTeamRecord(userConferenceTeams[13])}";
                eastFifteenthPositionLabel.Text = $"15. {userConferenceTeams[14]} {league.GetTeamRecord(userConferenceTeams[14])}";

                // now we make the user's team bold in the standings, if this is their conference
                if (userConferenceId == 1)
                {
                    if (eastFirstPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastFirstPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastSecondPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastSecondPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastThirdPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastThirdPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastFifthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastFifthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastSixthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastSixthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastSeventhPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastSeventhPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastEighthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastEighthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastNinthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastNinthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastTenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastTenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastEleventhPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastEleventhPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastTwelfthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastTwelfthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastThirteenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastThirteenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastFourteenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastFourteenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastFifteenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastFifteenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (eastFourthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        eastFourthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                }

            }

            // we work out the western conference standings in the dashboard
            {
                List<string> userConferenceTeams = league.GetConferenceTeams(2);

                westFirstPositionLabel.Text = $"1. {userConferenceTeams[0]} {league.GetTeamRecord(userConferenceTeams[0])}";
                westSecondPositionLabel.Text = $"2. {userConferenceTeams[1]} {league.GetTeamRecord(userConferenceTeams[1])}";
                westThirdPositionLabel.Text = $"3. {userConferenceTeams[2]} {league.GetTeamRecord(userConferenceTeams[2])}";
                westFourthPositionLabel.Text = $"4. {userConferenceTeams[3]} {league.GetTeamRecord(userConferenceTeams[3])}";
                westFifthPositionLabel.Text = $"5. {userConferenceTeams[4]} {league.GetTeamRecord(userConferenceTeams[4])}";
                westSixthPositionLabel.Text = $"6. {userConferenceTeams[5]} {league.GetTeamRecord(userConferenceTeams[5])}";
                westSeventhPositionLabel.Text = $"7. {userConferenceTeams[6]} {league.GetTeamRecord(userConferenceTeams[6])}";
                westEighthPositionLabel.Text = $"8. {userConferenceTeams[7]} {league.GetTeamRecord(userConferenceTeams[7])}";
                westNinthPositionLabel.Text = $"9. {userConferenceTeams[8]} {league.GetTeamRecord(userConferenceTeams[8])}";
                westTenthPositionLabel.Text = $"10. {userConferenceTeams[9]} {league.GetTeamRecord(userConferenceTeams[9])}";
                westEleventhPositionLabel.Text = $"11. {userConferenceTeams[10]} {league.GetTeamRecord(userConferenceTeams[10])}";
                westTwelfthPositionLabel.Text = $"12. {userConferenceTeams[11]} {league.GetTeamRecord(userConferenceTeams[11])}";
                westThirteenthPositionLabel.Text = $"13. {userConferenceTeams[12]} {league.GetTeamRecord(userConferenceTeams[12])}";
                westFourteenthPositionLabel.Text = $"14. {userConferenceTeams[13]} {league.GetTeamRecord(userConferenceTeams[13])}";
                westFifteenthPositionLabel.Text = $"15. {userConferenceTeams[14]} {league.GetTeamRecord(userConferenceTeams[14])}";

                // now we make the user's team bold in the standings, if this is their conference
                if (userConferenceId == 2)
                {
                    if (westFirstPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westFirstPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westSecondPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westSecondPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westThirdPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westThirdPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westFifthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westFifthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westSixthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westSixthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westSeventhPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westSeventhPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westEighthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westEighthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westNinthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westNinthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westTenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westTenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westEleventhPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westEleventhPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westTwelfthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westTwelfthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westThirteenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westThirteenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westFourteenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westFourteenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westFifteenthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westFifteenthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                    else if (westFourthPositionLabel.Text.Contains(league.UserTeamName))
                    {
                        westFourthPositionLabel.Font = new Font("Segoe UI", 17.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                }

            }
        }

        private void westernConferenceStandingsLabel_Click(object sender, EventArgs e)
        {

        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the main form again when second form is closed 
        }

        private void eastFirstPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastFirstPositionLabel.Text);
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



        private void eastSecondPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastSecondPositionLabel.Text);
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

        private void eastThirdPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastThirdPositionLabel.Text);

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

        private void eastFourthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastFourthPositionLabel.Text);
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

        private void eastFifthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastFifthPositionLabel.Text);
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

        private void eastSixthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastSixthPositionLabel.Text);
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

        private void eastSeventhPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastSeventhPositionLabel.Text);
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

        private void eastEighthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastEighthPositionLabel.Text);
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

        private void eastNinthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastNinthPositionLabel.Text);
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

        private void eastTenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastTenthPositionLabel.Text);
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

        private void eastEleventhPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastEleventhPositionLabel.Text);
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

        private void eastTwelfthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastTwelfthPositionLabel.Text);
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

        private void eastThirteenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastThirteenthPositionLabel.Text);
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

        private void eastFourteenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastFourteenthPositionLabel.Text);
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

        private void eastFifteenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(eastFifteenthPositionLabel.Text);
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

        private void westFirstPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westFirstPositionLabel.Text);
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

        private void westSecondPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westSecondPositionLabel.Text);
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

        private void westThirdPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westThirdPositionLabel.Text);
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

        private void westFourthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westFourthPositionLabel.Text);
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

        private void westFifthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westFifthPositionLabel.Text);
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

        private void westSixthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westSixthPositionLabel.Text);
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

        private void westSeventhPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westSeventhPositionLabel.Text);
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

        private void westEighthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westEighthPositionLabel.Text);
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

        private void westNinthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westNinthPositionLabel.Text);
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

        private void westTenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westTenthPositionLabel.Text);
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

        private void westEleventhPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westEleventhPositionLabel.Text);
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

        private void westTwelfthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westTwelfthPositionLabel.Text);
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

        private void westThirteenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westThirteenthPositionLabel.Text);
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

        private void westFourteenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westFourteenthPositionLabel.Text);
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

        private void westFifteenthPositionLabel_Click(object sender, EventArgs e)
        {
            string teamName = GetTeamNameFromRecordLabel(westFifteenthPositionLabel.Text);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void easternConferenceStandingsLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
