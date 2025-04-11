namespace LeagueSimulation
{
    partial class TradeProposalUserControl
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            currentTeamDataGridView = new DataGridView();
            teamToTradeWithDataGridView = new DataGridView();
            teamToTradeWithDropDown = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            playerToTradeAway1 = new TextBox();
            playerToTradeAway2 = new TextBox();
            playerToTradeAway3 = new TextBox();
            label6 = new Label();
            playerToTradeFor1 = new TextBox();
            playerToTradeFor2 = new TextBox();
            playerToTradeFor3 = new TextBox();
            proposeTradeButton = new Button();
            teamRecordLabel = new Label();
            currentTeamRecord = new Label();
            PlayerFirstname = new DataGridViewTextBoxColumn();
            PlayerSurname = new DataGridViewTextBoxColumn();
            PTS = new DataGridViewTextBoxColumn();
            REB = new DataGridViewTextBoxColumn();
            AST = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)currentTeamDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)teamToTradeWithDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(413, 13);
            label1.Name = "label1";
            label1.Size = new Size(290, 37);
            label1.TabIndex = 0;
            label1.Text = "Trade Proposal Menu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(18, 13);
            label2.Name = "label2";
            label2.Size = new Size(190, 37);
            label2.TabIndex = 0;
            label2.Text = "Current Team";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(732, 13);
            label3.Name = "label3";
            label3.Size = new Size(272, 37);
            label3.TabIndex = 0;
            label3.Text = "Team To Trade With";
            // 
            // currentTeamDataGridView
            // 
            currentTeamDataGridView.AllowUserToAddRows = false;
            currentTeamDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 8F);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            currentTeamDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            currentTeamDataGridView.Columns.AddRange(new DataGridViewColumn[] { PlayerFirstname, PlayerSurname, PTS, REB, AST });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 8F);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            currentTeamDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            currentTeamDataGridView.Location = new Point(3, 120);
            currentTeamDataGridView.Name = "currentTeamDataGridView";
            currentTeamDataGridView.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Control;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 8F);
            dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            currentTeamDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            currentTeamDataGridView.Size = new Size(400, 490);
            currentTeamDataGridView.TabIndex = 5;
            currentTeamDataGridView.CellClick += currentTeamDataGridView_CellClick;
            // 
            // teamToTradeWithDataGridView
            // 
            teamToTradeWithDataGridView.AllowUserToAddRows = false;
            teamToTradeWithDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = SystemColors.Control;
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 8F);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            teamToTradeWithDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            teamToTradeWithDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = SystemColors.Window;
            dataGridViewCellStyle11.Font = new Font("Segoe UI", 8F);
            dataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            teamToTradeWithDataGridView.DefaultCellStyle = dataGridViewCellStyle11;
            teamToTradeWithDataGridView.Location = new Point(697, 120);
            teamToTradeWithDataGridView.Name = "teamToTradeWithDataGridView";
            teamToTradeWithDataGridView.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = SystemColors.Control;
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 8F);
            dataGridViewCellStyle12.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            teamToTradeWithDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            teamToTradeWithDataGridView.Size = new Size(400, 490);
            teamToTradeWithDataGridView.TabIndex = 6;
            teamToTradeWithDataGridView.CellClick += teamToTradeWithDataGridView_CellClick;
            // 
            // teamToTradeWithDropDown
            // 
            teamToTradeWithDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
            teamToTradeWithDropDown.FormattingEnabled = true;
            teamToTradeWithDropDown.Items.AddRange(new object[] { "New York Bankers", "Philadelphia Hawks", "Boston Beavers", "Miami Crocodiles", "Atlanta Raptors", "Washington Wolves", "Charlotte Vipers", "Orlando Knights", "Detroit Thunder", "Cleveland Crows", "Milwaukee Spartans", "Indianapolis Falcons", "Chicago Raiders", "Brooklyn Bulls", "Toronto Titans", "Los Angeles Warriors", "San Francisco Saints", "Phoenix Dragons", "Dallas Cowboys", "Houston Eagles", "Denver Raccoons", "Portland Tornados", "San Antonio Kangaroos", "Las Vegas Dimes", "Seattle Panthers", "Sacramento Sharks", "Salt Lake City Lions", "Oklahoma City Sonics", "New Orleans Raiders", "Minneapolis Seals" });
            teamToTradeWithDropDown.Location = new Point(831, 64);
            teamToTradeWithDropDown.Name = "teamToTradeWithDropDown";
            teamToTradeWithDropDown.Size = new Size(142, 23);
            teamToTradeWithDropDown.TabIndex = 11;
            teamToTradeWithDropDown.SelectedIndexChanged += teamToTradeWithDropDown_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(732, 67);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 10;
            label4.Text = "Current Team:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(429, 120);
            label5.Name = "label5";
            label5.Size = new Size(121, 15);
            label5.TabIndex = 12;
            label5.Text = "Players to trade away:";
            // 
            // playerToTradeAway1
            // 
            playerToTradeAway1.Location = new Point(429, 147);
            playerToTradeAway1.Name = "playerToTradeAway1";
            playerToTradeAway1.Size = new Size(242, 23);
            playerToTradeAway1.TabIndex = 13;
            // 
            // playerToTradeAway2
            // 
            playerToTradeAway2.Location = new Point(429, 185);
            playerToTradeAway2.Name = "playerToTradeAway2";
            playerToTradeAway2.Size = new Size(242, 23);
            playerToTradeAway2.TabIndex = 13;
            // 
            // playerToTradeAway3
            // 
            playerToTradeAway3.Location = new Point(429, 224);
            playerToTradeAway3.Name = "playerToTradeAway3";
            playerToTradeAway3.Size = new Size(242, 23);
            playerToTradeAway3.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(429, 284);
            label6.Name = "label6";
            label6.Size = new Size(109, 15);
            label6.TabIndex = 12;
            label6.Text = "Players to trade for:";
            // 
            // playerToTradeFor1
            // 
            playerToTradeFor1.Location = new Point(429, 302);
            playerToTradeFor1.Name = "playerToTradeFor1";
            playerToTradeFor1.Size = new Size(242, 23);
            playerToTradeFor1.TabIndex = 13;
            // 
            // playerToTradeFor2
            // 
            playerToTradeFor2.Location = new Point(429, 350);
            playerToTradeFor2.Name = "playerToTradeFor2";
            playerToTradeFor2.Size = new Size(242, 23);
            playerToTradeFor2.TabIndex = 13;
            // 
            // playerToTradeFor3
            // 
            playerToTradeFor3.Location = new Point(429, 394);
            playerToTradeFor3.Name = "playerToTradeFor3";
            playerToTradeFor3.Size = new Size(242, 23);
            playerToTradeFor3.TabIndex = 13;
            // 
            // proposeTradeButton
            // 
            proposeTradeButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            proposeTradeButton.Location = new Point(454, 474);
            proposeTradeButton.Name = "proposeTradeButton";
            proposeTradeButton.Size = new Size(182, 40);
            proposeTradeButton.TabIndex = 14;
            proposeTradeButton.Text = "Propose Trade";
            proposeTradeButton.UseVisualStyleBackColor = true;
            proposeTradeButton.Click += proposeTradeButton_Click;
            // 
            // teamRecordLabel
            // 
            teamRecordLabel.AutoSize = true;
            teamRecordLabel.Location = new Point(732, 93);
            teamRecordLabel.Name = "teamRecordLabel";
            teamRecordLabel.Size = new Size(81, 15);
            teamRecordLabel.TabIndex = 15;
            teamRecordLabel.Text = "Team Record: ";
            // 
            // currentTeamRecord
            // 
            currentTeamRecord.AutoSize = true;
            currentTeamRecord.Location = new Point(18, 93);
            currentTeamRecord.Name = "currentTeamRecord";
            currentTeamRecord.Size = new Size(81, 15);
            currentTeamRecord.TabIndex = 15;
            currentTeamRecord.Text = "Team Record: ";
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
            // PTS
            // 
            PTS.DataPropertyName = "PTS";
            PTS.HeaderText = "PTS";
            PTS.Name = "PTS";
            PTS.ReadOnly = true;
            PTS.Width = 60;
            // 
            // REB
            // 
            REB.DataPropertyName = "REB";
            REB.HeaderText = "REB";
            REB.Name = "REB";
            REB.ReadOnly = true;
            REB.Width = 60;
            // 
            // AST
            // 
            AST.DataPropertyName = "AST";
            AST.HeaderText = "AST";
            AST.Name = "AST";
            AST.ReadOnly = true;
            AST.Width = 60;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "playerForename";
            dataGridViewTextBoxColumn1.HeaderText = "Firstname";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 90;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "playerSurname";
            dataGridViewTextBoxColumn2.HeaderText = "Surname";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 90;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "PTS";
            dataGridViewTextBoxColumn3.HeaderText = "PTS";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "REB";
            dataGridViewTextBoxColumn4.HeaderText = "REB";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "AST";
            dataGridViewTextBoxColumn5.HeaderText = "AST";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 60;
            // 
            // TradeProposalUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(currentTeamRecord);
            Controls.Add(teamRecordLabel);
            Controls.Add(proposeTradeButton);
            Controls.Add(playerToTradeFor3);
            Controls.Add(playerToTradeFor2);
            Controls.Add(playerToTradeAway3);
            Controls.Add(playerToTradeFor1);
            Controls.Add(playerToTradeAway2);
            Controls.Add(label6);
            Controls.Add(playerToTradeAway1);
            Controls.Add(label5);
            Controls.Add(teamToTradeWithDropDown);
            Controls.Add(label4);
            Controls.Add(teamToTradeWithDataGridView);
            Controls.Add(currentTeamDataGridView);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "TradeProposalUserControl";
            Size = new Size(1100, 630);
            ((System.ComponentModel.ISupportInitialize)currentTeamDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)teamToTradeWithDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DataGridView currentTeamDataGridView;
        private DataGridView teamToTradeWithDataGridView;
        public ComboBox teamToTradeWithDropDown;
        private Label label4;
        private Label label5;
        private TextBox playerToTradeAway1;
        private TextBox playerToTradeAway2;
        private TextBox playerToTradeAway3;
        private Label label6;
        private TextBox playerToTradeFor1;
        private TextBox playerToTradeFor2;
        private TextBox playerToTradeFor3;
        private Button proposeTradeButton;
        private Label teamRecordLabel;
        private Label currentTeamRecord;
        private DataGridViewTextBoxColumn PlayerFirstname;
        private DataGridViewTextBoxColumn PlayerSurname;
        private DataGridViewTextBoxColumn PTS;
        private DataGridViewTextBoxColumn REB;
        private DataGridViewTextBoxColumn AST;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
