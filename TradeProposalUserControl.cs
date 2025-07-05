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
    public partial class TradeProposalUserControl : UserControl
    {
        public League league;
        public TradeProposalUserControl(League league)
        {
            this.league = league;
            InitializeComponent();
            FillCurrentTeam();
            FillLabels();
        }

        private void FillCurrentTeam()
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
                        WHERE dayJoined <= 
                    
                    {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
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
                        AND pgs.seasonId = {league.CurrentSeason}
                    JOIN 
                        teams t ON cp.teamId = t.teamId
                        AND t.teamName = '{league.UserTeamName}'
                    JOIN 
                        position pos ON p.positionId = pos.positionId -- Position name lookup
    
                    GROUP BY
                        p.playerId
                    ORDER BY p.positionId
                    ";
                    SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                    DataTable dt = new DataTable();
                    rosterData.Fill(dt);
                    currentTeamDataGridView.AutoGenerateColumns = false;
                    currentTeamDataGridView.DataSource = dt;
                }
            }

            // fill team record label
            currentTeamRecord.Text = $"Team Record: {league.GetTeamRecord(league.UserTeamName)}";
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
                        WHERE ((dayJoined <= {league.CurrentDay} AND yearJoined = {league.CurrentSeason + 2023}) OR (yearJoined < {league.CurrentSeason + 2023}))
                        AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
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
                        AND pgs.seasonId = {league.CurrentSeason}
                    JOIN 
                        teams t ON cp.teamId = t.teamId
                        AND t.teamName = '{teamToTradeWithDropDown.Text}'
                    JOIN 
                        position pos ON p.positionId = pos.positionId -- Position name lookup
    
                    GROUP BY
                        p.playerId
                    ORDER BY p.positionId
                    ";
                    SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                    DataTable dt = new DataTable();
                    rosterData.Fill(dt);
                    teamToTradeWithDataGridView.AutoGenerateColumns = false;
                    teamToTradeWithDataGridView.DataSource = dt;
                }
            }

            // fill team record label
            teamRecordLabel.Text = $"Team Record: {league.GetTeamRecord(teamToTradeWithDropDown.Text)}";
        }

        public void currentTeamDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // get dataTable from gridView
                DataTable dt = (DataTable)currentTeamDataGridView.DataSource;

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

        public void teamToTradeWithDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // get dataTable from gridView
                DataTable dt = (DataTable)teamToTradeWithDataGridView.DataSource;

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

        private void proposeTradeButton_Click(object sender, EventArgs e)
        {
            if (league.CurrentDay > 100 & !league.Playoffs)
            {
                MessageBox.Show(text: "The trade deadline has passed. Trades can only be completed next season.");
            }
            else
            {
                bool validTrade = true;
                List<string> playersToTradeAway = new List<string>();
                List<string> playersToTradeFor = new List<string>();
                int userTeamId = league.GetIdFromTeamName(league.UserTeamName);
                int currentTeamId = league.GetIdFromTeamName(teamToTradeWithDropDown.Text);

                if (validTrade && CheckValidPlayerToTrade(playerToTradeAway1.Text)) playersToTradeAway.Add(playerToTradeAway1.Text);
                else if (playerToTradeAway1.Text != "") validTrade = false; // not a valid player, and a player was entered so trade is invalid
                if (validTrade && CheckValidPlayerToTrade(playerToTradeAway2.Text)) playersToTradeAway.Add(playerToTradeAway2.Text);
                else if (playerToTradeAway2.Text != "") validTrade = false; // not a valid player, and a player was entered so trade is invalid
                if (validTrade && CheckValidPlayerToTrade(playerToTradeAway3.Text)) playersToTradeAway.Add(playerToTradeAway3.Text);
                else if (playerToTradeAway3.Text != "") validTrade = false; // not a valid player, and a player was entered so trade is invalid

                if (validTrade && CheckValidPlayerToTrade(playerToTradeFor1.Text)) playersToTradeFor.Add(playerToTradeFor1.Text);
                else if (playerToTradeFor1.Text != "") validTrade = false; // not a valid player, and a player was entered so trade is invalid
                if (validTrade && CheckValidPlayerToTrade(playerToTradeFor2.Text)) playersToTradeFor.Add(playerToTradeFor2.Text);
                else if (playerToTradeFor2.Text != "") validTrade = false; // not a valid player, and a player was entered so trade is invalid
                if (validTrade && CheckValidPlayerToTrade(playerToTradeFor3.Text)) playersToTradeFor.Add(playerToTradeFor3.Text);
                else if (playerToTradeFor3.Text != "") validTrade = false; // not a valid player, and a player was entered so trade is invalid

                // need to add a check for trade value
                foreach (string playerName in playersToTradeFor)
                {
                    // get the player's name
                    string[] splitName = playerName.Split(' ');
                    if (splitName.Length > 2) splitName[1] += ' ' + splitName[2];
                    string firstName = splitName[0];
                    string lastName = splitName[1];
                    int playerId = league.GetPlayerIdFromName(firstName, lastName);
                }

                // trade away players to other team
                if (validTrade)
                {
                    foreach (string playerName in playersToTradeAway)
                    {
                        string[] splitName = playerName.Split(' ');
                        if (splitName.Length > 2) splitName[1] += ' ' + splitName[2];
                        int playerId = league.GetPlayerIdFromName(splitName[0], splitName[1]);
                        // set the teamId to the other teamtrade
                        string setTradeQuery = $@"
                    UPDATE playerOnTeam 
                    SET dayLeft = (SELECT currentDay FROM league),
                    yearLeft = (SELECT currentSeason FROM league) + 2023
                    WHERE playerOnTeamId = (SELECT MAX(playerOnTeamId) FROM playerOnTeam WHERE playerId = {playerId});
                    UPDATE players SET teamId = {currentTeamId} WHERE playerId = {playerId};
                    INSERT INTO playerOnTeam(playerId, teamId, dayJoined, yearJoined, dayLeft, yearLeft)
                     VALUES({playerId}, {currentTeamId}, (SELECT currentDay + 1 FROM league), (SELECT currentSeason + 2023 FROM league), 999, 9999);
                    ";

                        // remove player to minutesSelection table
                        string setMinutesQuery = $@"
                    ";

                        using (var connection = new SQLiteConnection(league.ConnectionString))
                        {
                            connection.Open();
                            using (var command = new SQLiteCommand(connection))
                            {
                                command.CommandText = setTradeQuery;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    // trade for players to your team
                    foreach (string playerName in playersToTradeFor)
                    {
                        string[] splitName = playerName.Split(' ');
                        if (splitName.Length > 2) splitName[1] += ' ' + splitName[2];
                        int playerId = league.GetPlayerIdFromName(splitName[0], splitName[1]);
                        // set the teamId to your team
                        string setTradeQuery = $@"
                    UPDATE playerOnTeam 
                    SET dayLeft = (SELECT currentDay FROM league),
                    yearLeft = (SELECT currentSeason FROM league) + 2023
                    WHERE playerOnTeamId = (SELECT MAX(playerOnTeamId) FROM playerOnTeam WHERE playerId = {playerId});
                    UPDATE players SET teamId = {userTeamId} WHERE playerId = {playerId};
                    INSERT INTO playerOnTeam(playerId, teamId, dayJoined, yearJoined, dayLeft, yearLeft)
                     VALUES({playerId}, {userTeamId}, (SELECT currentDay + 1 FROM league), (SELECT currentSeason + 2023 FROM league), 999, 9999);
                    
                    ";

                        // add player to minutesSelection table
                        string setMinutesQuery = $@"
                        INSERT INTO minutesSelection(playerId, minutesToPlay)
                        VALUES({playerId}, 16);
                    ";

                        using (var connection = new SQLiteConnection(league.ConnectionString))
                        {
                            connection.Open();
                            using (var command = new SQLiteCommand(connection))
                            {
                                command.CommandText = setTradeQuery;
                                command.ExecuteNonQuery();

                                command.CommandText = setMinutesQuery;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show(text: "Trade accepted.");
                }

            }
            
        }

        public bool CheckValidPlayerToTrade(string playerName)
        {
            string[] splitName = playerName.Split(' ');
            if (splitName.Length > 2) splitName[1] += ' ' + splitName[2];
            if (playerName == "" || splitName.Length < 2) return false;
            string firstName = splitName[0];
            string lastName = splitName[1];
            using (SQLiteConnection connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                string checkPlayerValidQuery = $@"
                    SELECT (p.teamId != 0) FROM players p WHERE p.playerForename = @firstName AND p.playerSurname = @lastName;";

                using (SQLiteCommand command = new SQLiteCommand(checkPlayerValidQuery, connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetBoolean(0);
                        }
                    }
                }
            }
            return false;
        }

        private void teamToTradeWithDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabels();
        }
    }
}
