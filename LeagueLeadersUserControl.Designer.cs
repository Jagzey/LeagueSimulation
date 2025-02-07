namespace LeagueSimulation
{
    partial class LeagueLeadersUserControl
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
            leagueLeadersFlowLayoutPanel = new FlowLayoutPanel();
            leagueLeadersTitlePanel = new Panel();
            currentStat = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            leagueLeaderDataGridView = new DataGridView();
            PlayerFirstname = new DataGridViewTextBoxColumn();
            PlayerSurname = new DataGridViewTextBoxColumn();
            TeamName = new DataGridViewTextBoxColumn();
            leagueLeadersFlowLayoutPanel.SuspendLayout();
            leagueLeadersTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)leagueLeaderDataGridView).BeginInit();
            SuspendLayout();
            // 
            // leagueLeadersFlowLayoutPanel
            // 
            leagueLeadersFlowLayoutPanel.Controls.Add(leagueLeadersTitlePanel);
            leagueLeadersFlowLayoutPanel.Controls.Add(leagueLeaderDataGridView);
            leagueLeadersFlowLayoutPanel.Location = new Point(3, 3);
            leagueLeadersFlowLayoutPanel.Name = "leagueLeadersFlowLayoutPanel";
            leagueLeadersFlowLayoutPanel.Size = new Size(1094, 624);
            leagueLeadersFlowLayoutPanel.TabIndex = 0;
            // 
            // leagueLeadersTitlePanel
            // 
            leagueLeadersTitlePanel.Controls.Add(currentStat);
            leagueLeadersTitlePanel.Controls.Add(label2);
            leagueLeadersTitlePanel.Controls.Add(label1);
            leagueLeadersTitlePanel.Location = new Point(3, 3);
            leagueLeadersTitlePanel.Name = "leagueLeadersTitlePanel";
            leagueLeadersTitlePanel.Size = new Size(1091, 52);
            leagueLeadersTitlePanel.TabIndex = 0;
            // 
            // currentStat
            // 
            currentStat.DropDownStyle = ComboBoxStyle.DropDownList;
            currentStat.FormattingEnabled = true;
            currentStat.Items.AddRange(new object[] { "Points", "Rebounds", "Assists", "Steals", "Turnovers", "Blocks", "Game Value", "Defense Value" });
            currentStat.Location = new Point(50, 16);
            currentStat.Name = "currentStat";
            currentStat.Size = new Size(142, 23);
            currentStat.TabIndex = 10;
            currentStat.SelectedIndexChanged += currentStat_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 19);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 2;
            label2.Text = "Value: ";
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(476, 2);
            label1.Name = "label1";
            label1.Size = new Size(251, 42);
            label1.TabIndex = 1;
            label1.Text = "League Leaders Menu";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // leagueLeaderDataGridView
            // 
            leagueLeaderDataGridView.AllowUserToAddRows = false;
            leagueLeaderDataGridView.AllowUserToDeleteRows = false;
            leagueLeaderDataGridView.Columns.AddRange(new DataGridViewColumn[] { PlayerFirstname, PlayerSurname, TeamName });
            leagueLeaderDataGridView.Location = new Point(3, 61);
            leagueLeaderDataGridView.Name = "leagueLeaderDataGridView";
            leagueLeaderDataGridView.ReadOnly = true;
            leagueLeaderDataGridView.Size = new Size(1072, 540);
            leagueLeaderDataGridView.TabIndex = 3;
            leagueLeaderDataGridView.CellClick += leagueLeaderDataGridView_CellClick;
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
            // TeamName
            // 
            TeamName.DataPropertyName = "teamName";
            TeamName.HeaderText = "Team Name";
            TeamName.Name = "TeamName";
            TeamName.ReadOnly = true;
            TeamName.Width = 160;
            // 
            // LeagueLeadersUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(leagueLeadersFlowLayoutPanel);
            Name = "LeagueLeadersUserControl";
            Size = new Size(1100, 630);
            leagueLeadersFlowLayoutPanel.ResumeLayout(false);
            leagueLeadersTitlePanel.ResumeLayout(false);
            leagueLeadersTitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)leagueLeaderDataGridView).EndInit();
            ResumeLayout(false);
        }

        private void LeagueLeaderDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private FlowLayoutPanel leagueLeadersFlowLayoutPanel;
        private Panel leagueLeadersTitlePanel;
        private Label label2;
        private Label label1;
        private ComboBox currentStat;
        private DataGridView leagueLeaderDataGridView;
        private DataGridViewTextBoxColumn PlayerFirstname;
        private DataGridViewTextBoxColumn PlayerSurname;
        private DataGridViewTextBoxColumn TeamName;
    }
}
