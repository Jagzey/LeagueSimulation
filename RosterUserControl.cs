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
                        SELECT players.playerForename, 
                        players.playerSurname,
                        players.overall,
                        players.potential,
                        players.position, 
                        players.age,
                        playersSeasonStats.MP, 
                        playersSeasonStats.FGPCT, 
                        playersSeasonStats.PTS, 
                        playersSeasonStats.REB, 
                        playersSeasonStats.AST
                        FROM playersSeasonStats, players
                        WHERE players.teamName = '{currentTeamRoster.Text}'
                        AND players.playerId = playersSeasonStats.playerId;
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
