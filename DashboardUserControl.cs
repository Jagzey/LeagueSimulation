using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using LeagueSimulation.Models;

namespace LeagueSimulation
{
    public class DashboardUserControl : UserControl
    {
        public League? league;
        public List<Label> PositionLabels;
        public DashboardUserControl(League league)
        {
            InitializeComponent();
            PositionLabels = new List<Label>()
            {
                firstPositionLabel,
                secondPositionLabel,
                thirdPositionLabel,
                fourthPositionLabel,
                fifthPositionLabel,
                sixthPositionLabel,
                seventhPositionLabel,
                eighthPositionLabel,
                ninthPositionLabel,
                tenthPositionLabel,
                eleventhPositionLabel,
                twelfthPositionLabel,
                thirteenthPositionLabel,
                fourteenthPositionLabel,
                fifteenthPositionLabel,
            };
            this.league = league;
            FillLabels();
        }

        public void FillLabels()
        {
            if (!league.Playoffs) seasonPointLabel.Text = "REGULAR \n SEASON";
            else { string round = league.PlayoffsRound; seasonPointLabel.Text = $"PLAYOFFS \n {round.ToUpper()}"; }
            // we work out the conference standings in the dashboard
            {
                List<string> userConferenceTeams = league.GetConferenceTeams(league.GetUserConferenceId());

                for (int i = 0; i < PositionLabels.Count; i++)
                {
                    Label positionLabel = PositionLabels[i];
                    positionLabel.Text = $"{i + 1}. {userConferenceTeams[i]}";
                    if (positionLabel.Text.Contains(league.UserTeamName))
                    {
                        positionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    }
                }

            }

            // we fill out the dashboard panel in the dashboard
            {
                teamNameLabel.Text = league.UserTeamName;
                teamRecordLabel.Text = league.GetTeamRecord(league.UserTeamName);
                seasonDayLabel.Text += $"{league.CurrentDay}";
                seasonYearLabel.Text += $"{league.CurrentSeason + 2023}";
                string conferencePositionText = "";
                int conferencePosition = league.GetLeaguePositionInConf(league.GetUserConferenceId(), league.UserTeamName);
                Dictionary<int, string> conferencePairs = new Dictionary<int, string>()
                {
                    {1, "1st in conference" },
                    {2, "2nd in conference" },
                    {3, "3rd in conference" }
                };
                if (conferencePosition < 4) conferencePositionText = conferencePairs[conferencePosition];
                else
                {
                    conferencePositionText = $"{conferencePosition}th in conference";
                }
                teamConfPositionLabel.Text = conferencePositionText;
            }

            // we fill out the upcoming games of the schedule
            {
                List<string> upcomingGames = league.GetTeamUpcomingGames();
                for (int i = 0; i < 3 - upcomingGames.Count; i++) upcomingGames.Add(" ");
                upcomingGame1Label.Text = upcomingGames[0];
                upcomingGame2Label.Text = upcomingGames[1];
                upcomingGame3Label.Text = upcomingGames[2];
            }

            // we update the team leaders in the dashboard
            {
                ptsLeaderLabel.Text = league.GetUserTeamLeaderInStatistic("PTS");
                rebLeaderLabel.Text = league.GetUserTeamLeaderInStatistic("REB");
                astLeaderLabel.Text = league.GetUserTeamLeaderInStatistic("AST");
            }

            // we update the team stats in the dashboard
            {
                pointsLabel.Text += league.GetTeamStatistic("PTS");
                reboundsLabel.Text += league.GetTeamStatistic("REB");
                assistsLabel.Text += league.GetTeamStatistic("AST");
                turnoversLabel.Text += league.GetTeamStatistic("TOV");
            }


            //league.userTeamName
        }



        private void InitializeComponent()
        {
            dashboardPanel = new Label();
            confStandingsLabel = new Label();
            confStandingsPanel = new Panel();
            panel2 = new Panel();
            eighthPositionLabel = new Label();
            seventhPositionLabel = new Label();
            firstPositionLabel = new Label();
            sixthPositionLabel = new Label();
            secondPositionLabel = new Label();
            fifthPositionLabel = new Label();
            thirdPositionLabel = new Label();
            fourthPositionLabel = new Label();
            nonPlayoffsPanel = new Panel();
            nonPlayoffsLabel = new Label();
            fifteenthPositionLabel = new Label();
            fourteenthPositionLabel = new Label();
            thirteenthPositionLabel = new Label();
            twelfthPositionLabel = new Label();
            eleventhPositionLabel = new Label();
            tenthPositionLabel = new Label();
            ninthPositionLabel = new Label();
            dashboardPanel1 = new Panel();
            seasonPointLabel = new Label();
            seasonYearLabel = new Label();
            seasonDayLabel = new Label();
            teamConfPositionLabel = new Label();
            teamNameLabel = new Label();
            teamRecordLabel = new Label();
            teamStatsPanel = new Panel();
            turnoversLabel = new Label();
            assistsLabel = new Label();
            reboundsLabel = new Label();
            pointsLabel = new Label();
            teamStatsLabel = new Label();
            teamLeadersPanel = new Panel();
            astLeaderLabel = new Label();
            rebLeaderLabel = new Label();
            ptsLeaderLabel = new Label();
            teamLeadersLabel = new Label();
            schedulePanel = new Panel();
            upcomingGame3Label = new Label();
            upcomingGame2Label = new Label();
            upcomingGame1Label = new Label();
            upcomingGamesLabel = new Label();
            confStandingsPanel.SuspendLayout();
            panel2.SuspendLayout();
            nonPlayoffsPanel.SuspendLayout();
            dashboardPanel1.SuspendLayout();
            teamStatsPanel.SuspendLayout();
            teamLeadersPanel.SuspendLayout();
            schedulePanel.SuspendLayout();
            SuspendLayout();
            // 
            // dashboardPanel
            // 
            dashboardPanel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dashboardPanel.Location = new Point(3, 1);
            dashboardPanel.Name = "dashboardPanel";
            dashboardPanel.Size = new Size(401, 25);
            dashboardPanel.TabIndex = 0;
            dashboardPanel.Text = "Dashboard Menu";
            dashboardPanel.TextAlign = ContentAlignment.TopCenter;
            // 
            // confStandingsLabel
            // 
            confStandingsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            confStandingsLabel.Location = new Point(3, 1);
            confStandingsLabel.Name = "confStandingsLabel";
            confStandingsLabel.Size = new Size(194, 24);
            confStandingsLabel.TabIndex = 1;
            confStandingsLabel.Text = "Conference Standings";
            confStandingsLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // confStandingsPanel
            // 
            confStandingsPanel.Controls.Add(panel2);
            confStandingsPanel.Controls.Add(nonPlayoffsPanel);
            confStandingsPanel.Location = new Point(3, 3);
            confStandingsPanel.Name = "confStandingsPanel";
            confStandingsPanel.Size = new Size(316, 634);
            confStandingsPanel.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(eighthPositionLabel);
            panel2.Controls.Add(confStandingsLabel);
            panel2.Controls.Add(seventhPositionLabel);
            panel2.Controls.Add(firstPositionLabel);
            panel2.Controls.Add(sixthPositionLabel);
            panel2.Controls.Add(secondPositionLabel);
            panel2.Controls.Add(fifthPositionLabel);
            panel2.Controls.Add(thirdPositionLabel);
            panel2.Controls.Add(fourthPositionLabel);
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(310, 216);
            panel2.TabIndex = 9;
            // 
            // eighthPositionLabel
            // 
            eighthPositionLabel.Font = new Font("Segoe UI", 12F);
            eighthPositionLabel.Location = new Point(3, 184);
            eighthPositionLabel.Name = "eighthPositionLabel";
            eighthPositionLabel.Size = new Size(194, 18);
            eighthPositionLabel.TabIndex = 15;
            eighthPositionLabel.Text = "8. ";
            // 
            // seventhPositionLabel
            // 
            seventhPositionLabel.Font = new Font("Segoe UI", 12F);
            seventhPositionLabel.Location = new Point(3, 161);
            seventhPositionLabel.Name = "seventhPositionLabel";
            seventhPositionLabel.Size = new Size(194, 23);
            seventhPositionLabel.TabIndex = 14;
            seventhPositionLabel.Text = "7. ";
            // 
            // firstPositionLabel
            // 
            firstPositionLabel.Font = new Font("Segoe UI", 12F);
            firstPositionLabel.Location = new Point(3, 25);
            firstPositionLabel.Name = "firstPositionLabel";
            firstPositionLabel.Size = new Size(194, 23);
            firstPositionLabel.TabIndex = 8;
            firstPositionLabel.Text = "1. New York vs Miami";
            // 
            // sixthPositionLabel
            // 
            sixthPositionLabel.Font = new Font("Segoe UI", 12F);
            sixthPositionLabel.Location = new Point(3, 138);
            sixthPositionLabel.Name = "sixthPositionLabel";
            sixthPositionLabel.Size = new Size(194, 23);
            sixthPositionLabel.TabIndex = 13;
            sixthPositionLabel.Text = "6. ";
            // 
            // secondPositionLabel
            // 
            secondPositionLabel.Font = new Font("Segoe UI", 12F);
            secondPositionLabel.Location = new Point(3, 46);
            secondPositionLabel.Name = "secondPositionLabel";
            secondPositionLabel.Size = new Size(194, 23);
            secondPositionLabel.TabIndex = 9;
            secondPositionLabel.Text = "2. ";
            // 
            // fifthPositionLabel
            // 
            fifthPositionLabel.Font = new Font("Segoe UI", 12F);
            fifthPositionLabel.Location = new Point(3, 115);
            fifthPositionLabel.Name = "fifthPositionLabel";
            fifthPositionLabel.Size = new Size(194, 23);
            fifthPositionLabel.TabIndex = 12;
            fifthPositionLabel.Text = "5. ";
            // 
            // thirdPositionLabel
            // 
            thirdPositionLabel.Font = new Font("Segoe UI", 12F);
            thirdPositionLabel.Location = new Point(3, 69);
            thirdPositionLabel.Name = "thirdPositionLabel";
            thirdPositionLabel.Size = new Size(194, 23);
            thirdPositionLabel.TabIndex = 10;
            thirdPositionLabel.Text = "3. ";
            // 
            // fourthPositionLabel
            // 
            fourthPositionLabel.Font = new Font("Segoe UI", 12F);
            fourthPositionLabel.Location = new Point(3, 92);
            fourthPositionLabel.Name = "fourthPositionLabel";
            fourthPositionLabel.Size = new Size(194, 23);
            fourthPositionLabel.TabIndex = 11;
            fourthPositionLabel.Text = "4. ";
            // 
            // nonPlayoffsPanel
            // 
            nonPlayoffsPanel.Controls.Add(nonPlayoffsLabel);
            nonPlayoffsPanel.Controls.Add(fifteenthPositionLabel);
            nonPlayoffsPanel.Controls.Add(fourteenthPositionLabel);
            nonPlayoffsPanel.Controls.Add(thirteenthPositionLabel);
            nonPlayoffsPanel.Controls.Add(twelfthPositionLabel);
            nonPlayoffsPanel.Controls.Add(eleventhPositionLabel);
            nonPlayoffsPanel.Controls.Add(tenthPositionLabel);
            nonPlayoffsPanel.Controls.Add(ninthPositionLabel);
            nonPlayoffsPanel.Location = new Point(3, 225);
            nonPlayoffsPanel.Name = "nonPlayoffsPanel";
            nonPlayoffsPanel.Size = new Size(310, 210);
            nonPlayoffsPanel.TabIndex = 3;
            // 
            // nonPlayoffsLabel
            // 
            nonPlayoffsLabel.Font = new Font("Segoe UI", 12F);
            nonPlayoffsLabel.Location = new Point(3, 3);
            nonPlayoffsLabel.Name = "nonPlayoffsLabel";
            nonPlayoffsLabel.Size = new Size(191, 23);
            nonPlayoffsLabel.TabIndex = 17;
            nonPlayoffsLabel.Text = "Non-Playoff Teams:";
            // 
            // fifteenthPositionLabel
            // 
            fifteenthPositionLabel.Font = new Font("Segoe UI", 12F);
            fifteenthPositionLabel.Location = new Point(3, 180);
            fifteenthPositionLabel.Name = "fifteenthPositionLabel";
            fifteenthPositionLabel.Size = new Size(191, 23);
            fifteenthPositionLabel.TabIndex = 16;
            fifteenthPositionLabel.Text = "15. ";
            // 
            // fourteenthPositionLabel
            // 
            fourteenthPositionLabel.Font = new Font("Segoe UI", 12F);
            fourteenthPositionLabel.Location = new Point(3, 154);
            fourteenthPositionLabel.Name = "fourteenthPositionLabel";
            fourteenthPositionLabel.Size = new Size(191, 23);
            fourteenthPositionLabel.TabIndex = 15;
            fourteenthPositionLabel.Text = "14.";
            // 
            // thirteenthPositionLabel
            // 
            thirteenthPositionLabel.Font = new Font("Segoe UI", 12F);
            thirteenthPositionLabel.Location = new Point(3, 131);
            thirteenthPositionLabel.Name = "thirteenthPositionLabel";
            thirteenthPositionLabel.Size = new Size(191, 23);
            thirteenthPositionLabel.TabIndex = 14;
            thirteenthPositionLabel.Text = "13. ";
            // 
            // twelfthPositionLabel
            // 
            twelfthPositionLabel.Font = new Font("Segoe UI", 12F);
            twelfthPositionLabel.Location = new Point(3, 108);
            twelfthPositionLabel.Name = "twelfthPositionLabel";
            twelfthPositionLabel.Size = new Size(191, 23);
            twelfthPositionLabel.TabIndex = 13;
            twelfthPositionLabel.Text = "12. ";
            // 
            // eleventhPositionLabel
            // 
            eleventhPositionLabel.Font = new Font("Segoe UI", 12F);
            eleventhPositionLabel.Location = new Point(3, 85);
            eleventhPositionLabel.Name = "eleventhPositionLabel";
            eleventhPositionLabel.Size = new Size(191, 23);
            eleventhPositionLabel.TabIndex = 12;
            eleventhPositionLabel.Text = "11. ";
            // 
            // tenthPositionLabel
            // 
            tenthPositionLabel.Font = new Font("Segoe UI", 12F);
            tenthPositionLabel.Location = new Point(3, 62);
            tenthPositionLabel.Name = "tenthPositionLabel";
            tenthPositionLabel.Size = new Size(191, 23);
            tenthPositionLabel.TabIndex = 11;
            tenthPositionLabel.Text = "10. ";
            // 
            // ninthPositionLabel
            // 
            ninthPositionLabel.Font = new Font("Segoe UI", 12F);
            ninthPositionLabel.Location = new Point(3, 39);
            ninthPositionLabel.Name = "ninthPositionLabel";
            ninthPositionLabel.Size = new Size(180, 23);
            ninthPositionLabel.TabIndex = 10;
            ninthPositionLabel.Text = "9. ";
            // 
            // dashboardPanel1
            // 
            dashboardPanel1.Controls.Add(seasonPointLabel);
            dashboardPanel1.Controls.Add(seasonYearLabel);
            dashboardPanel1.Controls.Add(seasonDayLabel);
            dashboardPanel1.Controls.Add(teamConfPositionLabel);
            dashboardPanel1.Controls.Add(teamNameLabel);
            dashboardPanel1.Controls.Add(teamRecordLabel);
            dashboardPanel1.Controls.Add(dashboardPanel);
            dashboardPanel1.Location = new Point(361, 7);
            dashboardPanel1.Name = "dashboardPanel1";
            dashboardPanel1.Size = new Size(407, 490);
            dashboardPanel1.TabIndex = 4;
            // 
            // seasonPointLabel
            // 
            seasonPointLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            seasonPointLabel.Location = new Point(123, 375);
            seasonPointLabel.Name = "seasonPointLabel";
            seasonPointLabel.Size = new Size(170, 83);
            seasonPointLabel.TabIndex = 12;
            seasonPointLabel.Text = "PLAYOFFS";
            seasonPointLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // seasonYearLabel
            // 
            seasonYearLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            seasonYearLabel.Location = new Point(3, 38);
            seasonYearLabel.Name = "seasonYearLabel";
            seasonYearLabel.Size = new Size(401, 43);
            seasonYearLabel.TabIndex = 11;
            seasonYearLabel.Text = "Season Year: ";
            seasonYearLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // seasonDayLabel
            // 
            seasonDayLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            seasonDayLabel.Location = new Point(3, 102);
            seasonDayLabel.Name = "seasonDayLabel";
            seasonDayLabel.Size = new Size(401, 39);
            seasonDayLabel.TabIndex = 10;
            seasonDayLabel.Text = "Season Day: ";
            seasonDayLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // teamConfPositionLabel
            // 
            teamConfPositionLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamConfPositionLabel.Location = new Point(6, 306);
            teamConfPositionLabel.Name = "teamConfPositionLabel";
            teamConfPositionLabel.Size = new Size(398, 49);
            teamConfPositionLabel.TabIndex = 10;
            teamConfPositionLabel.Text = "15th in conference";
            teamConfPositionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // teamNameLabel
            // 
            teamNameLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamNameLabel.Location = new Point(6, 178);
            teamNameLabel.Name = "teamNameLabel";
            teamNameLabel.Size = new Size(398, 37);
            teamNameLabel.TabIndex = 9;
            teamNameLabel.Text = "New York Bankers";
            teamNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // teamRecordLabel
            // 
            teamRecordLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamRecordLabel.Location = new Point(6, 245);
            teamRecordLabel.Name = "teamRecordLabel";
            teamRecordLabel.Size = new Size(398, 35);
            teamRecordLabel.TabIndex = 8;
            teamRecordLabel.Text = "0-0";
            teamRecordLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // teamStatsPanel
            // 
            teamStatsPanel.Controls.Add(turnoversLabel);
            teamStatsPanel.Controls.Add(assistsLabel);
            teamStatsPanel.Controls.Add(reboundsLabel);
            teamStatsPanel.Controls.Add(pointsLabel);
            teamStatsPanel.Controls.Add(teamStatsLabel);
            teamStatsPanel.Location = new Point(826, 8);
            teamStatsPanel.Name = "teamStatsPanel";
            teamStatsPanel.Size = new Size(254, 197);
            teamStatsPanel.TabIndex = 5;
            // 
            // turnoversLabel
            // 
            turnoversLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            turnoversLabel.Location = new Point(3, 154);
            turnoversLabel.Name = "turnoversLabel";
            turnoversLabel.Size = new Size(200, 34);
            turnoversLabel.TabIndex = 5;
            turnoversLabel.Text = "Turnovers: ";
            // 
            // assistsLabel
            // 
            assistsLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            assistsLabel.Location = new Point(3, 109);
            assistsLabel.Name = "assistsLabel";
            assistsLabel.Size = new Size(200, 35);
            assistsLabel.TabIndex = 4;
            assistsLabel.Text = "Assists: ";
            // 
            // reboundsLabel
            // 
            reboundsLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            reboundsLabel.Location = new Point(3, 69);
            reboundsLabel.Name = "reboundsLabel";
            reboundsLabel.Size = new Size(200, 36);
            reboundsLabel.TabIndex = 3;
            reboundsLabel.Text = "Rebounds: ";
            // 
            // pointsLabel
            // 
            pointsLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pointsLabel.Location = new Point(3, 27);
            pointsLabel.Name = "pointsLabel";
            pointsLabel.Size = new Size(200, 41);
            pointsLabel.TabIndex = 2;
            pointsLabel.Text = "Points: ";
            pointsLabel.Click += pointsLabel_Click;
            // 
            // teamStatsLabel
            // 
            teamStatsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            teamStatsLabel.Location = new Point(3, 0);
            teamStatsLabel.Name = "teamStatsLabel";
            teamStatsLabel.Size = new Size(200, 21);
            teamStatsLabel.TabIndex = 1;
            teamStatsLabel.Text = "Team Stats";
            teamStatsLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // teamLeadersPanel
            // 
            teamLeadersPanel.Controls.Add(astLeaderLabel);
            teamLeadersPanel.Controls.Add(rebLeaderLabel);
            teamLeadersPanel.Controls.Add(ptsLeaderLabel);
            teamLeadersPanel.Controls.Add(teamLeadersLabel);
            teamLeadersPanel.Location = new Point(826, 209);
            teamLeadersPanel.Name = "teamLeadersPanel";
            teamLeadersPanel.Size = new Size(254, 193);
            teamLeadersPanel.TabIndex = 6;
            // 
            // astLeaderLabel
            // 
            astLeaderLabel.AutoSize = true;
            astLeaderLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            astLeaderLabel.Location = new Point(3, 150);
            astLeaderLabel.Name = "astLeaderLabel";
            astLeaderLabel.Size = new Size(177, 25);
            astLeaderLabel.TabIndex = 8;
            astLeaderLabel.Text = "Bill Russell: 24.6 reb";
            // 
            // rebLeaderLabel
            // 
            rebLeaderLabel.AutoSize = true;
            rebLeaderLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rebLeaderLabel.Location = new Point(3, 89);
            rebLeaderLabel.Name = "rebLeaderLabel";
            rebLeaderLabel.Size = new Size(177, 25);
            rebLeaderLabel.TabIndex = 7;
            rebLeaderLabel.Text = "Trae Young: 11.2 ast";
            // 
            // ptsLeaderLabel
            // 
            ptsLeaderLabel.AutoSize = true;
            ptsLeaderLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ptsLeaderLabel.Location = new Point(3, 32);
            ptsLeaderLabel.Name = "ptsLeaderLabel";
            ptsLeaderLabel.Size = new Size(214, 25);
            ptsLeaderLabel.TabIndex = 6;
            ptsLeaderLabel.Text = "Michael Jordan: 30.5 pts";
            // 
            // teamLeadersLabel
            // 
            teamLeadersLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            teamLeadersLabel.Location = new Point(3, 1);
            teamLeadersLabel.Name = "teamLeadersLabel";
            teamLeadersLabel.Size = new Size(197, 21);
            teamLeadersLabel.TabIndex = 1;
            teamLeadersLabel.Text = "Team Leaders";
            teamLeadersLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // schedulePanel
            // 
            schedulePanel.Controls.Add(upcomingGame3Label);
            schedulePanel.Controls.Add(upcomingGame2Label);
            schedulePanel.Controls.Add(upcomingGame1Label);
            schedulePanel.Controls.Add(upcomingGamesLabel);
            schedulePanel.Location = new Point(774, 408);
            schedulePanel.Name = "schedulePanel";
            schedulePanel.Size = new Size(306, 229);
            schedulePanel.TabIndex = 8;
            // 
            // upcomingGame3Label
            // 
            upcomingGame3Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            upcomingGame3Label.Location = new Point(3, 185);
            upcomingGame3Label.Name = "upcomingGame3Label";
            upcomingGame3Label.Size = new Size(303, 29);
            upcomingGame3Label.TabIndex = 9;
            upcomingGame3Label.Text = "New York @ Los Angeles";
            // 
            // upcomingGame2Label
            // 
            upcomingGame2Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            upcomingGame2Label.Location = new Point(3, 111);
            upcomingGame2Label.Name = "upcomingGame2Label";
            upcomingGame2Label.Size = new Size(300, 32);
            upcomingGame2Label.TabIndex = 8;
            upcomingGame2Label.Text = "New York @ Salt Lake City";
            // 
            // upcomingGame1Label
            // 
            upcomingGame1Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            upcomingGame1Label.Location = new Point(3, 43);
            upcomingGame1Label.Name = "upcomingGame1Label";
            upcomingGame1Label.Size = new Size(300, 32);
            upcomingGame1Label.TabIndex = 7;
            upcomingGame1Label.Text = "New York vs Miami";
            // 
            // upcomingGamesLabel
            // 
            upcomingGamesLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            upcomingGamesLabel.Location = new Point(3, 6);
            upcomingGamesLabel.Name = "upcomingGamesLabel";
            upcomingGamesLabel.Size = new Size(197, 21);
            upcomingGamesLabel.TabIndex = 1;
            upcomingGamesLabel.Text = "Upcoming Games";
            upcomingGamesLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // DashboardUserControl
            // 
            Controls.Add(schedulePanel);
            Controls.Add(teamLeadersPanel);
            Controls.Add(teamStatsPanel);
            Controls.Add(dashboardPanel1);
            Controls.Add(confStandingsPanel);
            Name = "DashboardUserControl";
            Size = new Size(1100, 640);
            confStandingsPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            nonPlayoffsPanel.ResumeLayout(false);
            dashboardPanel1.ResumeLayout(false);
            teamStatsPanel.ResumeLayout(false);
            teamLeadersPanel.ResumeLayout(false);
            teamLeadersPanel.PerformLayout();
            schedulePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Label? confStandingsLabel;
        private Panel? confStandingsPanel;
        private Panel dashboardPanel1;
        private Panel schedulePanel;
        private Label upcomingGamesLabel;
        private Panel teamStatsPanel;
        private Label teamStatsLabel;
        private Panel teamLeadersPanel;
        private Label teamLeadersLabel;
        private Label teamRecordLabel;
        private Label teamNameLabel;
        private Label teamConfPositionLabel;
        private Label turnoversLabel;
        private Label assistsLabel;
        private Label reboundsLabel;
        private Label pointsLabel;
        private Label astLeaderLabel;
        private Label rebLeaderLabel;
        private Label ptsLeaderLabel;
        private Label upcomingGame1Label;
        private Panel nonPlayoffsPanel;
        private Label firstPositionLabel;
        private Label upcomingGame3Label;
        private Label upcomingGame2Label;
        private Label eighthPositionLabel;
        private Label seventhPositionLabel;
        private Label sixthPositionLabel;
        private Label fifthPositionLabel;
        private Label fourthPositionLabel;
        private Label thirdPositionLabel;
        private Label secondPositionLabel;
        private Label fifteenthPositionLabel;
        private Label fourteenthPositionLabel;
        private Label thirteenthPositionLabel;
        private Label twelfthPositionLabel;
        private Label eleventhPositionLabel;
        private Label tenthPositionLabel;
        private Label ninthPositionLabel;
        private Label nonPlayoffsLabel;
        private Panel panel2;
        private Label seasonDayLabel;
        private Label seasonYearLabel;
        private Label seasonPointLabel;
        private Label? dashboardPanel;

        private void pointsLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
