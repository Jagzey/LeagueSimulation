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
    public partial class PlayerFinderUserControl : UserControl
    {
        public League league;
        public PlayerFinderUserControl(League league)
        {
            this.league = league;
            InitializeComponent();
            teamNameTextBox.Text = league.UserTeamName;
        }

        public void leagueLeaderDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // get dataTable from gridView
                DataTable dt = (DataTable)playerFinderDataGridView.DataSource;

                // access data row we need
                DataRow dataRow = dt.Rows[e.RowIndex];

                int playerId = league.GetPlayerIdFromName((string)dataRow["playerForename"], (string)dataRow["playerSurname"]);
                PlayerStatsUserControl playerStatsUserControl = new PlayerStatsUserControl(league, playerId);
                MenuForm menuForm = new MenuForm();
                menuForm.FormClosed += new FormClosedEventHandler(MenuForm_FormClosed);
                menuForm.menuFormLayoutPanel.Size = playerStatsUserControl.Size += new Size(5, 5);
                menuForm.Size = menuForm.menuFormLayoutPanel.Size + new Size(40, 40);
                menuForm.menuFormLayoutPanel.Controls.Add(playerStatsUserControl);
                this.Hide();
                menuForm.Show();
            }
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the main form again when second form is closed 
        }

        private void playerFinderButton_Click(object sender, EventArgs e)
        {
            bool validSearch = true;
            foreach (char c in playerNameTextBox.Text)
            {
                if (!Char.IsLetter(c) && c != ' ' && c != '-') validSearch = false;
            }
            foreach (char c in teamNameTextBox.Text)
            {
                if (!Char.IsLetter(c) && c != ' ' && c != '-') validSearch = false;
            }
            if (validSearch)
            {
                // here, we need to check whether players exist within the user's specified criteria
                string teamName = "";
                string playerForename = "";
                string playerSurname = "";
                string playerPlaystyle = "";
                string playerPosition = "";
                if (playerNameTextBox.Text == "")
                {
                    playerForename = "%"; playerSurname = "%";
                }
                else
                {
                    playerForename = playerNameTextBox.Text.Split(' ')[0];
                    playerSurname = playerNameTextBox.Text.Split(' ')[1];
                }

                if (teamNameTextBox.Text == "") teamName = "%";
                else teamName = teamNameTextBox.Text;

                if (playerPlaystyleTextBox.Text == "") playerPlaystyle = "%";
                else playerPlaystyle = playerPlaystyleTextBox.Text;

                if (playerPositionTextBox.Text == "") playerPosition = "%";
                else playerPosition = playerPositionTextBox.Text;

                string findPlayersQuery = $@"
                SELECT p.playerForename, 
                p.playerSurname,
                t.teamName,
                pos.positionShort AS playerPosition,
                printf('%d''%d', p.height / 12, p.height % 12) AS realHeight,
                p.weight,
                sp.playstyle as playerPlaystyle,
                p.overall,
                p.potential,
                (({league.CurrentSeason} + 2023) - p.dateOfBirth) AS age,
                COALESCE(ROUND(AVG(pgs.PTS), 1), 0.0) AS avgPTS,
                COALESCE(ROUND(AVG(pgs.REB), 1), 0.0) AS avgREB,
                COALESCE(ROUND(AVG(pgs.AST), 1), 0.0) AS avgAST,
                COALESCE(ROUND(AVG(pgs.STL), 1), 0.0) AS avgSTL,
                COALESCE(ROUND(AVG(pgs.BLK), 1), 0.0) AS avgBLK,
                COALESCE(ROUND(AVG(pgs.TOV), 1), 0.0) AS avgTOV,
                COALESCE(ROUND(100 * AVG(pgs.FGM) / AVG(pgs.FGA), 1), 0) AS avgFGPCT,
                COALESCE(ROUND(100 * AVG(pgs.TFGM) / AVG(pgs.TFGA), 1), 0) AS avgTFGPCT
                FROM players p, league
                JOIN playerOnTeam pot 
                    ON ((dayJoined <= league.CurrentDay 
                    AND yearJoined = league.CurrentSeason + 2023) OR (yearJoined < league.CurrentSeason + 2023))
                    AND dayLeft >= league.CurrentDay 
                    AND yearLeft >= league.CurrentSeason + 2023
                    AND pot.playerId = p.playerId
                JOIN teams t ON t.teamId = pot.teamId
                JOIN playerGameStats pgs 
                    ON pgs.playerId = p.playerId
                    AND pgs.seasonId = league.CurrentSeason
                JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                JOIN position pos ON pos.positionId = p.positionId
                WHERE p.height BETWEEN {minHeightUpDown.Value} AND {maxHeightUpDown.Value}
                AND p.weight BETWEEN {minWeightUpDown.Value} AND {maxWeightUpDown.Value}
                AND p.playerForename LIKE @playerForename
                AND p.playerSurname LIKE @playerSurname
                AND t.teamName LIKE @teamName
                AND sp.playstyle LIKE @playerPlaystyle
                AND (pos.positionShort LIKE @playerPosition OR pos.positionName LIKE @playerPosition)
                GROUP BY p.playerId
                HAVING avgPTS BETWEEN {minPPGUpDown.Value} AND {maxPPGUpDown.Value}
                   AND avgREB BETWEEN {minRPGUpDown.Value} AND {maxRPGUpDown.Value}
                   AND avgAST BETWEEN {minAPGUpDown.Value} AND {maxAPGUpDown.Value}
                ORDER BY avgPTS DESC;
                ";
                if (league.Playoffs)
                {
                    findPlayersQuery = $@"
                SELECT p.playerForename, 
                p.playerSurname,
                t.teamName,
                pos.positionShort AS playerPosition,
                printf('%d''%d', p.height / 12, p.height % 12) AS realHeight,
                p.weight,
                sp.playstyle as playerPlaystyle,
                p.overall,
                p.potential,
                (({league.CurrentSeason} + 2023) - p.dateOfBirth) AS age,
                COALESCE(ROUND(AVG(pgs.PTS), 1), 0) AS avgPTS,
                COALESCE(ROUND(AVG(pgs.REB), 1), 0) AS avgREB,
                COALESCE(ROUND(AVG(pgs.AST), 1), 0) AS avgAST,
                COALESCE(ROUND(AVG(pgs.STL), 1), 0) AS avgSTL,
                COALESCE(ROUND(AVG(pgs.BLK), 1), 0) AS avgBLK,
                COALESCE(ROUND(AVG(pgs.TOV), 1), 0) AS avgTOV,
                COALESCE(ROUND(100 * AVG(pgs.FGM) / AVG(pgs.FGA), 1), 0) AS avgFGPCT,
                COALESCE(ROUND(100 * AVG(pgs.TFGM) / AVG(pgs.TFGA), 1), 0) AS avgTFGPCT
                FROM players p, league
                JOIN playerOnTeam pot 
                    ON ((dayJoined <= 150
                    AND yearJoined = league.CurrentSeason + 2023) OR (yearJoined < league.CurrentSeason + 2023))
                    AND dayLeft >= league.CurrentDay 
                    AND yearLeft >= league.CurrentSeason + 2023
                    AND pot.playerId = p.playerId
                JOIN teams t ON t.teamId = pot.teamId
                JOIN playerGameStats pgs 
                    ON pgs.playerId = p.playerId
                    AND pgs.seasonId = league.CurrentSeason
                JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                JOIN position pos ON pos.positionId = p.positionId
                WHERE p.height BETWEEN {minHeightUpDown.Value} AND {maxHeightUpDown.Value}
                AND p.weight BETWEEN {minWeightUpDown.Value} AND {maxWeightUpDown.Value}
                AND p.playerForename LIKE @playerForename
                AND p.playerSurname LIKE @playerSurname
                AND t.teamName LIKE @teamName
                AND sp.playstyle LIKE @playerPlaystyle
                AND (pos.positionShort LIKE @playerPosition OR pos.positionName LIKE @playerPosition)
                GROUP BY p.playerId
                HAVING avgPTS BETWEEN {minPPGUpDown.Value} AND {maxPPGUpDown.Value}
                   AND avgREB BETWEEN {minRPGUpDown.Value} AND {maxRPGUpDown.Value}
                   AND avgAST BETWEEN {minAPGUpDown.Value} AND {maxAPGUpDown.Value}
                ORDER BY avgPTS DESC;
                ";
                }
                int rowCount = 0;
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    SQLiteDataAdapter rosterData = new SQLiteDataAdapter(findPlayersQuery, connection);
                    rosterData.SelectCommand.Parameters.AddWithValue("@playerForename", playerForename);
                    rosterData.SelectCommand.Parameters.AddWithValue("@playerSurname", playerSurname);
                    rosterData.SelectCommand.Parameters.AddWithValue("@teamName", teamName);
                    rosterData.SelectCommand.Parameters.AddWithValue("@playerPlaystyle", playerPlaystyle);
                    rosterData.SelectCommand.Parameters.AddWithValue("@playerPosition", playerPosition);

                    DataTable dt = new DataTable();
                    rosterData.Fill(dt);
                    playerFinderDataGridView.AutoGenerateColumns = false;
                    playerFinderDataGridView.DataSource = dt;
                    rowCount = playerFinderDataGridView.Rows.Count;
                }
                if (rowCount == 0)
                {
                    MessageBox.Show(text: "No players matched the search criteria. Try and modify the search criteria by leaving some blank or increasing the range.");
                }
                else if (rowCount > 0)
                {
                    MessageBox.Show(text: $"{rowCount} player(s) matched the search criteria.");
                }
            }
            else
            {
                MessageBox.Show(text: "The player, team, position or playstyle names contain invalid characters. Try again.");
            }
            if (playerNameTextBox.Text == "%% %%") playerNameTextBox.Text = "";
            if (teamNameTextBox.Text == "%%") teamNameTextBox.Text = "";

        }

        private void minHeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            maxHeightUpDown.Minimum = minHeightUpDown.Value;
            minHeightLabel.Text = $"{(int)minHeightUpDown.Value / 12}'{minHeightUpDown.Value % 12}";
        }

        private void maxHeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            maxHeightLabel.Text = $"{(int)maxHeightUpDown.Value / 12}'{maxHeightUpDown.Value % 12}";
        }
    }
}
