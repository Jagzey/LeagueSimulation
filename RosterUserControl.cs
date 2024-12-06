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
                            players p, league l
                        LEFT JOIN 
                            playerGameStats pgs ON p.playerId = pgs.playerId
                        JOIN 
                            teams t ON p.teamId = t.teamId
                        JOIN 
                            position pos ON p.positionId = pos.positionId -- Position name lookup
                        WHERE 
                            t.teamName = '{currentTeamRoster.Text}'
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
        }

        private void RosterUserControl_Load(object sender, EventArgs e)
        {

        }

        private void currentTeamRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLabels();
        }
    }
}
