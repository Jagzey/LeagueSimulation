using LeagueSimulation.Models;
using System.Data.SQLite;

namespace LeagueSimulation
{
    public partial class Main : Form
    {
        private League currentLeague;
        private string currentUser = "";
        public string CurrentUser { get; set; }
        public League CurrentLeague { get; set; }
        public Main()
        {
            if (Directory.Exists("C:/Users/FiercePC")) this.CurrentUser = "FiercePC";
            else if (Directory.Exists("C:/Users/nzuobm")) this.CurrentUser = "nzuobm";
            InitializeComponent();
        }

        private bool CheckIfLeagueExists(int saveState, string currentUser)
        {
            // this is the string of the file name
            string variableFileName = $"C:\\Users\\{currentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Databases\\League{saveState}.db";
            // if the file doesn't exist, we initialise the database
            if (!File.Exists(variableFileName)) return false;
            return true;
        }

        private string GetLeagueFileName(int saveState)
        {
            // this is the string of the file name
            return $"C:\\Users\\{CurrentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueSimulation\\Databases\\League{saveState}.db";
        }

        // used to select the user's team name
        private string GetUserTeamName(int saveState, string currentUser)
        {
            string connectionString = $"Data Source={GetLeagueFileName(saveState)};Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string getTeamNameFromSaveState = "SELECT userTeamName FROM league";
                using (var command = new SQLiteCommand(getTeamNameFromSaveState, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            return "";
        }

        private void loadGameButton_Click(object sender, EventArgs e)
        {
            // first, we check if the save state is valid
            int saveState = (int)saveStateNum.Value;
            if (CheckIfLeagueExists(saveState, CurrentUser))
            {
                MessageBox.Show(text: "A league exists for this save state, it will now be loaded.");
                string userTeamName = GetUserTeamName(saveState, CurrentUser);
                string leagueFileName = GetLeagueFileName(saveState);
                Form form2 = new Form2(saveState, new League(CurrentUser, saveState, false, userTeamName, leagueFileName));
                this.Hide();
                form2.Show();
            }
            else
            {
                MessageBox.Show(text: "No league exists for this save state, press 'Create Game' to create it, or enter a different save state.");
            }
        }

        private void createGameButton_Click(object sender, EventArgs e)
        {
            string teamNamesConnectionString = $@"Data Source=C:\\Users\\{CurrentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueGenerator\\Player Data\\basketball_team_names_list.txt;Version=3;";
            // first, we check if the save state is valid
            int saveState = (int)saveStateNum.Value;
            // this occurs if the user's input is an integer
            {
                if (CheckIfLeagueExists(saveState, CurrentUser))
                {
                    MessageBox.Show(text: "A league exists for this save state, press 'Load Game' to load it, or enter a different save state.");
                }
                else if (teamNameDropDown.Text == "")
                {
                    MessageBox.Show(text: "Please choose a team name before creating your league.");
                }
                else
                {
                    // we load the league into form2
                    MessageBox.Show(text: "No league exists for this save state, it will now be created.");
                    string userTeamName = teamNameDropDown.Text;
                    string leagueFileName = GetLeagueFileName(saveState);
                    Form form2 = new Form2(saveState, new League(CurrentUser, saveState, false, userTeamName, leagueFileName));
                    this.Hide();
                    form2.Show();
                }
            }
        }
    }
}
