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
            FillLabels();
        }

        public void FillBiometrics()
        {
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                string getPlayerQuery = $@"
                    SELECT 
	                p.playerId,
	                p.playerForename || ' ' || p.playerSurname as name,
	                ((l.currentSeason + 2023) - p.dateOfBirth) AS age,
	                t.teamName,
	                pos.positionShort as playerPosition,
	                printf('%d''%d', p.height / 12, p.height % 12) as height,
	                p.weight,
	                sp.playstyle
	
	
                    FROM players p, league l
                    JOIN teams t ON p.teamId = t.teamId
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

        public void FillAttributes()
        {

        }
        public void FillLabels()
        {
            // here we fill the biometrics
            FillBiometrics();
            FillSummaryStats();
        }

        private void playerIdUpDown_ValueChanged(object sender, EventArgs e)
        {
            playerId = (int)playerIdUpDown.Value;
            FillLabels();
        }
    }
}
