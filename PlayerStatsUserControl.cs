using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Xml.Linq;

namespace LeagueSimulation
{
    public partial class PlayerStatsUserControl : UserControl
    {
        public League? league;
        public int playerId;
        public Player? playerShown;
        public PlayerStatsUserControl(League league, int playerId)
        {
            this.league = league;
            InitializeComponent();
            playerIdUpDown.Value = playerId;
            //FillLabels();
        }

        public void FillBiometrics()
        {
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                string getPlayerQuery = $@"
                    WITH currentPlayers AS (
                    SELECT playerId, teamId
                    FROM playerOnTeam
                    WHERE dayJoined <= {league.CurrentDay} AND yearJoined <= {league.CurrentSeason + 2023}
                    AND dayLeft >= {league.CurrentDay} AND yearLeft >= {league.CurrentSeason + 2023}
                )
                    SELECT 
	                p.playerId,
	                p.playerForename || ' ' || p.playerSurname as name,
	                ((l.currentSeason + 2023) - p.dateOfBirth) AS age,
	                t.teamName,
	                pos.positionShort as playerPosition,
	                printf('%d''%d', p.height / 12, p.height % 12) as height,
	                p.weight,
	                sp.playstyle,
                    p.dateOfBirth
	
	
                    FROM currentPlayers cp, league l
                    JOIN players p ON cp.playerId = p.playerId
                    JOIN teams t ON cp.teamId = t.teamId
                    JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                    JOIN position pos ON pos.positionId = p.positionId 
                    WHERE p.playerId = {playerId} -- Enter playerId I want
                ";
                using (var command = new SQLiteCommand(getPlayerQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playerNameLabel.Text = $"Name: {reader.GetString(1)}";
                            ageLabel.Text = $"Age: {reader.GetInt32(2)}";
                            teamNameLabel.Text = $"Team Name: {reader.GetString(3)}";
                            positionLabel.Text = $"Position: {reader.GetString(4)}";
                            heightLabel.Text = $"Height: {reader.GetString(5)}";
                            weightLabel.Text = $"Weight: {reader.GetInt32(6)} lbs";
                            playstyleLabel.Text = $"Playstyle: {reader.GetString(7)}";
                            dateOfBirthLabel.Text = $"Date Of Birth: {reader.GetInt32(8)}";
                        }
                    }
                }
            }
        }

        public void FillSummaryStats()
        {
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                string getPlayerQuery = $@"
                    SELECT 
                    p.playerId,
                    COUNT(*) as gamesPlayed,
                    ROUND(AVG(pgs.PTS), 1) as PTS,
                    ROUND(AVG(pgs.REB), 1) as REB,
                    ROUND(AVG(pgs.AST), 1) as AST,
                    ROUND(SUM(pgs.FGM) * 100.0 / SUM(pgs.FGA), 1) as FGPCT,
                    COALESCE(ROUND(SUM(pgs.TFGM) * 100.0 / SUM(pgs.TFGA), 1), 0.0) as TFGPCT
	
	
                    FROM players p
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId AND pgs.isPlayoffs = 0
                    WHERE p.playerId = {playerId} -- Enter playerId I want
                ";
                SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerQuery, connection);
                DataTable dt = new DataTable();
                rosterData.Fill(dt);
                summaryGridView.AutoGenerateColumns = false;
                summaryGridView.DataSource = dt;
            }
        }

        public void FillSeasonStats()
        {
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                string getSeasonDataQuery = $@"
                    SELECT 
                    p.playerId,
                    l.currentSeason + 2023 AS currentSeason,
                    p.playerForename || ' ' || p.playerSurname as name,
                    l.currentSeason + 2023 - p.dateOfBirth AS age,
                    t.teamName,
                    pos.positionShort as playerPosition,
                    ROUND(AVG(pgs.MP), 1) as MP,
                    ROUND(AVG(pgs.gameValue), 1) as gameValue,
                    ROUND(AVG(pgs.FGM), 1) as FGM,
                    ROUND(AVG(pgs.FGA), 1) as FGA,
                    COALESCE(ROUND(SUM(pgs.FGM) * 100.0 / SUM(pgs.FGA), 1), 0.0) as 'FG%',
                    ROUND(AVG(pgs.TFGM), 1) as '3PM',
                    ROUND(AVG(pgs.TFGA), 1) as '3PA',
                    COALESCE(ROUND(SUM(pgs.TFGM) * 100.0 / SUM(pgs.TFGA), 1), 0.0) as '3P%',
                    ROUND(AVG(pgs.PTS), 1) as PTS,
                    ROUND(AVG(pgs.REB), 1) as REB,
                    ROUND(AVG(pgs.AST), 1) as AST,
                    ROUND(AVG(pgs.STL), 1) as STL,
                    ROUND(AVG(pgs.BLK), 1) as BLK,
                    ROUND(AVG(pgs.TOV), 1) as TOV


                    FROM league l, playerGameStats pgs
                    JOIN seasonSchedule ss ON ss.gameId = pgs.gameId
                    JOIN playerOnTeam pot ON dayJoined <= ss.dayId AND yearJoined <= ss.seasonId + 2023
                    AND dayLeft >= ss.dayId AND yearLeft >= ss.seasonId + 2023
                    AND pgs.isPlayoffs = 0
                    JOIN players p ON pot.playerId = p.playerId
                    AND p.playerId = {playerId} -- Enter playerId I want
                    JOIN teams t ON pot.teamId = t.teamId
                    JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                    JOIN position pos ON pos.positionId = p.positionId
                    WHERE pgs.IsPlayoffs = 0
                    AND pgs.playerId = pot.playerId 
                    GROUP BY p.playerId, ss.seasonId
                    ORDER BY currentSeason
                ;";
                SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getSeasonDataQuery, connection);
                DataTable dt = new DataTable();
                rosterData.Fill(dt);
                seasonDataGridView.AutoGenerateColumns = false;
                seasonDataGridView.DataSource = dt;
            }
        }
        public void FillAttributes()
        {
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                string getPlayerQuery = $@"
                    SELECT 
                    p.playerId,
                    ((l.currentSeason + 2023) - p.dateOfBirth) AS age,
                    p.overall,
                    p.potential,
                    p.layup,
                    p.dunk,
                    p.midRange,
                    p.threePoint,
                    p.freeThrow,
                    p.passing,
                    p.ballHandle,
                    p.defense,
                    p.steal,
                    p.block,
                    p.rebound,
                    p.speed,
                    p.stamina,
                    p.strength
                    FROM players p, league l
                    WHERE p.playerId = {playerId} -- Enter playerId I want
                ";
                SQLiteDataAdapter rosterData = new SQLiteDataAdapter(getPlayerQuery, connection);
                DataTable dt = new DataTable();
                rosterData.Fill(dt);
                attributesGridView.AutoGenerateColumns = false;
                attributesGridView.DataSource = dt;
            }
        }
        public void FillLabels()
        {
            FillBiometrics();
            FillSummaryStats();
            FillAttributes();
            FillSeasonStats();
        }

        private void playerIdUpDown_ValueChanged(object sender, EventArgs e)
        {
            playerId = (int)playerIdUpDown.Value;
            FillLabels();
        }
    }
}
