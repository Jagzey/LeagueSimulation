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
            playerIdUpDown.Maximum = GetMaxPlayers();
            playerIdUpDown.Value = playerId;
            this.playerId = playerId;
            if (playerId == 1) FillLabels();
        }

        public int GetMaxPlayers()
        {
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                string getPlayerQuery = $@"
                    SELECT MAX(playerId) FROM players
                ";
                using (var command = new SQLiteCommand(getPlayerQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return -1;
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
                    pgs.seasonId + 2023 AS currentSeason,
                    p.playerForename || ' ' || p.playerSurname as name,
                    pgs.seasonId + 2023 - p.dateOfBirth AS age,
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
                    JOIN seasonSchedule ss ON pgs.seasonId = ss.seasonId
                    AND pgs.gameId = ss.gameId
                    JOIN playerOnTeam pot ON dayJoined <= ss.dayId AND yearJoined <= pgs.seasonId + 2023
                    AND dayLeft >= ss.dayId AND yearLeft >= pgs.seasonId + 2023
                    JOIN players p ON pot.playerId = p.playerId
                    AND p.playerId = {playerId} -- Enter playerId I want
                    JOIN teams t ON pot.teamId = t.teamId
                    JOIN secondaryPlaystyle sp ON sp.secondaryPlaystyleId = p.secondaryPlaystyleId
                    JOIN position pos ON pos.positionId = p.positionId
                    WHERE pgs.IsPlayoffs = 0
                    AND pgs.playerId = pot.playerId 
                    GROUP BY p.playerId, pgs.seasonId
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
            FillAwards();
        }

        public void FillAwards()
        {
            if (league.CurrentSeason != 1)
            {
                // get any awards
                int numMVPs = 0;
                int numDPOYs = 0;
                int numFMVPs = 0;
                int numCFMVPs = 0;
                int numROYs = 0;
                int numChampions = 0;
                int numAllLeagues = 0;
                int numAllDefenses = 0;
                int numPTSLeaders = 0;
                int numREBLeaders = 0;
                int numASTLeaders = 0;
                int numSTLLeaders = 0;
                int numBLKLeaders = 0;
                int numTOVLeaders = 0;
                int numFGMLeaders = 0;
                int numFGALeaders = 0;
                int numFGPCTLeaders = 0;
                int numTFGMLeaders = 0;
                int numTFGALeaders = 0;
                int numTFGPCTLeaders = 0;


                string getAwardsQuery = $@"
                    SELECT 
                    (SELECT COUNT(MVP) FROM seasonNonTeamAwards WHERE MVP = {playerId}) as numMVPs,
                    (SELECT COUNT(DPOY) FROM seasonNonTeamAwards WHERE DPOY = {playerId}) as numDPOYs,
                    (SELECT COUNT(FMVP) FROM seasonNonTeamAwards WHERE FMVP = {playerId}) as numFMVPs,
                    (SELECT COUNT(ROY) FROM seasonNonTeamAwards WHERE ROY = {playerId}) as numROYs,
                    (SELECT COUNT(EFMVP) FROM seasonNonTeamAwards WHERE EFMVP = {playerId}) as numEFMVPs,
                    (SELECT COUNT(WFMVP) FROM seasonNonTeamAwards WHERE WFMVP = {playerId}) as numWFMVPs, 
                    (SELECT COUNT(PTSLeader) FROM seasonNonTeamAwards WHERE PTSLeader = {playerId}) as numPTSLeaders,
                    (SELECT COUNT(REBLeader) FROM seasonNonTeamAwards WHERE REBLeader = {playerId}) as numREBLeaders,
                    (SELECT COUNT(ASTLeader) FROM seasonNonTeamAwards WHERE ASTLeader = {playerId}) as numASTLeaders,
                    (SELECT COUNT(STLLeader) FROM seasonNonTeamAwards WHERE STLLeader = {playerId}) as numSTLLeaders,
                    (SELECT COUNT(BLKLeader) FROM seasonNonTeamAwards WHERE BLKLeader = {playerId}) as numBLKLeaders,
                    (SELECT COUNT(TOVLeader) FROM seasonNonTeamAwards WHERE TOVLeader = {playerId}) as numTOVLeaders,
                    (SELECT COUNT(FGMLeader) FROM seasonNonTeamAwards WHERE FGMLeader = {playerId}) as numFGMLeaders,
                    (SELECT COUNT(FGALeader) FROM seasonNonTeamAwards WHERE FGALeader = {playerId}) as numFGALeaders,
                    (SELECT COUNT(FGPCTLeader) FROM seasonNonTeamAwards WHERE FGPCTLeader = {playerId}) as numFGPCTLeaders,
                    (SELECT COUNT(TFGMLeader) FROM seasonNonTeamAwards WHERE TFGMLeader = {playerId}) as numTFGMLeaders,
                    (SELECT COUNT(TFGALeader) FROM seasonNonTeamAwards WHERE TFGALeader = {playerId}) as numTFGALeaders,
                    (SELECT COUNT(TFGPCTLeader) FROM seasonNonTeamAwards WHERE TFGPCTLeader = {playerId}) as numTFGPCTLeaders,
                    (SELECT COUNT(AllNBAOne) FROM seasonTeamAwards WHERE AllNBAOne = {playerId}) as numAllNBAOnes,
                    (SELECT COUNT(AllNBATwo) FROM seasonTeamAwards WHERE AllNBATwo = {playerId}) as numAllNBATwos,
                    (SELECT COUNT(AllNBAThree) FROM seasonTeamAwards WHERE AllNBAThree = {playerId}) as numAllNBAThrees,
                    (SELECT COUNT(AllDefenseOne) FROM seasonTeamAwards WHERE AllDefenseOne = {playerId}) as numAllDefenseOnes,
                    (SELECT COUNT(AllDefenseTwo) FROM seasonTeamAwards WHERE AllDefenseTwo = {playerId}) as numAllDefenseTwos,
                    (SELECT COUNT(AllDefenseThree) FROM seasonTeamAwards WHERE AllDefenseThree = {playerId}) as numAllDefenseThrees,
                    COUNT(*) as Champions
                    FROM seasonNonTeamAwards snta
                    JOIN playerOnTeam pot ON dayJoined <= 125 AND yearJoined <= snta.seasonId + 2023
                    AND dayLeft >= 125 AND yearLeft >= snta.seasonId + 2023
                    AND pot.playerId = {playerId}
                    AND snta.Champion = pot.teamId
                    ";
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(getAwardsQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                numMVPs = reader.GetInt32(reader.GetOrdinal("numMVPs"));
                                numDPOYs = reader.GetInt32(reader.GetOrdinal("numDPOYs"));
                                numFMVPs = reader.GetInt32(reader.GetOrdinal("numFMVPs"));
                                numCFMVPs += reader.GetInt32(reader.GetOrdinal("numEFMVPs"));
                                numCFMVPs += reader.GetInt32(reader.GetOrdinal("numWFMVPs"));
                                numROYs = reader.GetInt32(reader.GetOrdinal("numROYs"));
                                numChampions = reader.GetInt32(reader.GetOrdinal("Champions"));
                                numPTSLeaders = reader.GetInt32(reader.GetOrdinal("numPTSLeaders"));
                                numREBLeaders = reader.GetInt32(reader.GetOrdinal("numREBLeaders"));
                                numASTLeaders = reader.GetInt32(reader.GetOrdinal("numASTLeaders"));
                                numSTLLeaders = reader.GetInt32(reader.GetOrdinal("numSTLLeaders"));
                                numBLKLeaders = reader.GetInt32(reader.GetOrdinal("numBLKLeaders"));
                                numTOVLeaders = reader.GetInt32(reader.GetOrdinal("numTOVLeaders"));
                                numFGMLeaders = reader.GetInt32(reader.GetOrdinal("numFGMLeaders"));
                                numFGALeaders = reader.GetInt32(reader.GetOrdinal("numFGALeaders"));
                                numFGPCTLeaders = reader.GetInt32(reader.GetOrdinal("numFGPCTLeaders"));
                                numTFGMLeaders = reader.GetInt32(reader.GetOrdinal("numTFGALeaders"));
                                numTFGALeaders = reader.GetInt32(reader.GetOrdinal("numTFGMLeaders"));
                                numTFGPCTLeaders = reader.GetInt32(reader.GetOrdinal("numTFGPCTLeaders"));
                                numAllLeagues += reader.GetInt32(reader.GetOrdinal("numAllNBAOnes"));
                                numAllLeagues += reader.GetInt32(reader.GetOrdinal("numAllNBATwos"));
                                numAllLeagues += reader.GetInt32(reader.GetOrdinal("numAllNBAThrees"));
                                numAllDefenses += reader.GetInt32(reader.GetOrdinal("numAllDefenseOnes"));
                                numAllDefenses += reader.GetInt32(reader.GetOrdinal("numAllDefenseTwos"));
                                numAllDefenses += reader.GetInt32(reader.GetOrdinal("numAllDefenseThrees"));
                            }
                        }
                    }
                    awardsLabel.Text = "Awards: ";
                    if (numMVPs > 0) awardsLabel.Text += $"\n {numMVPs}x MVP";
                    if (numDPOYs > 0) awardsLabel.Text += $"\n {numDPOYs}x DPOY";
                    if (numChampions > 0) awardsLabel.Text += $"\n {numChampions}x Champion";
                    if (numFMVPs > 0) awardsLabel.Text += $"\n {numFMVPs}x FMVP";
                    if (numCFMVPs > 0) awardsLabel.Text += $"\n {numCFMVPs}x CFMVP";
                    if (numROYs > 0) awardsLabel.Text += $"\n {numROYs}x ROY";
                    if (numPTSLeaders > 0) awardsLabel.Text += $"\n {numPTSLeaders}x PTS Leader";
                    if (numASTLeaders > 0) awardsLabel.Text += $"\n {numASTLeaders}x AST Leader";
                    if (numSTLLeaders > 0) awardsLabel.Text += $"\n {numSTLLeaders}x STL Leader";
                    if (numBLKLeaders > 0) awardsLabel.Text += $"\n {numBLKLeaders}x BLK Leader";
                    if (numTOVLeaders > 0) awardsLabel.Text += $"\n {numTOVLeaders}x TOV Leader";
                    if (numFGMLeaders > 0) awardsLabel.Text += $"\n {numFGMLeaders}x FGM Leader";
                    if (numFGALeaders > 0) awardsLabel.Text += $"\n {numFGALeaders}x FGA Leader";
                    if (numFGPCTLeaders > 0) awardsLabel.Text += $"\n {numFGPCTLeaders}x FG% Leader";
                    if (numTFGMLeaders > 0) awardsLabel.Text += $"\n {numTFGMLeaders}x 3PM Leader";
                    if (numTFGALeaders > 0) awardsLabel.Text += $"\n {numTFGALeaders}x 3PA Leader";
                    if (numTFGPCTLeaders > 0) awardsLabel.Text += $"\n {numTFGPCTLeaders}x 3P% Leader";
                    if (numAllLeagues > 0) awardsLabel.Text += $"\n {numAllLeagues}x All-League";
                    if (numAllDefenses > 0) awardsLabel.Text += $"\n {numAllDefenses}x All-Defense";
                }
            }
            
        }

        private void playerIdUpDown_ValueChanged(object sender, EventArgs e)
        {
            playerId = (int)playerIdUpDown.Value;
            FillLabels();
        }
    }
}
