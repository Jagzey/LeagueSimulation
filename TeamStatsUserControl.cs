using LeagueSimulation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueSimulation
{
    public partial class TeamStatsUserControl : UserControl
    {
        public League league;
        public int CurrentSeason;
		public bool Playoffs;
        public TeamStatsUserControl(League league)
        {
            InitializeComponent();
			Playoffs = false;
            this.league = league;
            seasonSummaryYear.Maximum = league.CurrentSeason + 2023;
            seasonSummaryYear.Minimum = 2024;
            seasonSummaryYear.Value = league.CurrentSeason + 2023;
            if (league.CurrentSeason == 1)
            {
                CurrentSeason = league.CurrentSeason + 2023;
                FillLabels();
            }
            seasonDataGridView.DataBindingComplete += (s, e) =>
            {
                for (int i = 0; i < seasonDataGridView.Rows.Count; i++)
                {
                    DataGridViewRow row = seasonDataGridView.Rows[i];
                    var teamName = (string)row.Cells["teamName"].Value;
                    if (teamName == league.UserTeamName)
                    {
                        seasonDataGridView.Rows[i].DefaultCellStyle.Font = new Font(seasonDataGridView.Font.FontFamily, seasonDataGridView.Font.Size - 1, FontStyle.Bold);
                    }
                    else continue;
                }
                seasonDataGridView.Refresh();
            };
        }

        private void FillLabels()
        {
            string teamDataQuery = $@"
                WITH currentPlayers AS (
				SELECT playerId, teamId
				FROM playerOnTeam pot
				WHERE ((dayJoined <= 140 AND yearJoined = {CurrentSeason}) OR (yearJoined < {CurrentSeason}))
				AND ((dayLeft >= 140 AND yearLeft >= {CurrentSeason}) OR (yearLeft > {CurrentSeason}))
				),
				teamResultsData AS(
				SELECT
				t.teamId,
				t.teamName,
				tr.wins + tr.losses AS gamesPlayed,
				tr.wins,
				tr.losses,
				CASE WHEN tr.losses = 0 THEN tr.wins 
				ELSE CAST (tr.wins AS DOUBLE) / (tr.wins + tr.losses)
				END AS winPct
				FROM teams t, league
				JOIN teamResults tr ON t.teamId = tr.teamId
				WHERE tr.seasonId = {CurrentSeason} - 2023
				GROUP BY t.teamId
				),
				gameResultsSums AS(
					SELECT
					cp.teamId,
					ROUND(AVG(currentSeason + 2023 - p.dateOfBirth), 1) as avgAge,
					ROUND(AVG(pgs.gameValue), 1) AS totalGameValue,
					SUM(pgs.FGM) AS totalFGM,
					SUM(pgs.FGA) AS totalFGA,
					SUM(pgs.TFGM) AS totalTFGM,
					SUM(pgs.TFGA) AS totalTFGA,
					SUM(pgs.FTM) AS totalFTM,
					SUM(pgs.FTA) AS totalFTA,
					SUM(pgs.PTS) AS totalPTS,
					SUM(pgs.REB) AS totalREB,
					SUM(pgs.AST) AS totalAST,
					SUM(pgs.STL) AS totalSTL,
					SUM(pgs.BLK) AS totalBLK,
					SUM(pgs.TOV) AS totalTOV,
					SUM(pgs.PF) AS totalPF
					FROM currentPlayers cp, league
					JOIN playerGameStats pgs ON cp.playerId = pgs.playerId
					JOIN players p ON cp.playerId = p.playerId
					AND pgs.seasonId = {CurrentSeason} - 2023
					GROUP BY cp.teamId, pgs.gameId, pgs.seasonId

				),
				finalResults AS (
					SELECT
					grs.teamId,
					t.teamName,
					trd.gamesPlayed,
					trd.wins,
					trd.losses,
					ROUND(100 * trd.winPct, 1) as winPct,
					grs.avgAge,
					ROUND(AVG(grs.totalGameValue), 1) as avgGameValue,
					ROUND(AVG(grs.totalFGM), 1) as avgFGM,
					ROUND(AVG(grs.totalFGA), 1) as avgFGA,
					ROUND(AVG(grs.totalFGM * 100.0 / grs.totalFGA), 1) as avgFGPCT,
					ROUND(AVG(grs.totalTFGM), 1) as avgTFGM,
					ROUND(AVG(grs.totalTFGA), 1) as avgTFGA,
					ROUND(AVG(grs.totalTFGM * 100.0 / grs.totalTFGA), 1) as avgTFGPCT,
					ROUND(AVG(grs.totalFTM), 1) as avgFTM,
					ROUND(AVG(grs.totalFTA), 1) as avgFTA,
					ROUND(AVG(grs.totalFTM * 100.0 / grs.totalFTA), 1) as avgFTPCT,
					ROUND(AVG(grs.totalPTS), 1) as avgPTS,
					ROUND(AVG(grs.totalREB), 1) as avgREB,
					ROUND(AVG(grs.totalAST), 1) as avgAST,
					ROUND(AVG(grs.totalSTL), 1) as avgSTL,
					ROUND(AVG(grs.totalBLK), 1) as avgBLK,
					ROUND(AVG(grs.totalTOV), 1) as avgTOV,
					ROUND(AVG(grs.totalPF), 1) as avgPF
	
					FROM gameResultsSums grs
					JOIN teams t ON t.teamId = grs.teamId
					JOIN teamResultsData trd ON trd.teamId = grs.teamId
					GROUP BY grs.teamId
				)
				SELECT *
				FROM finalResults fr
				ORDER BY fr.winPct DESC
				";
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                SQLiteDataAdapter rosterData = new SQLiteDataAdapter(teamDataQuery, connection);
                DataTable dt = new DataTable();
                rosterData.Fill(dt);
                seasonDataGridView.AutoGenerateColumns = false;
                seasonDataGridView.DataSource = dt;
            }


        }

        private void seasonSummaryYear_ValueChanged(object sender, EventArgs e)
        {
            CurrentSeason = (int)seasonSummaryYear.Value;
            seasonDataGridView.DataSource = null;
            seasonDataGridView.Rows.Clear();
            FillLabels();
        }

        private void seasonDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // get dataTable from gridView
                DataTable dt = (DataTable)seasonDataGridView.DataSource;

                // access data row we need
                DataRow dataRow = dt.Rows[e.RowIndex];

                string teamName = (string)dataRow["teamName"];
                RosterUserControl rosterUserControl = new RosterUserControl(league, teamName);
                MenuForm menuForm = new MenuForm();
                menuForm.FormClosed += new FormClosedEventHandler(MenuForm_FormClosed);
                menuForm.menuFormLayoutPanel.Size = rosterUserControl.Size += new Size(5, 5);
                menuForm.Size = menuForm.menuFormLayoutPanel.Size + new Size(40, 40);
                menuForm.menuFormLayoutPanel.Controls.Add(rosterUserControl);
                this.Hide();
                menuForm.Show();
            }
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the main form again when second form is closed 
        }

        private void playoffsButton_Click(object sender, EventArgs e)
        {
			if (league.Playoffs) Playoffs = true;
			else if (!league.Playoffs && CurrentSeason == league.CurrentSeason + 2023)
			{
				MessageBox.Show(text: "the playoffs have not begun yet, for this year.");
			}
        }
    }
}
