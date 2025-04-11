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
    public partial class SeasonSummaryUserControl : UserControl
    {
        public League league;
        public SeasonSummaryUserControl(League league)
        {
            this.league = league;
            InitializeComponent();
            seasonSummaryYear.Maximum = league.CurrentSeason + 2022;
            seasonSummaryYear.Value = league.CurrentSeason + 2022;
            if (league.CurrentSeason == 2) FillLabels();
        }

        public void FillLabels()
        {
            seasonSummaryDataPanel.Hide();
            int currentSeason = (int)seasonSummaryYear.Value - 2023;
            // fill the nonTeam Awards and team records
            string getBestTeams = $@"
                SELECT * FROM (SELECT 
                t.teamId, 
                t.teamName,
                tr.wins,
                tr.losses,
                0.5 as winPct
                FROM teamResults tr
                JOIN teams t ON tr.teamId = t.teamId
                AND t.teamId = (SELECT Champion FROM seasonNonTeamAwards WHERE seasonId = {currentSeason})
                WHERE tr.seasonId = {currentSeason}
                LIMIT 1) as bestTeam
                UNION ALL
                SELECT * FROM(SELECT
                t.teamId, 
                t.teamName,
                tr.wins,
                tr.losses,
                CASE WHEN tr.losses = 0 THEN tr.wins 
                ELSE CAST(tr.wins AS DOUBLE) / (tr.wins + tr.losses) 
                END AS winPct
                FROM teamResults tr
                JOIN teams t ON tr.teamId = t.teamId
                WHERE tr.seasonId = {currentSeason}
                AND t.conferenceId = 1
                ORDER BY winPct DESC
                LIMIT 1) as eastBestTeam
                UNION ALL
                SELECT * FROM (SELECT
                t.teamId, 
                t.teamName,
                tr.wins,
                tr.losses,
                CASE WHEN tr.losses = 0 THEN tr.wins 
                ELSE CAST(tr.wins AS DOUBLE) / (tr.wins + tr.losses) 
                END AS winPct
                FROM teamResults tr
                JOIN teams t ON tr.teamId = t.teamId
                WHERE tr.seasonId = {currentSeason}
                AND t.conferenceId = 2
                ORDER BY winPct DESC
                LIMIT 1) as westBestTeam;
                ";
            using (var connection = new SQLiteConnection(league.ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(getBestTeams, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        int teamCounter = 0;
                        while (reader.Read())
                        {
                            string teamName = reader.GetString(reader.GetOrdinal("teamName"));
                            int wins = reader.GetInt32(reader.GetOrdinal("wins"));
                            int losses = reader.GetInt32(reader.GetOrdinal("losses"));
                            if (teamCounter == 0)
                            {
                                leagueChampsTeamLabel.Text = $"{teamName} ({wins}-{losses})";
                            }
                            else if (teamCounter == 1)
                            {
                                easternConferenceBestRecordTeam.Text = $"{teamName} ({wins}-{losses})";
                            }
                            else if (teamCounter == 2)
                            {
                                westernConferenceBestRecordTeam.Text = $"{teamName} ({wins}-{losses})";
                            }
                            teamCounter++;
                        }
                    }
                }
                
            }
            List<string> awards = new List<string>()
            { "MVP", "DPOY", "EFMVP", "WFMVP", "FMVP", "ROY"};
            foreach (string award in awards)
            {
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    connection.Open();
                    string getAwardQuery = $@"
                    SELECT
                    p.playerId,
                    p.playerForename || ' ' || p.playerSurname AS name,
                    t.city,
                    ROUND(AVG(pgs.PTS), 1) as PTS,
                    ROUND(AVG(pgs.REB), 1) as REB,
                    ROUND(AVG(pgs.AST), 1) as AST

                    FROM players p
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                    AND pgs.isPlayoffs = 0
                    AND pgs.seasonId = {currentSeason}
                    JOIN playerOnTeam pot ON ((pot.dayJoined <= 150 AND pot.yearJoined = {currentSeason} + 2023) OR (pot.yearJoined < {currentSeason} + 2023))
                    AND (pot.yearLeft > {currentSeason} + 2023)
                    AND pot.playerId = p.playerId
                    JOIN teams t ON t.teamId = pot.teamId
                    WHERE p.playerId = (SELECT {award} FROM seasonNonTeamAwards WHERE seasonId = {currentSeason}) -- Enter playerId I want
                    GROUP BY p.playerId
                    ";
                    if (award == "FMVP")
                    {
                        getAwardQuery = $@"
                        SELECT
                        p.playerId,
                        p.playerForename || ' ' || p.playerSurname AS name,
                        t.city,
                        ROUND(AVG(pgs.PTS), 1) as PTS,
                        ROUND(AVG(pgs.REB), 1) as REB,
                        ROUND(AVG(pgs.AST), 1) as AST

                        FROM players p
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                        AND pgs.isPlayoffs = 1
                        JOIN playoffsSchedule ps ON ps.playoffsGameId = pgs.gameId AND ps.playoffsRound = 'Finals'
                        AND pgs.seasonId = {currentSeason}
                        JOIN playerOnTeam pot ON ((pot.dayJoined <= 150 AND pot.yearJoined = {currentSeason} + 2023) OR (pot.yearJoined < {currentSeason} + 2023))
                        AND (pot.yearLeft > {currentSeason} + 2023)
                        AND pot.playerId = p.playerId
                        JOIN teams t ON t.teamId = pot.teamId
                        WHERE p.playerId = (SELECT {award} FROM seasonNonTeamAwards WHERE seasonId = {currentSeason}) -- Enter playerId I want
                        GROUP BY p.playerId";
                    }
                    else if (award.Contains("FMVP"))
                    {
                        getAwardQuery = $@"
                        SELECT
                        p.playerId,
                        p.playerForename || ' ' || p.playerSurname AS name,
                        t.city,
                        ROUND(AVG(pgs.PTS), 1) as PTS,
                        ROUND(AVG(pgs.REB), 1) as REB,
                        ROUND(AVG(pgs.AST), 1) as AST

                        FROM players p
                        JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                        AND pgs.isPlayoffs = 1
                        JOIN playoffsSchedule ps ON ps.playoffsGameId = pgs.gameId AND ps.playoffsRound = 'Conference Finals'
                        AND pgs.seasonId = {currentSeason}
                        JOIN playerOnTeam pot ON ((pot.dayJoined <= 150 AND pot.yearJoined = {currentSeason} + 2023) OR (pot.yearJoined < {currentSeason} + 2023))
                        AND (pot.yearLeft > {currentSeason} + 2023)
                        AND pot.playerId = p.playerId
                        JOIN teams t ON t.teamId = pot.teamId
                        WHERE p.playerId = (SELECT {award} FROM seasonNonTeamAwards WHERE seasonId = {currentSeason}) -- Enter playerId I want
                        GROUP BY p.playerId";
                    }
                    using (var command = new SQLiteCommand(getAwardQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                string city = reader.GetString(reader.GetOrdinal("city"));
                                double pts = reader.GetDouble(reader.GetOrdinal("PTS"));
                                double reb = reader.GetDouble(reader.GetOrdinal("REB"));
                                double ast = reader.GetDouble(reader.GetOrdinal("AST"));
                                if (award == "MVP")
                                {
                                    mvpPlayerInfo.Text = $"{name} ({city})";
                                    mvpPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                }
                                else if (award == "DPOY")
                                {
                                    dpoyPlayerInfo.Text = $"{name} ({city})";
                                    dpoyPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                }
                                else if (award == "ROY")
                                {
                                    royPlayerInfo.Text = $"{name} ({city})";
                                    royPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                }
                                else if (award == "EFMVP")
                                {
                                    efmvpPlayerInfo.Text = $"{name} ({city})";
                                    efmvpPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                }
                                else if (award == "WFMVP")
                                {
                                    wfmvpPlayerInfo.Text = $"{name} ({city})";
                                    wfmvpPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                }
                                else if (award == "FMVP")
                                {
                                    fmvpPlayerInfo.Text = $"{name} ({city})";
                                    fmvpPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                }
                            }
                        }
                    }
                }
            }

            //fill the teamAwards
            List<string> teamAwards = new List<string>()
            { "AllNBAOne", "AllNBATwo", "AllNBAThree", "AllDefenseOne", "AllDefenseTwo", "AllDefenseThree"};
            foreach (string teamAward in teamAwards)
            {
                using (var connection = new SQLiteConnection(league.ConnectionString))
                {
                    connection.Open();
                    string getAwardQuery = $@"
                    SELECT
                    p.playerId,
                    p.playerForename || ' ' || p.playerSurname AS name,
                    t.city,
                    ROUND(AVG(pgs.PTS), 1) as PTS,
                    ROUND(AVG(pgs.REB), 1) as REB,
                    ROUND(AVG(pgs.AST), 1) as AST
                    FROM players p
                    JOIN playerGameStats pgs ON pgs.playerId = p.playerId 
                    AND pgs.isPlayoffs = 0
                    AND pgs.seasonId = {currentSeason}
                    JOIN playerOnTeam pot ON ((pot.dayJoined <= 150 AND pot.yearJoined = {currentSeason} + 2023) OR (pot.yearJoined < {currentSeason} + 2023))
                    AND (pot.yearLeft > {currentSeason} + 2023)
                    AND pot.playerId = p.playerId
                    JOIN teams t ON t.teamId = pot.teamId
                    WHERE p.playerId = (SELECT {teamAward} FROM seasonTeamAwards WHERE seasonId = {currentSeason} AND positionId = 1) -- Enter playerId I want
                    OR p.playerId = (SELECT {teamAward} FROM seasonTeamAwards WHERE seasonId = {currentSeason} AND positionId = 2)
                    OR p.playerId = (SELECT {teamAward} FROM seasonTeamAwards WHERE seasonId = {currentSeason} AND positionId = 3)
                    OR p.playerId = (SELECT {teamAward} FROM seasonTeamAwards WHERE seasonId = {currentSeason} AND positionId = 4)
                    OR p.playerId = (SELECT {teamAward} FROM seasonTeamAwards WHERE seasonId = {currentSeason} AND positionId = 5)
                    GROUP BY p.playerId
                    ORDER BY positionId
                    ";
                    using (var command = new SQLiteCommand(getAwardQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            int posCounter = 0;
                            while (reader.Read())
                            {
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                string city = reader.GetString(reader.GetOrdinal("city"));
                                double pts = reader.GetDouble(reader.GetOrdinal("PTS"));
                                double reb = reader.GetDouble(reader.GetOrdinal("REB"));
                                double ast = reader.GetDouble(reader.GetOrdinal("AST"));
                                if (teamAward == "AllNBAOne")
                                {
                                    if (posCounter == 0)
                                    {
                                        allNBA1PGPlayerInfo.Text = $"{name} ({city})";
                                        allNBA1PGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 1)
                                    {
                                        allNBA1SGPlayerInfo.Text = $"{name} ({city})";
                                        allNBA1SGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 2)
                                    {
                                        allNBA1SFPlayerInfo.Text = $"{name} ({city})";
                                        allNBA1SFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 3)
                                    {
                                        allNBA1PFPlayerInfo.Text = $"{name} ({city})";
                                        allNBA1PFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 4)
                                    {
                                        allNBA1CPlayerInfo.Text = $"{name} ({city})";
                                        allNBA1CPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                }
                                else if (teamAward == "AllNBATwo")
                                {
                                    if (posCounter == 0)
                                    {
                                        allNBA2PGPlayerInfo.Text = $"{name} ({city})";
                                        allNBA2PGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 1)
                                    {
                                        allNBA2SGPlayerInfo.Text = $"{name} ({city})";
                                        allNBA2SGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 2)
                                    {
                                        allNBA2SFPlayerInfo.Text = $"{name} ({city})";
                                        allNBA2SFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 3)
                                    {
                                        allNBA2PFPlayerInfo.Text = $"{name} ({city})";
                                        allNBA2PFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 4)
                                    {
                                        allNBA2CPlayerInfo.Text = $"{name} ({city})";
                                        allNBA2CPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                }
                                else if (teamAward == "AllNBAThree")
                                {
                                    if (posCounter == 0)
                                    {
                                        allNBA3PGPlayerInfo.Text = $"{name} ({city})";
                                        allNBA3PGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 1)
                                    {
                                        allNBA3SGPlayerInfo.Text = $"{name} ({city})";
                                        allNBA3SGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 2)
                                    {
                                        allNBA3SFPlayerInfo.Text = $"{name} ({city})";
                                        allNBA3SFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 3)
                                    {
                                        allNBA3PFPlayerInfo.Text = $"{name} ({city})";
                                        allNBA3PFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 4)
                                    {
                                        allNBA3CPlayerInfo.Text = $"{name} ({city})";
                                        allNBA3CPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                }
                                else if (teamAward == "AllDefenseOne")
                                {
                                    if (posCounter == 0)
                                    {
                                        allDefense1PGPlayerInfo.Text = $"{name} ({city})";
                                        allDefense1PGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 1)
                                    {
                                        allDefense1SGPlayerInfo.Text = $"{name} ({city})";
                                        allDefense1SGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 2)
                                    {
                                        allDefense1SFPlayerInfo.Text = $"{name} ({city})";
                                        allDefense1SFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 3)
                                    {
                                        allDefense1PFPlayerInfo.Text = $"{name} ({city})";
                                        allDefense1PFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 4)
                                    {
                                        allDefense1CPlayerInfo.Text = $"{name} ({city})";
                                        allDefense1CPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                }
                                else if (teamAward == "AllDefenseTwo")
                                {
                                    if (posCounter == 0)
                                    {
                                        allDefense2PGPlayerInfo.Text = $"{name} ({city})";
                                        allDefense2PGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 1)
                                    {
                                        allDefense2SGPlayerInfo.Text = $"{name} ({city})";
                                        allDefense2SGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 2)
                                    {
                                        allDefense2SFPlayerInfo.Text = $"{name} ({city})";
                                        allDefense2SFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 3)
                                    {
                                        allDefense2PFPlayerInfo.Text = $"{name} ({city})";
                                        allDefense2PFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 4)
                                    {
                                        allDefense2CPlayerInfo.Text = $"{name} ({city})";
                                        allDefense2CPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                }
                                else if (teamAward == "AllDefenseThree")
                                {
                                    if (posCounter == 0)
                                    {
                                        allDefense3PGPlayerInfo.Text = $"{name} ({city})";
                                        allDefense3PGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 1)
                                    {
                                        allDefense3SGPlayerInfo.Text = $"{name} ({city})";
                                        allDefense3SGPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 2)
                                    {
                                        allDefense3SFPlayerInfo.Text = $"{name} ({city})";
                                        allDefense3SFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 3)
                                    {
                                        allDefense3PFPlayerInfo.Text = $"{name} ({city})";
                                        allDefense3PFPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                    else if (posCounter == 4)
                                    {
                                        allDefense3CPlayerInfo.Text = $"{name} ({city})";
                                        allDefense3CPlayerStats.Text = $"{pts} pts, {reb} reb, {ast} ast";
                                    }
                                }
                                posCounter++;
                            }
                        }
                    }
                }
            }
            foreach (Label label in seasonSummaryDataPanel.Controls)
            {
                if (label.Text.Contains(Team.GetCityFromTeamName(league.UserTeamName)) && !label.Font.Style.HasFlag(FontStyle.Bold))
                {
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size - 1, FontStyle.Bold);
                }
                else if (!label.Text.Contains(Team.GetCityFromTeamName(league.UserTeamName)) && label.Font.Style.HasFlag(FontStyle.Bold) && !label.Text.Contains(":"))
                {
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size + 1);
                }
            }
            seasonSummaryDataPanel.Show();
        }


        private void seasonSummaryYear_ValueChanged(object sender, EventArgs e)
        {
            FillLabels();
        }
    }
}
