namespace LeagueSimulation
{
    partial class PlayerStatsUserControl
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
            label1 = new Label();
            playerIdUpDown = new NumericUpDown();
            titleLabel = new Label();
            panel2 = new Panel();
            potentialLabel = new Label();
            overallLabel = new Label();
            label5 = new Label();
            label4 = new Label();
            defenseAttributesLabel = new Label();
            offenseAttributesLabel = new Label();
            awardsLabel = new Label();
            seasonDataGridView = new DataGridView();
            season = new DataGridViewTextBoxColumn();
            age = new DataGridViewTextBoxColumn();
            teamName = new DataGridViewTextBoxColumn();
            playerPosition = new DataGridViewTextBoxColumn();
            MP = new DataGridViewTextBoxColumn();
            gameValue = new DataGridViewTextBoxColumn();
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
            summaryGridView = new DataGridView();
            gamesPlayed = new DataGridViewTextBoxColumn();
            PTS = new DataGridViewTextBoxColumn();
            REB = new DataGridViewTextBoxColumn();
            AST = new DataGridViewTextBoxColumn();
            FGPCT = new DataGridViewTextBoxColumn();
            TFGPCT = new DataGridViewTextBoxColumn();
            ageLabel = new Label();
            dateOfBirthLabel = new Label();
            playstyleLabel = new Label();
            positionLabel = new Label();
            weightLabel = new Label();
            heightLabel = new Label();
            teamNameLabel = new Label();
            playerNameLabel = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)playerIdUpDown).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)seasonDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)summaryGridView).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(playerIdUpDown);
            panel1.Controls.Add(titleLabel);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(950, 43);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 15);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 3;
            label1.Text = "playerId:";
            // 
            // playerIdUpDown
            // 
            playerIdUpDown.Location = new Point(79, 13);
            playerIdUpDown.Maximum = new decimal(new int[] { 450, 0, 0, 0 });
            playerIdUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            playerIdUpDown.Name = "playerIdUpDown";
            playerIdUpDown.Size = new Size(120, 23);
            playerIdUpDown.TabIndex = 2;
            playerIdUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            playerIdUpDown.ValueChanged += playerIdUpDown_ValueChanged;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(487, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(101, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Player Stats Menu";
            // 
            // panel2
            // 
            panel2.Controls.Add(potentialLabel);
            panel2.Controls.Add(overallLabel);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(defenseAttributesLabel);
            panel2.Controls.Add(offenseAttributesLabel);
            panel2.Controls.Add(awardsLabel);
            panel2.Controls.Add(seasonDataGridView);
            panel2.Controls.Add(summaryGridView);
            panel2.Controls.Add(ageLabel);
            panel2.Controls.Add(dateOfBirthLabel);
            panel2.Controls.Add(playstyleLabel);
            panel2.Controls.Add(positionLabel);
            panel2.Controls.Add(weightLabel);
            panel2.Controls.Add(heightLabel);
            panel2.Controls.Add(teamNameLabel);
            panel2.Controls.Add(playerNameLabel);
            panel2.Location = new Point(3, 52);
            panel2.Name = "panel2";
            panel2.Size = new Size(950, 671);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // potentialLabel
            // 
            potentialLabel.AutoSize = true;
            potentialLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            potentialLabel.Location = new Point(35, 305);
            potentialLabel.Name = "potentialLabel";
            potentialLabel.Size = new Size(155, 25);
            potentialLabel.TabIndex = 14;
            potentialLabel.Text = "Potential: 77 (+2)";
            // 
            // overallLabel
            // 
            overallLabel.AutoSize = true;
            overallLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            overallLabel.Location = new Point(35, 248);
            overallLabel.Name = "overallLabel";
            overallLabel.Size = new Size(141, 25);
            overallLabel.TabIndex = 14;
            overallLabel.Text = "Overall: 75 (+2)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(380, 195);
            label5.Name = "label5";
            label5.Size = new Size(72, 21);
            label5.TabIndex = 13;
            label5.Text = "Defense";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(222, 195);
            label4.Name = "label4";
            label4.Size = new Size(69, 21);
            label4.TabIndex = 13;
            label4.Text = "Offense";
            // 
            // defenseAttributesLabel
            // 
            defenseAttributesLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            defenseAttributesLabel.Location = new Point(362, 225);
            defenseAttributesLabel.Name = "defenseAttributesLabel";
            defenseAttributesLabel.Size = new Size(119, 148);
            defenseAttributesLabel.TabIndex = 12;
            defenseAttributesLabel.Text = "Defense: xx\r\nSteal: xx\r\nBlock: xx\r\nRebound: xx\r\nSpeed: xx\r\nStrength: xx\r\nStamina: xx\r\n\r\n";
            // 
            // offenseAttributesLabel
            // 
            offenseAttributesLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            offenseAttributesLabel.Location = new Point(210, 225);
            offenseAttributesLabel.Name = "offenseAttributesLabel";
            offenseAttributesLabel.Size = new Size(119, 158);
            offenseAttributesLabel.TabIndex = 12;
            offenseAttributesLabel.Text = "Layup: xx\r\nDunk: xx\r\nMid Range: xx\r\nThree Point: xx\r\nFree Throw: xx\r\nPassing: xx\r\nBall Handle: xx\r\n\r\n";
            // 
            // awardsLabel
            // 
            awardsLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            awardsLabel.Location = new Point(674, 12);
            awardsLabel.Name = "awardsLabel";
            awardsLabel.Size = new Size(264, 384);
            awardsLabel.TabIndex = 11;
            awardsLabel.Text = "Awards:";
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
            seasonDataGridView.Columns.AddRange(new DataGridViewColumn[] { season, age, teamName, playerPosition, MP, gameValue, FGM, FGA, seasonFGPCT, TFGM, TFGA, seasonTFGPCT, FTM, FTA, FTPCT, seasonPTS, seasonREB, seasonAST, STL, BLK, TOV });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 7F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            seasonDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            seasonDataGridView.Location = new Point(35, 399);
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
            seasonDataGridView.Size = new Size(903, 249);
            seasonDataGridView.TabIndex = 10;
            // 
            // season
            // 
            season.DataPropertyName = "currentSeason";
            season.HeaderText = "Season";
            season.Name = "season";
            season.ReadOnly = true;
            season.Width = 60;
            // 
            // age
            // 
            age.DataPropertyName = "age";
            age.HeaderText = "Age";
            age.Name = "age";
            age.ReadOnly = true;
            age.Width = 40;
            // 
            // teamName
            // 
            teamName.DataPropertyName = "teamName";
            teamName.HeaderText = "Team Name";
            teamName.Name = "teamName";
            teamName.ReadOnly = true;
            teamName.Width = 115;
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
            // gameValue
            // 
            gameValue.DataPropertyName = "gameValue";
            gameValue.HeaderText = "Game Value";
            gameValue.Name = "gameValue";
            gameValue.ReadOnly = true;
            gameValue.Width = 80;
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
            // seasonFGPCT
            // 
            seasonFGPCT.DataPropertyName = "FG%";
            seasonFGPCT.HeaderText = "FG%";
            seasonFGPCT.Name = "seasonFGPCT";
            seasonFGPCT.ReadOnly = true;
            seasonFGPCT.Width = 40;
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
            // seasonTFGPCT
            // 
            seasonTFGPCT.DataPropertyName = "3P%";
            seasonTFGPCT.HeaderText = "3P%";
            seasonTFGPCT.Name = "seasonTFGPCT";
            seasonTFGPCT.ReadOnly = true;
            seasonTFGPCT.Width = 40;
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
            // FTPCT
            // 
            FTPCT.DataPropertyName = "FT%";
            FTPCT.HeaderText = "FT%";
            FTPCT.Name = "FTPCT";
            FTPCT.ReadOnly = true;
            FTPCT.Width = 40;
            // 
            // seasonPTS
            // 
            seasonPTS.DataPropertyName = "PTS";
            seasonPTS.HeaderText = "PTS";
            seasonPTS.Name = "seasonPTS";
            seasonPTS.ReadOnly = true;
            seasonPTS.Width = 30;
            // 
            // seasonREB
            // 
            seasonREB.DataPropertyName = "REB";
            seasonREB.HeaderText = "REB";
            seasonREB.Name = "seasonREB";
            seasonREB.ReadOnly = true;
            seasonREB.Width = 30;
            // 
            // seasonAST
            // 
            seasonAST.DataPropertyName = "AST";
            seasonAST.HeaderText = "AST";
            seasonAST.Name = "seasonAST";
            seasonAST.ReadOnly = true;
            seasonAST.Width = 30;
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
            // summaryGridView
            // 
            summaryGridView.AllowUserToAddRows = false;
            summaryGridView.AllowUserToDeleteRows = false;
            summaryGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            summaryGridView.Columns.AddRange(new DataGridViewColumn[] { gamesPlayed, PTS, REB, AST, FGPCT, TFGPCT });
            summaryGridView.Location = new Point(35, 103);
            summaryGridView.Name = "summaryGridView";
            summaryGridView.ReadOnly = true;
            summaryGridView.Size = new Size(385, 69);
            summaryGridView.TabIndex = 8;
            // 
            // gamesPlayed
            // 
            gamesPlayed.DataPropertyName = "gamesPlayed";
            gamesPlayed.HeaderText = "GP";
            gamesPlayed.Name = "gamesPlayed";
            gamesPlayed.ReadOnly = true;
            gamesPlayed.Width = 50;
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
            // FGPCT
            // 
            FGPCT.DataPropertyName = "FGPCT";
            FGPCT.HeaderText = "FG%";
            FGPCT.Name = "FGPCT";
            FGPCT.ReadOnly = true;
            FGPCT.Width = 60;
            // 
            // TFGPCT
            // 
            TFGPCT.DataPropertyName = "TFGPCT";
            TFGPCT.HeaderText = "3P%";
            TFGPCT.Name = "TFGPCT";
            TFGPCT.ReadOnly = true;
            TFGPCT.Width = 60;
            // 
            // ageLabel
            // 
            ageLabel.AutoSize = true;
            ageLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ageLabel.Location = new Point(487, 204);
            ageLabel.Name = "ageLabel";
            ageLabel.Size = new Size(59, 20);
            ageLabel.TabIndex = 7;
            ageLabel.Text = "Age: 25";
            // 
            // dateOfBirthLabel
            // 
            dateOfBirthLabel.AutoSize = true;
            dateOfBirthLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateOfBirthLabel.Location = new Point(487, 170);
            dateOfBirthLabel.Name = "dateOfBirthLabel";
            dateOfBirthLabel.Size = new Size(135, 20);
            dateOfBirthLabel.TabIndex = 6;
            dateOfBirthLabel.Text = "Date Of Birth: 1999";
            // 
            // playstyleLabel
            // 
            playstyleLabel.AutoSize = true;
            playstyleLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playstyleLabel.Location = new Point(487, 131);
            playstyleLabel.Name = "playstyleLabel";
            playstyleLabel.Size = new Size(123, 20);
            playstyleLabel.TabIndex = 5;
            playstyleLabel.Text = "Playstyle: Finisher";
            // 
            // positionLabel
            // 
            positionLabel.AutoSize = true;
            positionLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            positionLabel.Location = new Point(487, 93);
            positionLabel.Name = "positionLabel";
            positionLabel.Size = new Size(77, 20);
            positionLabel.TabIndex = 4;
            positionLabel.Text = "Position: C";
            // 
            // weightLabel
            // 
            weightLabel.AutoSize = true;
            weightLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            weightLabel.Location = new Point(487, 57);
            weightLabel.Name = "weightLabel";
            weightLabel.Size = new Size(110, 20);
            weightLabel.TabIndex = 3;
            weightLabel.Text = "Weight: 210 lbs";
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            heightLabel.Location = new Point(487, 21);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(80, 20);
            heightLabel.TabIndex = 2;
            heightLabel.Text = "Height: 6'5";
            // 
            // teamNameLabel
            // 
            teamNameLabel.AutoSize = true;
            teamNameLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamNameLabel.Location = new Point(50, 65);
            teamNameLabel.Name = "teamNameLabel";
            teamNameLabel.Size = new Size(213, 20);
            teamNameLabel.TabIndex = 1;
            teamNameLabel.Text = "Team Name: New York Bankers";
            // 
            // playerNameLabel
            // 
            playerNameLabel.AutoSize = true;
            playerNameLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerNameLabel.Location = new Point(50, 12);
            playerNameLabel.Name = "playerNameLabel";
            playerNameLabel.Size = new Size(179, 20);
            playerNameLabel.TabIndex = 0;
            playerNameLabel.Text = "Name: Dontae Thompson";
            // 
            // PlayerStatsUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "PlayerStatsUserControl";
            Size = new Size(959, 728);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)playerIdUpDown).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)seasonDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)summaryGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label titleLabel;
        private Panel panel2;
        private Label weightLabel;
        private Label heightLabel;
        private Label teamNameLabel;
        private Label playerNameLabel;
        private DataGridView summaryGridView;
        private Label ageLabel;
        private Label dateOfBirthLabel;
        private Label playstyleLabel;
        private Label positionLabel;
        private Label label1;
        public NumericUpDown playerIdUpDown;
        private DataGridViewTextBoxColumn gamesPlayed;
        private DataGridViewTextBoxColumn PTS;
        private DataGridViewTextBoxColumn REB;
        private DataGridViewTextBoxColumn AST;
        private DataGridViewTextBoxColumn FGPCT;
        private DataGridViewTextBoxColumn TFGPCT;
        private DataGridView seasonDataGridView;
        private Label awardsLabel;
        private DataGridViewTextBoxColumn season;
        private DataGridViewTextBoxColumn age;
        private DataGridViewTextBoxColumn teamName;
        private DataGridViewTextBoxColumn playerPosition;
        private DataGridViewTextBoxColumn MP;
        private DataGridViewTextBoxColumn gameValue;
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
        private Label offenseAttributesLabel;
        private Label defenseAttributesLabel;
        private Label potentialLabel;
        private Label overallLabel;
        private Label label5;
        private Label label4;
    }
}
