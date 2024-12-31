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
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
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
                        leagueLeaderDataGridView.Columns[3].Width = 50;
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
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
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
                        leagueLeaderDataGridView.Columns[3].Width = 50;
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
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
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
                        leagueLeaderDataGridView.Columns[3].Width = 50;
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
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
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
                        leagueLeaderDataGridView.Columns[3].Width = 50;
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
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
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
                        leagueLeaderDataGridView.Columns[3].Width = 50;
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
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
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
                        leagueLeaderDataGridView.Columns[3].Width = 50;
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }
            else if (currentStat.Text == "Game Value")
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
                        ROUND(AVG(pgs.gameValue), 1) as gameValue
                        FROM players p
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
                        JOIN
                            playerGameStats pgs on pgs.playerId = p.playerId
                        GROUP BY p.playerId
                        ORDER BY gameValue DESC
                        LIMIT 15
                        ;";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("gameValue", "Game Value");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "gameValue";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }
            else if (currentStat.Text == "Defense Value")
            {
                // fill the table with players from the database
                {
                    using (var connection = new SQLiteConnection(league.ConnectionString))
                    {
                        connection.Open();
                        string getPlayerDataQuery = $@"
                        WITH playerData AS (SELECT 
                        p.playerId,
                        p.playerForename,
                        p.playerSurname,
                        t.teamName,
                        ROUND(AVG(pgs.gameValue), 1) as gameValue,
                        ROUND(AVG(pgs.REB), 1) as REB,
                        ROUND(AVG(pgs.STL), 1) as STL,
                        ROUND(AVG(pgs.BLK), 1) as BLK


                        FROM players p, league l
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                        JOIN playerOnTeam pot ON dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        AND pot.playerId = p.playerId
                        JOIN
                            teams t ON t.teamId = pot.teamId
                        JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                        JOIN position pos ON pos.positionId = p.positionId
                        GROUP BY p.playerId
                        )
                        SELECT 
                        pd.*,
                        ROUND(1.3 * BLK + STL + 0.04 * gameValue + 0.3 * REB, 1) AS defenseValue
                        FROM playerData pd
                        ORDER BY defenseValue DESC
                        LIMIT 15;";
                        SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                        DataTable dt = new DataTable();
                        rosterData.Fill(dt);
                        if (leagueLeaderDataGridView.ColumnCount > 3) leagueLeaderDataGridView.Columns.RemoveAt(3);
                        leagueLeaderDataGridView.Columns.Add("defenseValue", "Defense Value");
                        leagueLeaderDataGridView.Columns[3].DataPropertyName = "defenseValue";
                        leagueLeaderDataGridView.AutoGenerateColumns = false;
                        leagueLeaderDataGridView.DataSource = dt;
                    }
                }
            }

        }

        
        private void currentStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabels();
        }

        public void leagueLeaderDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // get dataTable from gridView
                DataTable dt = (DataTable)leagueLeaderDataGridView.DataSource;

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
    }
}


