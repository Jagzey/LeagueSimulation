using System.Data;
using System.Data.SQLite;

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
                        SELECT players.playerForename, 
                        players.playerSurname,
                        teams.teamName,
                        playersSeasonStats.PTS
                        FROM playersSeasonStats, players, teams
                        WHERE players.teamName = teams.teamName
                        AND players.playerId = playersSeasonStats.playerId
                        ORDER BY PTS DESC
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
                        SELECT players.playerForename, 
                        players.playerSurname,
                        teams.teamName,
                        playersSeasonStats.REB
                        FROM playersSeasonStats, players, teams
                        WHERE players.teamName = teams.teamName
                        AND players.playerId = playersSeasonStats.playerId
                        ORDER BY REB DESC
                        ;";
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
                        SELECT players.playerForename, 
                        players.playerSurname,
                        teams.teamName,
                        playersSeasonStats.AST
                        FROM playersSeasonStats, players, teams
                        WHERE players.teamName = teams.teamName
                        AND players.playerId = playersSeasonStats.playerId
                        ORDER BY AST DESC
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
                        SELECT players.playerForename, 
                        players.playerSurname,
                        teams.teamName,
                        playersSeasonStats.STL
                        FROM playersSeasonStats, players, teams
                        WHERE players.teamName = teams.teamName
                        AND players.playerId = playersSeasonStats.playerId
                        ORDER BY STL DESC
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
                        SELECT players.playerForename, 
                        players.playerSurname,
                        teams.teamName,
                        playersSeasonStats.TOV
                        FROM playersSeasonStats, players, teams
                        WHERE players.teamName = teams.teamName
                        AND players.playerId = playersSeasonStats.playerId
                        ORDER BY TOV DESC
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
        }

        private void currentStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabels();
        }
    }

}


