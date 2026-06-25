namespace LeagueSimulation
{
    partial class ViewGameResultsUserControl
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            flowLayoutPanel1 = new FlowLayoutPanel();
            gameResultPanel = new Panel();
            gameResultLabel = new Label();
            gameDataPanel = new Panel();
            team2DataGridView = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            team2FTM = new DataGridViewTextBoxColumn();
            team2FTA = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            team2PF = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            team1DataGridView = new DataGridView();
            playerName = new DataGridViewTextBoxColumn();
            age = new DataGridViewTextBoxColumn();
            playerPosition = new DataGridViewTextBoxColumn();
            MP = new DataGridViewTextBoxColumn();
            FGM = new DataGridViewTextBoxColumn();
            FGA = new DataGridViewTextBoxColumn();
            TFGM = new DataGridViewTextBoxColumn();
            TFGA = new DataGridViewTextBoxColumn();
            FTM = new DataGridViewTextBoxColumn();
            FTA = new DataGridViewTextBoxColumn();
            PTS = new DataGridViewTextBoxColumn();
            REB = new DataGridViewTextBoxColumn();
            AST = new DataGridViewTextBoxColumn();
            STL = new DataGridViewTextBoxColumn();
            BLK = new DataGridViewTextBoxColumn();
            TOV = new DataGridViewTextBoxColumn();
            PF = new DataGridViewTextBoxColumn();
            playstyle = new DataGridViewTextBoxColumn();
            gameValue = new DataGridViewTextBoxColumn();
            team2NameDataLabel = new Label();
            team1NameDataLabel = new Label();
            flowLayoutPanel1.SuspendLayout();
            gameResultPanel.SuspendLayout();
            gameDataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)team2DataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)team1DataGridView).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(gameResultPanel);
            flowLayoutPanel1.Controls.Add(gameDataPanel);
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(874, 1025);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // gameResultPanel
            // 
            gameResultPanel.Controls.Add(gameResultLabel);
            gameResultPanel.Location = new Point(3, 3);
            gameResultPanel.Name = "gameResultPanel";
            gameResultPanel.Size = new Size(871, 79);
            gameResultPanel.TabIndex = 1;
            // 
            // gameResultLabel
            // 
            gameResultLabel.AutoSize = true;
            gameResultLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gameResultLabel.Location = new Point(164, 27);
            gameResultLabel.Name = "gameResultLabel";
            gameResultLabel.Size = new Size(521, 25);
            gameResultLabel.TabIndex = 0;
            gameResultLabel.Text = "Minneapolis Seals (27-11) 101-77 Boston Beavers (18-21)";
            // 
            // gameDataPanel
            // 
            gameDataPanel.Controls.Add(team2DataGridView);
            gameDataPanel.Controls.Add(team1DataGridView);
            gameDataPanel.Controls.Add(team2NameDataLabel);
            gameDataPanel.Controls.Add(team1NameDataLabel);
            gameDataPanel.Location = new Point(3, 88);
            gameDataPanel.Name = "gameDataPanel";
            gameDataPanel.Size = new Size(871, 932);
            gameDataPanel.TabIndex = 2;
            // 
            // team2DataGridView
            // 
            team2DataGridView.AllowUserToAddRows = false;
            team2DataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            team2DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            team2DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            team2DataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, team2FTM, team2FTA, dataGridViewTextBoxColumn11, dataGridViewTextBoxColumn12, dataGridViewTextBoxColumn13, dataGridViewTextBoxColumn14, dataGridViewTextBoxColumn15, dataGridViewTextBoxColumn16, team2PF, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn6 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            team2DataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            team2DataGridView.Location = new Point(24, 533);
            team2DataGridView.Name = "team2DataGridView";
            team2DataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            team2DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            team2DataGridView.Size = new Size(808, 352);
            team2DataGridView.TabIndex = 10;
            team2DataGridView.CellClick += team2DataGridView_CellClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "name";
            dataGridViewTextBoxColumn1.HeaderText = "Player Name";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "age";
            dataGridViewTextBoxColumn2.HeaderText = "Age";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 40;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "playerPosition";
            dataGridViewTextBoxColumn3.HeaderText = "Position";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 45;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "MP";
            dataGridViewTextBoxColumn5.HeaderText = "MP";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 40;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.DataPropertyName = "FGM";
            dataGridViewTextBoxColumn7.HeaderText = "FGM";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            dataGridViewTextBoxColumn7.Width = 30;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.DataPropertyName = "FGA";
            dataGridViewTextBoxColumn8.HeaderText = "FGA";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            dataGridViewTextBoxColumn8.Width = 30;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.DataPropertyName = "3PM";
            dataGridViewTextBoxColumn9.HeaderText = "3PM";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            dataGridViewTextBoxColumn9.Width = 30;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.DataPropertyName = "3PA";
            dataGridViewTextBoxColumn10.HeaderText = "3PA";
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            dataGridViewTextBoxColumn10.Width = 30;
            // 
            // team2FTM
            // 
            team2FTM.DataPropertyName = "FTM";
            team2FTM.HeaderText = "FTM";
            team2FTM.Name = "team2FTM";
            team2FTM.ReadOnly = true;
            team2FTM.Width = 30;
            // 
            // team2FTA
            // 
            team2FTA.DataPropertyName = "FTA";
            team2FTA.HeaderText = "FTA";
            team2FTA.Name = "team2FTA";
            team2FTA.ReadOnly = true;
            team2FTA.Width = 30;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.DataPropertyName = "PTS";
            dataGridViewTextBoxColumn11.HeaderText = "PTS";
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            dataGridViewTextBoxColumn11.Width = 30;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewTextBoxColumn12.DataPropertyName = "REB";
            dataGridViewTextBoxColumn12.HeaderText = "REB";
            dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            dataGridViewTextBoxColumn12.ReadOnly = true;
            dataGridViewTextBoxColumn12.Width = 30;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewTextBoxColumn13.DataPropertyName = "AST";
            dataGridViewTextBoxColumn13.HeaderText = "AST";
            dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            dataGridViewTextBoxColumn13.ReadOnly = true;
            dataGridViewTextBoxColumn13.Width = 30;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewTextBoxColumn14.DataPropertyName = "STL";
            dataGridViewTextBoxColumn14.HeaderText = "STL";
            dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            dataGridViewTextBoxColumn14.ReadOnly = true;
            dataGridViewTextBoxColumn14.Width = 30;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewTextBoxColumn15.DataPropertyName = "BLK";
            dataGridViewTextBoxColumn15.HeaderText = "BLK";
            dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            dataGridViewTextBoxColumn15.ReadOnly = true;
            dataGridViewTextBoxColumn15.Width = 30;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewTextBoxColumn16.DataPropertyName = "TOV";
            dataGridViewTextBoxColumn16.HeaderText = "TOV";
            dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            dataGridViewTextBoxColumn16.ReadOnly = true;
            dataGridViewTextBoxColumn16.Width = 30;
            // 
            // team2PF
            // 
            team2PF.DataPropertyName = "PF";
            team2PF.HeaderText = "PF";
            team2PF.Name = "team2PF";
            team2PF.ReadOnly = true;
            team2PF.Width = 30;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "playstyle";
            dataGridViewTextBoxColumn4.HeaderText = "Playstyle";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.DataPropertyName = "gameValue";
            dataGridViewTextBoxColumn6.HeaderText = "Game Value";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Width = 80;
            // 
            // team1DataGridView
            // 
            team1DataGridView.AllowUserToAddRows = false;
            team1DataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            team1DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            team1DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            team1DataGridView.Columns.AddRange(new DataGridViewColumn[] { playerName, age, playerPosition, MP, FGM, FGA, TFGM, TFGA, FTM, FTA, PTS, REB, AST, STL, BLK, TOV, PF, playstyle, gameValue });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            team1DataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            team1DataGridView.Location = new Point(24, 72);
            team1DataGridView.Name = "team1DataGridView";
            team1DataGridView.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            team1DataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.BackColor = SystemColors.Window;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            team1DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle7;
            team1DataGridView.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            team1DataGridView.RowTemplate.DefaultCellStyle.BackColor = SystemColors.Window;
            team1DataGridView.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 7F);
            team1DataGridView.RowTemplate.DefaultCellStyle.ForeColor = SystemColors.WindowText;
            team1DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            team1DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            team1DataGridView.Size = new Size(808, 371);
            team1DataGridView.TabIndex = 9;
            team1DataGridView.CellClick += team1DataGridView_CellClick;
            // 
            // playerName
            // 
            playerName.DataPropertyName = "name";
            playerName.HeaderText = "Player Name";
            playerName.Name = "playerName";
            playerName.ReadOnly = true;
            // 
            // age
            // 
            age.DataPropertyName = "age";
            age.HeaderText = "Age";
            age.Name = "age";
            age.ReadOnly = true;
            age.Width = 40;
            // 
            // playerPosition
            // 
            playerPosition.DataPropertyName = "playerPosition";
            playerPosition.HeaderText = "Position";
            playerPosition.Name = "playerPosition";
            playerPosition.ReadOnly = true;
            playerPosition.Width = 45;
            // 
            // MP
            // 
            MP.DataPropertyName = "MP";
            MP.HeaderText = "MP";
            MP.Name = "MP";
            MP.ReadOnly = true;
            MP.Width = 40;
            // 
            // FGM
            // 
            FGM.DataPropertyName = "FGM";
            FGM.HeaderText = "FGM";
            FGM.Name = "FGM";
            FGM.ReadOnly = true;
            FGM.Width = 30;
            // 
            // FGA
            // 
            FGA.DataPropertyName = "FGA";
            FGA.HeaderText = "FGA";
            FGA.Name = "FGA";
            FGA.ReadOnly = true;
            FGA.Width = 30;
            // 
            // TFGM
            // 
            TFGM.DataPropertyName = "3PM";
            TFGM.HeaderText = "3PM";
            TFGM.Name = "TFGM";
            TFGM.ReadOnly = true;
            TFGM.Width = 30;
            // 
            // TFGA
            // 
            TFGA.DataPropertyName = "3PA";
            TFGA.HeaderText = "3PA";
            TFGA.Name = "TFGA";
            TFGA.ReadOnly = true;
            TFGA.Width = 30;
            // 
            // FTM
            // 
            FTM.DataPropertyName = "FTM";
            FTM.HeaderText = "FTM";
            FTM.Name = "FTM";
            FTM.ReadOnly = true;
            FTM.Width = 30;
            // 
            // FTA
            // 
            FTA.DataPropertyName = "FTA";
            FTA.HeaderText = "FTA";
            FTA.Name = "FTA";
            FTA.ReadOnly = true;
            FTA.Width = 30;
            // 
            // PTS
            // 
            PTS.DataPropertyName = "PTS";
            PTS.HeaderText = "PTS";
            PTS.Name = "PTS";
            PTS.ReadOnly = true;
            PTS.Width = 30;
            // 
            // REB
            // 
            REB.DataPropertyName = "REB";
            REB.HeaderText = "REB";
            REB.Name = "REB";
            REB.ReadOnly = true;
            REB.Width = 30;
            // 
            // AST
            // 
            AST.DataPropertyName = "AST";
            AST.HeaderText = "AST";
            AST.Name = "AST";
            AST.ReadOnly = true;
            AST.Width = 30;
            // 
            // STL
            // 
            STL.DataPropertyName = "STL";
            STL.HeaderText = "STL";
            STL.Name = "STL";
            STL.ReadOnly = true;
            STL.Width = 30;
            // 
            // BLK
            // 
            BLK.DataPropertyName = "BLK";
            BLK.HeaderText = "BLK";
            BLK.Name = "BLK";
            BLK.ReadOnly = true;
            BLK.Width = 30;
            // 
            // TOV
            // 
            TOV.DataPropertyName = "TOV";
            TOV.HeaderText = "TOV";
            TOV.Name = "TOV";
            TOV.ReadOnly = true;
            TOV.Width = 30;
            // 
            // PF
            // 
            PF.DataPropertyName = "PF";
            PF.HeaderText = "PF";
            PF.Name = "PF";
            PF.ReadOnly = true;
            PF.Width = 30;
            // 
            // playstyle
            // 
            playstyle.DataPropertyName = "playstyle";
            playstyle.HeaderText = "Playstyle";
            playstyle.Name = "playstyle";
            playstyle.ReadOnly = true;
            playstyle.Width = 70;
            // 
            // gameValue
            // 
            gameValue.DataPropertyName = "gameValue";
            gameValue.HeaderText = "Game Value";
            gameValue.Name = "gameValue";
            gameValue.ReadOnly = true;
            gameValue.Width = 80;
            // 
            // team2NameDataLabel
            // 
            team2NameDataLabel.AutoSize = true;
            team2NameDataLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            team2NameDataLabel.Location = new Point(24, 491);
            team2NameDataLabel.Name = "team2NameDataLabel";
            team2NameDataLabel.Size = new Size(148, 25);
            team2NameDataLabel.TabIndex = 2;
            team2NameDataLabel.Text = "Boston Beavers";
            // 
            // team1NameDataLabel
            // 
            team1NameDataLabel.AutoSize = true;
            team1NameDataLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            team1NameDataLabel.Location = new Point(24, 35);
            team1NameDataLabel.Name = "team1NameDataLabel";
            team1NameDataLabel.Size = new Size(170, 25);
            team1NameDataLabel.TabIndex = 1;
            team1NameDataLabel.Text = "Minneapolis Seals";
            // 
            // ViewGameResultsUserControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(flowLayoutPanel1);
            Name = "ViewGameResultsUserControl";
            Size = new Size(880, 1080);
            flowLayoutPanel1.ResumeLayout(false);
            gameResultPanel.ResumeLayout(false);
            gameResultPanel.PerformLayout();
            gameDataPanel.ResumeLayout(false);
            gameDataPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)team2DataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)team1DataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Panel gameResultPanel;
        private Label gameResultLabel;
        private Panel gameDataPanel;
        private Label team2NameDataLabel;
        private Label team1NameDataLabel;
        private DataGridView team1DataGridView;
        private DataGridView team2DataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn team2FTM;
        private DataGridViewTextBoxColumn team2FTA;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn team2PF;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn playerName;
        private DataGridViewTextBoxColumn age;
        private DataGridViewTextBoxColumn playerPosition;
        private DataGridViewTextBoxColumn MP;
        private DataGridViewTextBoxColumn FGM;
        private DataGridViewTextBoxColumn FGA;
        private DataGridViewTextBoxColumn TFGM;
        private DataGridViewTextBoxColumn TFGA;
        private DataGridViewTextBoxColumn FTM;
        private DataGridViewTextBoxColumn FTA;
        private DataGridViewTextBoxColumn PTS;
        private DataGridViewTextBoxColumn REB;
        private DataGridViewTextBoxColumn AST;
        private DataGridViewTextBoxColumn STL;
        private DataGridViewTextBoxColumn BLK;
        private DataGridViewTextBoxColumn TOV;
        private DataGridViewTextBoxColumn PF;
        private DataGridViewTextBoxColumn playstyle;
        private DataGridViewTextBoxColumn gameValue;
    }
}
