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
            rosterFlowLayoutPanel = new FlowLayoutPanel();
            panel1 = new Panel();
            teamRecordLabel = new Label();
            currentTeamRoster = new ComboBox();
            label1 = new Label();
            rosterLabel = new Label();
            rosterDataGridView = new DataGridView();
            PlayerFirstname = new DataGridViewTextBoxColumn();
            PlayerSurname = new DataGridViewTextBoxColumn();
            Overall = new DataGridViewTextBoxColumn();
            Potential = new DataGridViewTextBoxColumn();
            Position = new DataGridViewTextBoxColumn();
            Age = new DataGridViewTextBoxColumn();
            MinutesPlayed = new DataGridViewTextBoxColumn();
            FGPCT = new DataGridViewTextBoxColumn();
            Points = new DataGridViewTextBoxColumn();
            Rebounds = new DataGridViewTextBoxColumn();
            Assists = new DataGridViewTextBoxColumn();
            rosterFlowLayoutPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rosterDataGridView).BeginInit();
            SuspendLayout();
            // 
            // rosterFlowLayoutPanel
            // 
            rosterFlowLayoutPanel.Controls.Add(panel1);
            rosterFlowLayoutPanel.Controls.Add(rosterDataGridView);
            rosterFlowLayoutPanel.Location = new Point(3, 3);
            rosterFlowLayoutPanel.Name = "rosterFlowLayoutPanel";
            rosterFlowLayoutPanel.Size = new Size(628, 394);
            rosterFlowLayoutPanel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(teamRecordLabel);
            panel1.Controls.Add(currentTeamRoster);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(rosterLabel);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(609, 60);
            panel1.TabIndex = 3;
            // 
            // teamRecordLabel
            // 
            teamRecordLabel.AutoSize = true;
            teamRecordLabel.Location = new Point(402, 24);
            teamRecordLabel.Name = "teamRecordLabel";
            teamRecordLabel.Size = new Size(81, 15);
            teamRecordLabel.TabIndex = 10;
            teamRecordLabel.Text = "Team Record: ";
            // 
            // currentTeamRoster
            // 
            currentTeamRoster.DropDownStyle = ComboBoxStyle.DropDownList;
            currentTeamRoster.FormattingEnabled = true;
            currentTeamRoster.Items.AddRange(new object[] { "New York Bankers", "Philadelphia Hawks", "Boston Beavers", "Miami Crocodiles", "Atlanta Raptors", "Washington Wolves", "Charlotte Vipers", "Orlando Knights", "Detroit Thunder", "Cleveland Crows", "Milwaukee Spartans", "Indianapolis Falcons", "Chicago Raiders", "Brooklyn Bulls", "Toronto Titans", "Los Angeles Warriors", "San Francisco Saints", "Phoenix Dragons", "Dallas Cowboys", "Houston Eagles", "Denver Raccoons", "Portland Tornados", "San Antonio Kangaroos", "Las Vegas Dimes", "Seattle Panthers", "Sacramento Sharks", "Salt Lake City Lions", "Oklahoma City Sonics", "New Orleans Raiders", "Minneapolis Seals" });
            currentTeamRoster.Location = new Point(20, 24);
            currentTeamRoster.Name = "currentTeamRoster";
            currentTeamRoster.Size = new Size(142, 23);
            currentTeamRoster.TabIndex = 9;
            currentTeamRoster.SelectedIndexChanged += currentTeamRoster_SelectedIndexChanged;
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
            // rosterDataGridView
            // 
            rosterDataGridView.AllowUserToAddRows = false;
            rosterDataGridView.AllowUserToDeleteRows = false;
            rosterDataGridView.Columns.AddRange(new DataGridViewColumn[] { PlayerFirstname, PlayerSurname, Overall, Potential, Position, Age, MinutesPlayed, FGPCT, Points, Rebounds, Assists });
            rosterDataGridView.Location = new Point(3, 69);
            rosterDataGridView.Name = "rosterDataGridView";
            rosterDataGridView.ReadOnly = true;
            rosterDataGridView.Size = new Size(625, 312);
            rosterDataGridView.TabIndex = 2;
            rosterDataGridView.CellClick += rosterDataGridView_CellClick;
            // 
            // PlayerFirstname
            // 
            PlayerFirstname.DataPropertyName = "playerForename";
            PlayerFirstname.HeaderText = "Firstname";
            PlayerFirstname.Name = "PlayerFirstname";
            PlayerFirstname.ReadOnly = true;
            PlayerFirstname.Width = 90;
            // 
            // PlayerSurname
            // 
            PlayerSurname.DataPropertyName = "playerSurname";
            PlayerSurname.HeaderText = "Surname";
            PlayerSurname.Name = "PlayerSurname";
            PlayerSurname.ReadOnly = true;
            PlayerSurname.Width = 90;
            // 
            // Overall
            // 
            Overall.DataPropertyName = "overall";
            Overall.HeaderText = "Ovr";
            Overall.Name = "Overall";
            Overall.ReadOnly = true;
            Overall.Width = 42;
            // 
            // Potential
            // 
            Potential.DataPropertyName = "potential";
            Potential.HeaderText = "Pot";
            Potential.Name = "Potential";
            Potential.ReadOnly = true;
            Potential.Width = 42;
            // 
            // Position
            // 
            Position.DataPropertyName = "playerPosition";
            Position.HeaderText = "Pos";
            Position.Name = "Position";
            Position.ReadOnly = true;
            Position.Width = 42;
            // 
            // Age
            // 
            Age.DataPropertyName = "age";
            Age.HeaderText = "Age";
            Age.Name = "Age";
            Age.ReadOnly = true;
            Age.Width = 42;
            // 
            // MinutesPlayed
            // 
            MinutesPlayed.DataPropertyName = "MP";
            MinutesPlayed.HeaderText = "MP";
            MinutesPlayed.Name = "MinutesPlayed";
            MinutesPlayed.ReadOnly = true;
            MinutesPlayed.Width = 43;
            // 
            // FGPCT
            // 
            FGPCT.DataPropertyName = "FGPCT";
            FGPCT.HeaderText = "FG%";
            FGPCT.Name = "FGPCT";
            FGPCT.ReadOnly = true;
            FGPCT.Width = 45;
            // 
            // Points
            // 
            Points.DataPropertyName = "PTS";
            Points.HeaderText = "PTS";
            Points.Name = "Points";
            Points.ReadOnly = true;
            Points.Width = 53;
            // 
            // Rebounds
            // 
            Rebounds.DataPropertyName = "REB";
            Rebounds.HeaderText = "REB";
            Rebounds.Name = "Rebounds";
            Rebounds.ReadOnly = true;
            Rebounds.Width = 53;
            // 
            // Assists
            // 
            Assists.DataPropertyName = "AST";
            Assists.HeaderText = "AST";
            Assists.Name = "Assists";
            Assists.ReadOnly = true;
            Assists.Width = 53;
            // 
            // RosterUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rosterFlowLayoutPanel);
            Name = "RosterUserControl";
            Size = new Size(634, 400);
            Load += RosterUserControl_Load;
            rosterFlowLayoutPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rosterDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel rosterFlowLayoutPanel;
        private Panel panel1;
        private Label label1;
        private Label rosterLabel;
        private DataGridView rosterDataGridView;
        public ComboBox currentTeamRoster;
        private DataGridViewTextBoxColumn PlayerFirstname;
        private DataGridViewTextBoxColumn PlayerSurname;
        private DataGridViewTextBoxColumn Overall;
        private DataGridViewTextBoxColumn Potential;
        private DataGridViewTextBoxColumn Position;
        private DataGridViewTextBoxColumn Age;
        private DataGridViewTextBoxColumn MinutesPlayed;
        private DataGridViewTextBoxColumn FGPCT;
        private DataGridViewTextBoxColumn Points;
        private DataGridViewTextBoxColumn Rebounds;
        private DataGridViewTextBoxColumn Assists;
        private Label teamRecordLabel;
    }
}
