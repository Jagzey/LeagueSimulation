namespace LeagueSimulation
{
    partial class RosterUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            cu = new ListBox();
            label1 = new Label();
            rosterLabel = new Label();
            PlayerName = new DataGridViewTextBoxColumn();
            Overall = new DataGridViewTextBoxColumn();
            Potential = new DataGridViewTextBoxColumn();
            Position = new DataGridViewTextBoxColumn();
            Age = new DataGridViewTextBoxColumn();
            MinutesPlayed = new DataGridViewTextBoxColumn();
            GamesPlayed = new DataGridViewTextBoxColumn();
            Points = new DataGridViewTextBoxColumn();
            Rebounds = new DataGridViewTextBoxColumn();
            Assists = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { PlayerName, Overall, Potential, Position, Age, MinutesPlayed, GamesPlayed, Points, Rebounds, Assists });
            dataGridView1.Location = new Point(3, 69);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(628, 328);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(cu);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(rosterLabel);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(628, 60);
            panel1.TabIndex = 1;
            // 
            // cu
            // 
            cu.FormattingEnabled = true;
            cu.ItemHeight = 15;
            cu.Items.AddRange(new object[] { "New York Bankers", "", "Philadelphia Hawks", "", "Boston Beavers", "", "Miami Crocodiles", "", "Atlanta Raptors", "", "Washington Wolves", "", "Charlotte Vipers", "", "Orlando Knights", "Detroit Thunder", "", "Cleveland Crows", "", "Milwaukee Spartans", "", "Indianapolis Falcons", "", "Chicago Raiders", "", "Brooklyn Bulls", "", "Toronto Titans", "", "Los Angeles Warriors", "", "San Francisco Saints", "", "Phoenix Dragons", "", "Dallas Cowboys", "", "Houston Eagles", "", "Denver Raccoons", "", "Portland Tornados", "", "San Antonio Kangaroos", "", "Las Vegas Dimes", "", "Seattle Panthers", "", "Sacramento Sharks", "", "Salt Lake City Lions", "", "Oklahoma City Sonics", "", "New Orleans Raiders", "", "Minneapolis Seals" });
            cu.Location = new Point(29, 24);
            cu.Name = "cu";
            cu.Size = new Size(120, 19);
            cu.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 6);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 1;
            label1.Text = "Current Team:";
            // 
            // rosterLabel
            // 
            rosterLabel.AutoSize = true;
            rosterLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rosterLabel.Location = new Point(257, 18);
            rosterLabel.Name = "rosterLabel";
            rosterLabel.Size = new Size(126, 25);
            rosterLabel.TabIndex = 0;
            rosterLabel.Text = "Roster Menu";
            // 
            // PlayerName
            // 
            PlayerName.HeaderText = "Player Name";
            PlayerName.Name = "PlayerName";
            PlayerName.ReadOnly = true;
            PlayerName.Width = 140;
            // 
            // Overall
            // 
            Overall.HeaderText = "Ovr";
            Overall.Name = "Overall";
            Overall.ReadOnly = true;
            Overall.Width = 42;
            // 
            // Potential
            // 
            Potential.HeaderText = "Pot";
            Potential.Name = "Potential";
            Potential.ReadOnly = true;
            Potential.Width = 42;
            // 
            // Position
            // 
            Position.HeaderText = "Pos";
            Position.Name = "Position";
            Position.ReadOnly = true;
            Position.Width = 42;
            // 
            // Age
            // 
            Age.HeaderText = "Age";
            Age.Name = "Age";
            Age.ReadOnly = true;
            Age.Width = 42;
            // 
            // MinutesPlayed
            // 
            MinutesPlayed.HeaderText = "MP";
            MinutesPlayed.Name = "MinutesPlayed";
            MinutesPlayed.ReadOnly = true;
            MinutesPlayed.Width = 65;
            // 
            // GamesPlayed
            // 
            GamesPlayed.HeaderText = "GP";
            GamesPlayed.Name = "GamesPlayed";
            GamesPlayed.ReadOnly = true;
            GamesPlayed.Width = 45;
            // 
            // Points
            // 
            Points.HeaderText = "PTS";
            Points.Name = "Points";
            Points.ReadOnly = true;
            Points.Width = 53;
            // 
            // Rebounds
            // 
            Rebounds.HeaderText = "REB";
            Rebounds.Name = "Rebounds";
            Rebounds.ReadOnly = true;
            Rebounds.Width = 53;
            // 
            // Assists
            // 
            Assists.HeaderText = "AST";
            Assists.Name = "Assists";
            Assists.ReadOnly = true;
            Assists.Width = 53;
            // 
            // RosterUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "RosterUserControl";
            Size = new Size(634, 400);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Label rosterLabel;
        private Label label1;
        private ListBox cu;
        private DataGridViewTextBoxColumn PlayerName;
        private DataGridViewTextBoxColumn Overall;
        private DataGridViewTextBoxColumn Potential;
        private DataGridViewTextBoxColumn Position;
        private DataGridViewTextBoxColumn Age;
        private DataGridViewTextBoxColumn MinutesPlayed;
        private DataGridViewTextBoxColumn GamesPlayed;
        private DataGridViewTextBoxColumn Points;
        private DataGridViewTextBoxColumn Rebounds;
        private DataGridViewTextBoxColumn Assists;
    }
}
