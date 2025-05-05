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
            playerDropDown = new ComboBox();
            playerManagementLabel = new Label();
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
            rosterManagementPanel = new Panel();
            minutesSelectorButton = new Button();
            positionChangeButton = new Button();
            label4 = new Label();
            newPositionDropDown = new ComboBox();
            label5 = new Label();
            currentPositionLabel = new Label();
            minutesSelectorUpDown = new NumericUpDown();
            label3 = new Label();
            label2 = new Label();
            rosterFlowLayoutPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rosterDataGridView).BeginInit();
            rosterManagementPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)minutesSelectorUpDown).BeginInit();
            SuspendLayout();
            // 
            // rosterFlowLayoutPanel
            // 
            rosterFlowLayoutPanel.Controls.Add(panel1);
            rosterFlowLayoutPanel.Controls.Add(rosterDataGridView);
            rosterFlowLayoutPanel.Controls.Add(rosterManagementPanel);
            rosterFlowLayoutPanel.Location = new Point(3, 3);
            rosterFlowLayoutPanel.Name = "rosterFlowLayoutPanel";
            rosterFlowLayoutPanel.Size = new Size(1094, 417);
            rosterFlowLayoutPanel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(playerDropDown);
            panel1.Controls.Add(playerManagementLabel);
            panel1.Controls.Add(teamRecordLabel);
            panel1.Controls.Add(currentTeamRoster);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(rosterLabel);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1081, 48);
            panel1.TabIndex = 3;
            // 
            // playerDropDown
            // 
            playerDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
            playerDropDown.FormattingEnabled = true;
            playerDropDown.Items.AddRange(new object[] { "New York Bankers", "Philadelphia Hawks", "Boston Beavers", "Miami Crocodiles", "Atlanta Raptors", "Washington Wolves", "Charlotte Vipers", "Orlando Knights", "Detroit Thunder", "Cleveland Crows", "Milwaukee Spartans", "Indianapolis Falcons", "Chicago Raiders", "Brooklyn Bulls", "Toronto Titans", "Los Angeles Warriors", "San Francisco Saints", "Phoenix Dragons", "Dallas Cowboys", "Houston Eagles", "Denver Raccoons", "Portland Tornados", "San Antonio Kangaroos", "Las Vegas Dimes", "Seattle Panthers", "Sacramento Sharks", "Salt Lake City Lions", "Oklahoma City Sonics", "New Orleans Raiders", "Minneapolis Seals" });
            playerDropDown.Location = new Point(835, 14);
            playerDropDown.Name = "playerDropDown";
            playerDropDown.Size = new Size(142, 23);
            playerDropDown.TabIndex = 12;
            playerDropDown.SelectedIndexChanged += playerDropDown_SelectedIndexChanged;
            // 
            // playerManagementLabel
            // 
            playerManagementLabel.AutoSize = true;
            playerManagementLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            playerManagementLabel.Location = new Point(666, 17);
            playerManagementLabel.Name = "playerManagementLabel";
            playerManagementLabel.Size = new Size(131, 17);
            playerManagementLabel.TabIndex = 11;
            playerManagementLabel.Text = "Player Management";
            // 
            // teamRecordLabel
            // 
            teamRecordLabel.AutoSize = true;
            teamRecordLabel.Location = new Point(489, 19);
            teamRecordLabel.Name = "teamRecordLabel";
            teamRecordLabel.Size = new Size(81, 15);
            teamRecordLabel.TabIndex = 10;
            teamRecordLabel.Text = "Team Record: ";
            // 
            // currentTeamRoster
            // 
            currentTeamRoster.DropDownStyle = ComboBoxStyle.DropDownList;
            currentTeamRoster.FormattingEnabled = true;
            currentTeamRoster.Items.AddRange(new object[] { "New York Sentinels", "Philadelphia Hawks", "Boston Beavers", "Miami Crocodiles", "Atlanta Raptors", "Washington Wolves", "Charlotte Vipers", "Orlando Knights", "Detroit Thunder", "Cleveland Crows", "Milwaukee Spartans", "Indianapolis Falcons", "Chicago Raiders", "Brooklyn Bulls", "Toronto Titans", "Los Angeles Warriors", "San Francisco Saints", "Phoenix Dragons", "Dallas Cowboys", "Houston Eagles", "Denver Raccoons", "Portland Tornados", "San Antonio Kangaroos", "Las Vegas Dimes", "Seattle Panthers", "Sacramento Sharks", "Salt Lake City Lions", "Oklahoma City Sonics", "New Orleans Raiders", "Minneapolis Seals" });
            currentTeamRoster.Location = new Point(20, 19);
            currentTeamRoster.Name = "currentTeamRoster";
            currentTeamRoster.Size = new Size(142, 23);
            currentTeamRoster.TabIndex = 9;
            currentTeamRoster.SelectedIndexChanged += currentTeamRoster_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 0);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 1;
            label1.Text = "Current Team:";
            // 
            // rosterLabel
            // 
            rosterLabel.AutoSize = true;
            rosterLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rosterLabel.Location = new Point(280, 14);
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
            rosterDataGridView.Location = new Point(3, 57);
            rosterDataGridView.Name = "rosterDataGridView";
            rosterDataGridView.ReadOnly = true;
            rosterDataGridView.Size = new Size(638, 352);
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
            // rosterManagementPanel
            // 
            rosterManagementPanel.Controls.Add(minutesSelectorButton);
            rosterManagementPanel.Controls.Add(positionChangeButton);
            rosterManagementPanel.Controls.Add(label4);
            rosterManagementPanel.Controls.Add(newPositionDropDown);
            rosterManagementPanel.Controls.Add(label5);
            rosterManagementPanel.Controls.Add(currentPositionLabel);
            rosterManagementPanel.Controls.Add(minutesSelectorUpDown);
            rosterManagementPanel.Controls.Add(label3);
            rosterManagementPanel.Controls.Add(label2);
            rosterManagementPanel.Location = new Point(647, 57);
            rosterManagementPanel.Name = "rosterManagementPanel";
            rosterManagementPanel.Size = new Size(437, 352);
            rosterManagementPanel.TabIndex = 5;
            // 
            // minutesSelectorButton
            // 
            minutesSelectorButton.Location = new Point(316, 22);
            minutesSelectorButton.Name = "minutesSelectorButton";
            minutesSelectorButton.Size = new Size(75, 23);
            minutesSelectorButton.TabIndex = 19;
            minutesSelectorButton.Text = "Confirm";
            minutesSelectorButton.UseVisualStyleBackColor = true;
            minutesSelectorButton.Click += minutesSelectorButton_Click;
            // 
            // positionChangeButton
            // 
            positionChangeButton.Location = new Point(316, 185);
            positionChangeButton.Name = "positionChangeButton";
            positionChangeButton.Size = new Size(75, 23);
            positionChangeButton.TabIndex = 19;
            positionChangeButton.Text = "Confirm";
            positionChangeButton.UseVisualStyleBackColor = true;
            positionChangeButton.Click += positionChangeButton_Click;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(22, 233);
            label4.Name = "label4";
            label4.Size = new Size(215, 65);
            label4.TabIndex = 18;
            label4.Text = "(make sure you pick a suitable position for the player's\r\nplaystyle. e.g. if you set a point guard to center, they\r\nwill not be able to defend these players and create\r\nvulnerable matchups)\r\n";
            // 
            // newPositionDropDown
            // 
            newPositionDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
            newPositionDropDown.FormattingEnabled = true;
            newPositionDropDown.Items.AddRange(new object[] { "Point Guard", "Shooting Guard", "Small Forward", "Power Forward", "Center" });
            newPositionDropDown.Location = new Point(126, 186);
            newPositionDropDown.Name = "newPositionDropDown";
            newPositionDropDown.Size = new Size(142, 23);
            newPositionDropDown.TabIndex = 17;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(22, 192);
            label5.Name = "label5";
            label5.Size = new Size(98, 17);
            label5.TabIndex = 16;
            label5.Text = "New Position: ";
            // 
            // currentPositionLabel
            // 
            currentPositionLabel.AutoSize = true;
            currentPositionLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            currentPositionLabel.Location = new Point(22, 143);
            currentPositionLabel.Name = "currentPositionLabel";
            currentPositionLabel.Size = new Size(117, 17);
            currentPositionLabel.TabIndex = 15;
            currentPositionLabel.Text = "Current Position: ";
            // 
            // minutesSelectorUpDown
            // 
            minutesSelectorUpDown.Location = new Point(146, 22);
            minutesSelectorUpDown.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            minutesSelectorUpDown.Name = "minutesSelectorUpDown";
            minutesSelectorUpDown.Size = new Size(48, 23);
            minutesSelectorUpDown.TabIndex = 14;
            minutesSelectorUpDown.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 6.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(22, 49);
            label3.Name = "label3";
            label3.Size = new Size(154, 75);
            label3.TabIndex = 13;
            label3.Text = "(each position can only have a max of 48 mins played\r\nin total.) any positions that have over 48 mins assigned\r\nthe players at that position will have their minutes reduced\r\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(22, 22);
            label2.Name = "label2";
            label2.Size = new Size(118, 17);
            label2.TabIndex = 12;
            label2.Text = "Minutes Assigned";
            // 
            // RosterUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rosterFlowLayoutPanel);
            Name = "RosterUserControl";
            Size = new Size(1100, 630);
            Load += RosterUserControl_Load;
            rosterFlowLayoutPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rosterDataGridView).EndInit();
            rosterManagementPanel.ResumeLayout(false);
            rosterManagementPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)minutesSelectorUpDown).EndInit();
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
        private Label playerManagementLabel;
        public ComboBox playerDropDown;
        private Panel rosterManagementPanel;
        private Label label3;
        private Label label2;
        public ComboBox newPositionDropDown;
        private Label label5;
        private Label currentPositionLabel;
        private NumericUpDown minutesSelectorUpDown;
        private Label label4;
        private Button positionChangeButton;
        private Button minutesSelectorButton;
    }
}
