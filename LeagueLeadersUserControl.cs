using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;

namespace LeagueSimulation
{
    public partial class LeagueLeadersUserControl : UserControl
    {
        public League? league;
        public LeagueLeadersUserControl(League league)
        {
            InitializeComponent();
            this.league = league;
            this.currentStat.Text = "Points";
            FillLabels();
        }

        public void FillLabels()
        {
            if (currentStat.Text == "Points")
            {
                // fill the table with players from the database
                {
                    using (var connection = new SQLiteConnection(league.ConnectionString))
                    {
                        connection.Open();
                        string getPlayerDataQuery = $@"
                        SELECT p.playerForename, 
                        p.playerSurname,
                        t.teamName,
                        ROUND(AVG(pgs.PTS), 1) as PTS
                        FROM players p
                        JOIN
                            teams t ON t.teamId = p.teamId
                        JOIN
                            playerGameStats pgs on pgs.playerId = p.playerId
                        GROUP BY p.playerId
                        ORDER BY PTS DESC
                        LIMIT 15
                        ;";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("Points", "PTS");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "PTS";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }
            else if (currentStat.Text == "Rebounds")
            {
                // fill the table with players from the database
                {
                    using (var connection = new SQLiteConnection(league.ConnectionString))
                    {
                        connection.Open();
                        string getPlayerDataQuery = $@"
                        SELECT p.playerForename, 
                        p.playerSurname,
                        t.teamName,
                        ROUND(AVG(pgs.REB), 1) as REB
                        FROM players p
                        JOIN
                            teams t ON t.teamId = p.teamId
                        JOIN
                            playerGameStats pgs on pgs.playerId = p.playerId
                        GROUP BY p.playerId
                        ORDER BY REB DESC
                        LIMIT 15
                        ; ";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("Rebounds", "REB");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "REB";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }
            else if (currentStat.Text == "Assists")
            {
                // fill the table with players from the database
                {
                    using (var connection = new SQLiteConnection(league.ConnectionString))
                    {
                        connection.Open();
                        string getPlayerDataQuery = $@"
                        SELECT p.playerForename, 
                        p.playerSurname,
                        t.teamName,
                        ROUND(AVG(pgs.AST), 1) as AST
                        FROM players p
                        JOIN
                            teams t ON t.teamId = p.teamId
                        JOIN
                            playerGameStats pgs on pgs.playerId = p.playerId
                        GROUP BY p.playerId
                        ORDER BY AST DESC
                        LIMIT 15
                        ;";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("Assists", "AST");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "AST";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }
            else if (currentStat.Text == "Steals")
            {
                // fill the table with players from the database
                {
                    using (var connection = new SQLiteConnection(league.ConnectionString))
                    {
                        connection.Open();
                        string getPlayerDataQuery = $@"
                        SELECT p.playerForename, 
                        p.playerSurname,
                        t.teamName,
                        ROUND(AVG(pgs.STL), 1) as STL
                        FROM players p
                        JOIN
                            teams t ON t.teamId = p.teamId
                        JOIN
                            playerGameStats pgs on pgs.playerId = p.playerId
                        GROUP BY p.playerId
                        ORDER BY STL DESC
                        LIMIT 15
                        ;";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("Steals", "STL");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "STL";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }
            else if (currentStat.Text == "Turnovers")
            {
                // fill the table with players from the database
                {
                    using (var connection = new SQLiteConnection(league.ConnectionString))
                    {
                        connection.Open();
                        string getPlayerDataQuery = $@"
                        SELECT p.playerForename, 
                        p.playerSurname,
                        t.teamName,
                        ROUND(AVG(pgs.TOV), 1) as TOV
                        FROM players p
                        JOIN
                            teams t ON t.teamId = p.teamId
                        JOIN
                            playerGameStats pgs on pgs.playerId = p.playerId
                        GROUP BY p.playerId
                        ORDER BY TOV DESC
                        LIMIT 15
                        ;";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("Turnovers", "TOV");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "TOV";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }
            else if (currentStat.Text == "Blocks")
            {
                // fill the table with players from the database
                {
                    using (var connection = new SQLiteConnection(league.ConnectionString))
                    {
                        connection.Open();
                        string getPlayerDataQuery = $@"
                        SELECT p.playerForename, 
                        p.playerSurname,
                        t.teamName,
                        ROUND(AVG(pgs.BLK), 1) as BLK
                        FROM players p
                        JOIN
                            teams t ON t.teamId = p.teamId
                        JOIN
                            playerGameStats pgs on pgs.playerId = p.playerId
                        GROUP BY p.playerId
                        ORDER BY BLK DESC
                        LIMIT 15
                        ;";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("Blocks", "BLK");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "BLK";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }

        }

        public int GetPlayerIdFromName(string firstname, string surname)
        {
            string getPlayerIdQuery = $"SELECT p.playerId FROM players p WHERE p.playerForename = '{firstname}' AND p.playerSurname = '{surname}';";
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getPlayerIdQuery, connection))
                {
                    using (var reader  = command.ExecuteReader()) while (reader.Read()) return reader.GetInt32(0);
                }
            }
            return 1;
        }
        private void currentStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabels();
        }

        private void leagueLeaderDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // get dataTable from gridView
                DataTable dt = (DataTable)leagueLeaderDataGridView.DataSource;

                // access data row we need
                DataRow dataRow = dt.Rows[e.RowIndex];

                int playerId = GetPlayerIdFromName((string)dataRow["playerForename"], (string)dataRow["playerSurname"]);
                PlayerStatsUserControl playerStatsUserControl = new PlayerStatsUserControl(league, playerId);
                leagueLeadersFlowLayoutPanel.Size = new Size(630, 1500);
                leagueLeadersFlowLayoutPanel.Controls.Clear();
                leagueLeadersFlowLayoutPanel.Controls.Add(playerStatsUserControl);
                
            }

        }
    }

}


