using LeagueSimulation.Models;

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
            string currentUser = "nzuobm";
            this.CurrentUser = currentUser;
            InitializeComponent();
        }

        public void ShowUserControl(UserControl userControl)
        {
            // clear existing controls
            this.Controls.Clear();

            this.Controls.Add(userControl);
        }

        private void loadGameButton_Click(object sender, EventArgs e)
        {
            // first, we check if the save state is valid
            int saveState = (int)saveStateNum.Value;
            if (League.CheckIfLeagueExists(saveState, CurrentUser))
            {
                MessageBox.Show(text: "A league exists for this save state, it will now be loaded.");
                Form form2 = new Form2(saveState, new League(CurrentUser, saveState, false, League.CheckTeamMatchesSaveState(saveState, CurrentUser)));
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
            string teamNamesConnectionString = $@"Data Source=C:\\Users\\{CurrentUser}\\OneDrive - The Kings School Chester\\A-Level\\Computer Science\\NEA Project\\Project Files\\LeagueGenerator\\Names Files\\basketball_team_names_list.txt;Version=3;";
            // first, we check if the save state is valid
            int saveState = (int)saveStateNum.Value;
            // this occurs if the user's input is an integer
            {
                if (League.CheckIfLeagueExists(saveState, CurrentUser))
                {
                    MessageBox.Show(text: "A league exists for this save state, press 'Load Game' to load it, or enter a different save state.");
                }
                else
                {
                    // we load the league into form2
                    MessageBox.Show(text: "No league exists for this save state, it will now be created.");
                    Form form2 = new Form2(saveState, new League(CurrentUser, saveState, true, teamNameDropDown.Text));
                    this.Hide();
                    form2.Show();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
