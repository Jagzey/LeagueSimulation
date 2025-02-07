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
using LeagueSimulation.Models;

namespace LeagueSimulation
{
    public partial class ViewGameResultsUserControl : UserControl
    {
        public League? league;
        public int gameId;
        public ViewGameResultsUserControl(League league, int gameId)
        {
            this.league = league;
            this.gameId = gameId;
            InitializeComponent();
            FillLabels();
        }

        public void FillLabels()
        {
            string team1Name = "";
            string team1Record = "";
            string team2Name = "";
            string team2Record = "";
            string gameScore = league.GetGameScore(gameId);

            // here, we fill the names for the teams and their records
            {
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    connection.Open();
                    string getTeamGameDataQuery = $@"
                    SELECT *
                    FROM seasonSchedule ss
                    WHERE ss.gameId = {gameId}
                    AND ss.seasonId = {league.CurrentSeason}
                    ;";
                    if (league.Playoffs)
                    {
                        getTeamGameDataQuery = $@"
                        SELECT *
                        FROM playoffsSchedule ss
                        WHERE ss.playoffsGameId = {gameId}
                        AND ss.seasonId = {league.CurrentSeason}
                    ;";
                    }

                    using (var command = new SQLiteCommand(getTeamGameDataQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                team1Name = league.GetTeamNameFromId(reader.GetInt32(reader.GetOrdinal("homeTeamId")).ToString());
                                team2Name = league.GetTeamNameFromId(reader.GetInt32(reader.GetOrdinal("awayTeamId")).ToString());
                            }
                        }
                    }
                }
                team1Record = league.GetTeamRecord(team1Name);
                team2Record = league.GetTeamRecord(team2Name);
                if (league.Playoffs)
                {
                    team1Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(team1Name).ToString(), league.GetIdFromTeamName(team2Name).ToString());
                    team2Record = league.GetSeriesRecordToDisplay(league.GetIdFromTeamName(team2Name).ToString(), league.GetIdFromTeamName(team1Name).ToString());
                }
                gameResultLabel.Text = $"{team1Name} ({team1Record}) {gameScore} {team2Name} ({team2Record})";
            }

            // fill team 1 data
            {
                team1NameDataLabel.Text = team1Name;
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    connection.Open();
                    string getPlayerDataQuery = $@"
                        WITH currentPlayers AS (
                            SELECT playerId, teamId
                            FROM playerOnTeam
                            WHERE dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                            AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        )
                        SELECT
                        p.playerForename || ' ' || p.playerSurname as name,
                        ((l.currentSeason + 2023) - p.dateOfBirth) AS age,
                        pos.positionShort as playerPosition,
                        sp.playstyle,
                        pgs.MP,
                        pgs.gameValue,
                        pgs.FGM,
                        pgs.FGA,
                        pgs.TFGM as '3PM',
                        pgs.TFGA as '3PA',
                        pgs.FTM,
                        pgs.FTA,
                        pgs.PTS,
                        pgs.REB,
                        pgs.AST,
                        pgs.STL,
                        pgs.BLK,
                        pgs.TOV,
                        pgs.PF

                        FROM currentPlayers cp, league l
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId
                        AND pgs.isPlayoffs = {league.Playoffs}
                        AND pgs.gameId = {gameId} -- gameId for the results
                        AND pgs.seasonId = {league.CurrentSeason}
                        JOIN players p ON cp.playerId = p.playerId
                        JOIN teams t ON cp.teamId = t.teamId
                        AND t.teamName = '{team1Name}' -- teamName to input
                        JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                        JOIN position pos ON pos.positionId = p.positionId
                        ORDER BY MP DESC
                        ;";
                    SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                    DataTable dt = new DataTable();
                    rosterData.Fill(dt);
                    team1DataGridView.AutoGenerateColumns = false;
                    team1DataGridView.DataSource = dt;
                }
            }

            // fill team 2 data
            {
                team2NameDataLabel.Text = team2Name;
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    connection.Open();
                    string getPlayerDataQuery = $@"
                        WITH currentPlayers AS (
                            SELECT playerId, teamId
                            FROM playerOnTeam
                            WHERE dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                            AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                        )
                        SELECT
                        p.playerForename || ' ' || p.playerSurname as name,
                        ((l.currentSeason + 2023) - p.dateOfBirth) AS age,
                        pos.positionShort as playerPosition,
                        sp.playstyle,
                        pgs.MP,
                        pgs.gameValue,
                        pgs.FGM,
                        pgs.FGA,
                        pgs.TFGM as '3PM',
                        pgs.TFGA as '3PA',
                        pgs.FTM,
                        pgs.FTA,
                        pgs.PTS,
                        pgs.REB,
                        pgs.AST,
                        pgs.STL,
                        pgs.BLK,
                        pgs.TOV,
                        pgs.PF

                        FROM currentPlayers cp, league l
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId
                        AND pgs.isPlayoffs = {league.Playoffs}
                        AND pgs.gameId = {gameId} -- gameId for the results
                        AND pgs.seasonId = {league.CurrentSeason}
                        JOIN players p ON cp.playerId = p.playerId
                        JOIN teams t ON cp.teamId = t.teamId
                        AND t.teamName = '{team2Name}' -- teamName to input
                        JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                        JOIN position pos ON pos.positionId = p.positionId
                        ORDER BY MP DESC
                        ;";
                    SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerDataQuery, connection);
                    DataTable dt = new DataTable();
                    rosterData.Fill(dt);
                    team2DataGridView.AutoGenerateColumns = false;
                    team2DataGridView.DataSource = dt;
                }
            }
        }

        private void backToScheduleButton_Click(object sender, EventArgs e)
        {

        }

        private void team1DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // access data row we need
                DataGridViewRow dataRow = team1DataGridView.Rows[e.RowIndex];
                // Access the correct data object from the row
                var boundData = dataRow.DataBoundItem;
                string name = "";
                if (boundData != null)
                {
                    DataRowView rowView = boundData as DataRowView;
                    name = (string)rowView["name"];
                }
                string[] splitName = name.Split(" ");
                int playerId = league.GetPlayerIdFromName(splitName[0], splitName[1]);
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

        private void team2DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // makes sure we don't check in the title row
            if (e.RowIndex >= 0)
            {
                // access data row we need
                DataGridViewRow dataRow = team2DataGridView.Rows[e.RowIndex];
                // Access the correct data object from the row
                var boundData = dataRow.DataBoundItem;
                string name = "";
                if (boundData != null)
                {
                    DataRowView rowView = boundData as DataRowView;
                    name = (string)rowView["name"];
                }
                string[] splitName = name.Split(" ");
                int playerId = league.GetPlayerIdFromName(splitName[0], splitName[1]);
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
    }
}
