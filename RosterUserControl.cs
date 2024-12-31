using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueSimulation
{
    public partial class RosterUserControl : UserControl
    {
        public League? league;
        public RosterUserControl(League league)
        {
            InitializeComponent();
            this.league = league;
            // set the current team, to the user's team
            currentTeamRoster.Text = league.UserTeamName;
            FillLabels();
        }

        private void FillLabels()
        {
            // fill the table with players from the database
            {
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    connection.Open();
                    string getPlayerDataQuery = $@"
                        WITH currentPlayers AS (
                        SELECT playerId, teamId
                        FROM playerOnTeam
                        WHERE dayJoined <= 1 AND yearJoined <= 2024
                        AND dayLeft >= 1 AND yearLeft >= 2024
                    )
                    SELECT 
                        p.playerForename,
                        p.playerSurname,
                        p.overall,
                        p.potential,
                        pos.positionShort AS playerPosition,
                        (l.currentSeason + 2023) - p.dateOfBirth AS age,
                        COALESCE(ROUND(AVG(pgs.MP), 1), 0.0) as MP,
                        COALESCE(ROUND(SUM(pgs.FGM) * 100 / CAST(SUM(pgs.FGA) AS REAL), 1), 0.0) AS FGPCT,
                        COALESCE(ROUND(AVG(pgs.PTS), 1), 0.0) AS PTS,
                        COALESCE(ROUND(AVG(pgs.REB), 1), 0.0) AS REB,
                        COALESCE(ROUND(AVG(pgs.AST), 1), 0.0) AS AST
                    FROM 
                        currentPlayers cp, league l
                    JOIN
                        players p ON p.playerId = cp.playerId
                    LEFT JOIN 
                        playerGameStats pgs ON p.playerId = pgs.playerId
                    JOIN 
                        teams t ON cp.teamId = t.teamId
                        AND t.teamName = '{currentTeamRoster.Text}'
                    JOIN 
                        position pos ON p.positionId = pos.positionId -- Position name lookup
    
                    GROUP BY
                        p.playerId
                    ";
                    SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                    DataTable dt = new DataTable();
                    rosterData.Fill(dt);
                    rosterDataGridView.AutoGenerateColumns = false;
                    rosterDataGridView.DataSource = dt;
                }
            }

            // fill team record label
            teamRecordLabel.Text = $"Team Record: {league.GetTeamRecord(currentTeamRoster.Text)}";
        }

        private void RosterUserControl_Load(object sender, EventArgs e)
        {

        }

        private void currentTeamRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabels();
        }

        public int GetPlayerIdFromName(string firstname, string surname)
        {
            string getPlayerIdQuery = $"SELECT p.playerId FROM players p WHERE p.playerForename = '{firstname}' AND p.playerSurname = '{surname}';";
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getPlayerIdQuery, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) return reader.GetInt32(0);
                }
            }
            return 1;
        }

        public void rosterDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // access data row we need
                DataGridViewRow dataRow = rosterDataGridView.Rows[e.RowIndex];
                // Access the correct data object from the row
                var boundData = dataRow.DataBoundItem;
                string playerForename = "";
                string playerSurname = "";
                if (boundData != null)
                {
                    DataRowView rowView = boundData as DataRowView;
                    playerForename = (string)rowView["playerForename"];
                    playerSurname = (string)rowView["playerSurname"];
                }
                // get dataTable from gridView
                DataTable dt = (DataTable)rosterDataGridView.DataSource;

                int playerId = league.GetPlayerIdFromName(playerForename, playerSurname);
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
    }
}
