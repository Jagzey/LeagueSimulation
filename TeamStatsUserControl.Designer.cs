namespace LeagueSimulation
{
    partial class TeamStatsUserControl
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
            panel1 = new Panel();
            playoffsButton = new Button();
            seasonSummaryYear = new NumericUpDown();
            seasonYearLabel = new Label();
            label1 = new Label();
            panel2 = new Panel();
            seasonDataGridView = new DataGridView();
            teamName = new DataGridViewTextBoxColumn();
            GP = new DataGridViewTextBoxColumn();
            W = new DataGridViewTextBoxColumn();
            L = new DataGridViewTextBoxColumn();
            winPct = new DataGridViewTextBoxColumn();
            gameValue = new DataGridViewTextBoxColumn();
            age = new DataGridViewTextBoxColumn();
            FGM = new DataGridViewTextBoxColumn();
            FGA = new DataGridViewTextBoxColumn();
            seasonFGPCT = new DataGridViewTextBoxColumn();
            TFGM = new DataGridViewTextBoxColumn();
            TFGA = new DataGridViewTextBoxColumn();
            seasonTFGPCT = new DataGridViewTextBoxColumn();
            FTM = new DataGridViewTextBoxColumn();
            FTA = new DataGridViewTextBoxColumn();
            FTPCT = new DataGridViewTextBoxColumn();
            seasonPTS = new DataGridViewTextBoxColumn();
            seasonREB = new DataGridViewTextBoxColumn();
            seasonAST = new DataGridViewTextBoxColumn();
            STL = new DataGridViewTextBoxColumn();
            BLK = new DataGridViewTextBoxColumn();
            TOV = new DataGridViewTextBoxColumn();
            PF = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)seasonSummaryYear).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)seasonDataGridView).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(playoffsButton);
            panel1.Controls.Add(seasonSummaryYear);
            panel1.Controls.Add(seasonYearLabel);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1294, 52);
            panel1.TabIndex = 0;
            // 
            // playoffsButton
            // 
            playoffsButton.Location = new Point(1017, 19);
            playoffsButton.Name = "playoffsButton";
            playoffsButton.Size = new Size(75, 23);
            playoffsButton.TabIndex = 5;
            playoffsButton.Text = "Playoffs?";
            playoffsButton.UseVisualStyleBackColor = true;
            playoffsButton.Click += playoffsButton_Click;
            // 
            // seasonSummaryYear
            // 
            seasonSummaryYear.Location = new Point(129, 19);
            seasonSummaryYear.Maximum = new decimal(new int[] { 2024, 0, 0, 0 });
            seasonSummaryYear.Minimum = new decimal(new int[] { 2024, 0, 0, 0 });
            seasonSummaryYear.Name = "seasonSummaryYear";
            seasonSummaryYear.Size = new Size(120, 23);
            seasonSummaryYear.TabIndex = 4;
            seasonSummaryYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            seasonSummaryYear.ValueChanged += seasonSummaryYear_ValueChanged;
            // 
            // seasonYearLabel
            // 
            seasonYearLabel.AutoSize = true;
            seasonYearLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            seasonYearLabel.Location = new Point(39, 19);
            seasonYearLabel.Name = "seasonYearLabel";
            seasonYearLabel.Size = new Size(95, 20);
            seasonYearLabel.TabIndex = 3;
            seasonYearLabel.Text = "Season Year: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(584, 12);
            label1.Name = "label1";
            label1.Size = new Size(180, 30);
            label1.TabIndex = 0;
            label1.Text = "Team Stats Menu";
            // 
            // panel2
            // 
            panel2.Controls.Add(seasonDataGridView);
            panel2.Location = new Point(3, 61);
            panel2.Name = "panel2";
            panel2.Size = new Size(1294, 656);
            panel2.TabIndex = 1;
            // 
            // seasonDataGridView
            // 
            seasonDataGridView.AllowUserToAddRows = false;
            seasonDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            seasonDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            seasonDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            seasonDataGridView.Columns.AddRange(new DataGridViewColumn[] { teamName, GP, W, L, winPct, gameValue, age, FGM, FGA, seasonFGPCT, TFGM, TFGA, seasonTFGPCT, FTM, FTA, FTPCT, seasonPTS, seasonREB, seasonAST, STL, BLK, TOV, PF });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            seasonDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            seasonDataGridView.Location = new Point(23, 21);
            seasonDataGridView.Name = "seasonDataGridView";
            seasonDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            seasonDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            seasonDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            seasonDataGridView.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            seasonDataGridView.RowTemplate.DefaultCellStyle.BackColor = SystemColors.Window;
            seasonDataGridView.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 7F);
            seasonDataGridView.RowTemplate.DefaultCellStyle.ForeColor = SystemColors.WindowText;
            seasonDataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            seasonDataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            seasonDataGridView.Size = new Size(1248, 605);
            seasonDataGridView.TabIndex = 11;
            seasonDataGridView.CellClick += seasonDataGridView_CellClick;
            // 
            // teamName
            // 
            teamName.DataPropertyName = "teamName";
            teamName.HeaderText = "Team Name";
            teamName.Name = "teamName";
            teamName.ReadOnly = true;
            teamName.Width = 130;
            // 
            // GP
            // 
            GP.DataPropertyName = "gamesPlayed";
            GP.HeaderText = "GP";
            GP.Name = "GP";
            GP.ReadOnly = true;
            GP.Width = 30;
            // 
            // W
            // 
            W.DataPropertyName = "wins";
            W.HeaderText = "W";
            W.Name = "W";
            W.ReadOnly = true;
            W.Width = 30;
            // 
            // L
            // 
            L.DataPropertyName = "losses";
            L.HeaderText = "L";
            L.Name = "L";
            L.ReadOnly = true;
            L.Width = 30;
            // 
            // winPct
            // 
            winPct.DataPropertyName = "winPct";
            winPct.HeaderText = "winPct";
            winPct.Name = "winPct";
            winPct.ReadOnly = true;
            winPct.Width = 50;
            // 
            // gameValue
            // 
            gameValue.DataPropertyName = "avgGameValue";
            gameValue.HeaderText = "Game Value";
            gameValue.Name = "gameValue";
            gameValue.ReadOnly = true;
            gameValue.Width = 80;
            // 
            // age
            // 
            age.DataPropertyName = "avgAge";
            age.HeaderText = "Age";
            age.Name = "age";
            age.ReadOnly = true;
            age.Width = 40;
            // 
            // FGM
            // 
            FGM.DataPropertyName = "avgFGM";
            FGM.HeaderText = "FGM";
            FGM.Name = "FGM";
            FGM.ReadOnly = true;
            FGM.Width = 50;
            // 
            // FGA
            // 
            FGA.DataPropertyName = "avgFGA";
            FGA.HeaderText = "FGA";
            FGA.Name = "FGA";
            FGA.ReadOnly = true;
            FGA.Width = 50;
            // 
            // seasonFGPCT
            // 
            seasonFGPCT.DataPropertyName = "avgFGPCT";
            seasonFGPCT.HeaderText = "FG%";
            seasonFGPCT.Name = "seasonFGPCT";
            seasonFGPCT.ReadOnly = true;
            seasonFGPCT.Width = 55;
            // 
            // TFGM
            // 
            TFGM.DataPropertyName = "avgTFGM";
            TFGM.HeaderText = "3PM";
            TFGM.Name = "TFGM";
            TFGM.ReadOnly = true;
            TFGM.Width = 50;
            // 
            // TFGA
            // 
            TFGA.DataPropertyName = "avgTFGA";
            TFGA.HeaderText = "3PA";
            TFGA.Name = "TFGA";
            TFGA.ReadOnly = true;
            TFGA.Width = 50;
            // 
            // seasonTFGPCT
            // 
            seasonTFGPCT.DataPropertyName = "avgTFGPCT";
            seasonTFGPCT.HeaderText = "3P%";
            seasonTFGPCT.Name = "seasonTFGPCT";
            seasonTFGPCT.ReadOnly = true;
            seasonTFGPCT.Width = 55;
            // 
            // FTM
            // 
            FTM.DataPropertyName = "avgFTM";
            FTM.HeaderText = "FTM";
            FTM.Name = "FTM";
            FTM.ReadOnly = true;
            FTM.Width = 50;
            // 
            // FTA
            // 
            FTA.DataPropertyName = "avgFTA";
            FTA.HeaderText = "FTA";
            FTA.Name = "FTA";
            FTA.ReadOnly = true;
            FTA.Width = 50;
            // 
            // FTPCT
            // 
            FTPCT.DataPropertyName = "avgFTPCT";
            FTPCT.HeaderText = "FT%";
            FTPCT.Name = "FTPCT";
            FTPCT.ReadOnly = true;
            FTPCT.Width = 55;
            // 
            // seasonPTS
            // 
            seasonPTS.DataPropertyName = "avgPTS";
            seasonPTS.HeaderText = "PTS";
            seasonPTS.Name = "seasonPTS";
            seasonPTS.ReadOnly = true;
            seasonPTS.Width = 50;
            // 
            // seasonREB
            // 
            seasonREB.DataPropertyName = "avgREB";
            seasonREB.HeaderText = "REB";
            seasonREB.Name = "seasonREB";
            seasonREB.ReadOnly = true;
            seasonREB.Width = 50;
            // 
            // seasonAST
            // 
            seasonAST.DataPropertyName = "avgAST";
            seasonAST.HeaderText = "AST";
            seasonAST.Name = "seasonAST";
            seasonAST.ReadOnly = true;
            seasonAST.Width = 50;
            // 
            // STL
            // 
            STL.DataPropertyName = "avgSTL";
            STL.HeaderText = "STL";
            STL.Name = "STL";
            STL.ReadOnly = true;
            STL.Width = 50;
            // 
            // BLK
            // 
            BLK.DataPropertyName = "avgBLK";
            BLK.HeaderText = "BLK";
            BLK.Name = "BLK";
            BLK.ReadOnly = true;
            BLK.Width = 50;
            // 
            // TOV
            // 
            TOV.DataPropertyName = "avgTOV";
            TOV.HeaderText = "TOV";
            TOV.Name = "TOV";
            TOV.ReadOnly = true;
            TOV.Width = 50;
            // 
            // PF
            // 
            PF.DataPropertyName = "avgPF";
            PF.HeaderText = "PF";
            PF.Name = "PF";
            PF.ReadOnly = true;
            PF.Width = 50;
            // 
            // TeamStatsUserControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "TeamStatsUserControl";
            Size = new Size(1300, 720);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)seasonSummaryYear).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)seasonDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private DataGridView seasonDataGridView;
        private NumericUpDown seasonSummaryYear;
        private Label seasonYearLabel;
        private Button playoffsButton;
        private DataGridViewTextBoxColumn teamName;
        private DataGridViewTextBoxColumn GP;
        private DataGridViewTextBoxColumn W;
        private DataGridViewTextBoxColumn L;
        private DataGridViewTextBoxColumn winPct;
        private DataGridViewTextBoxColumn gameValue;
        private DataGridViewTextBoxColumn age;
        private DataGridViewTextBoxColumn FGM;
        private DataGridViewTextBoxColumn FGA;
        private DataGridViewTextBoxColumn seasonFGPCT;
        private DataGridViewTextBoxColumn TFGM;
        private DataGridViewTextBoxColumn TFGA;
        private DataGridViewTextBoxColumn seasonTFGPCT;
        private DataGridViewTextBoxColumn FTM;
        private DataGridViewTextBoxColumn FTA;
        private DataGridViewTextBoxColumn FTPCT;
        private DataGridViewTextBoxColumn seasonPTS;
        private DataGridViewTextBoxColumn seasonREB;
        private DataGridViewTextBoxColumn seasonAST;
        private DataGridViewTextBoxColumn STL;
        private DataGridViewTextBoxColumn BLK;
        private DataGridViewTextBoxColumn TOV;
        private DataGridViewTextBoxColumn PF;
    }
}
