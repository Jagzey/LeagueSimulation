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
            panel1 = new Panel();
            label1 = new Label();
            playerIdUpDown = new NumericUpDown();
            titleLabel = new Label();
            panel2 = new Panel();
            attributesGridView = new DataGridView();
            seasonAttributes = new DataGridViewTextBoxColumn();
            ageAttributes = new DataGridViewTextBoxColumn();
            overallAttributes = new DataGridViewTextBoxColumn();
            potentialAttributes = new DataGridViewTextBoxColumn();
            layup = new DataGridViewTextBoxColumn();
            dunk = new DataGridViewTextBoxColumn();
            midRange = new DataGridViewTextBoxColumn();
            threePoint = new DataGridViewTextBoxColumn();
            freeThrow = new DataGridViewTextBoxColumn();
            Passing = new DataGridViewTextBoxColumn();
            ballHandle = new DataGridViewTextBoxColumn();
            defense = new DataGridViewTextBoxColumn();
            steal = new DataGridViewTextBoxColumn();
            block = new DataGridViewTextBoxColumn();
            rebound = new DataGridViewTextBoxColumn();
            speed = new DataGridViewTextBoxColumn();
            stamina = new DataGridViewTextBoxColumn();
            summaryGridView = new DataGridView();
            ageLabel = new Label();
            dateOfBirthLabel = new Label();
            playstyleLabel = new Label();
            positionLabel = new Label();
            weightLabel = new Label();
            heightLabel = new Label();
            teamNameLabel = new Label();
            playerNameLabel = new Label();
            gamesPlayed = new DataGridViewTextBoxColumn();
            PTS = new DataGridViewTextBoxColumn();
            REB = new DataGridViewTextBoxColumn();
            AST = new DataGridViewTextBoxColumn();
            FGPCT = new DataGridViewTextBoxColumn();
            TFGPCT = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)playerIdUpDown).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attributesGridView).BeginInit();
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
            panel1.Size = new Size(614, 43);
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
            titleLabel.Location = new Point(276, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(101, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Player Stats Menu";
            // 
            // panel2
            // 
            panel2.Controls.Add(attributesGridView);
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
            panel2.Size = new Size(614, 1445);
            panel2.TabIndex = 1;
            // 
            // attributesGridView
            // 
            attributesGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attributesGridView.Columns.AddRange(new DataGridViewColumn[] { seasonAttributes, ageAttributes, overallAttributes, potentialAttributes, layup, dunk, midRange, threePoint, freeThrow, Passing, ballHandle, defense, steal, block, rebound, speed, stamina });
            attributesGridView.Location = new Point(21, 319);
            attributesGridView.Name = "attributesGridView";
            attributesGridView.Size = new Size(567, 137);
            attributesGridView.TabIndex = 9;
            // 
            // seasonAttributes
            // 
            seasonAttributes.HeaderText = "Season";
            seasonAttributes.Name = "seasonAttributes";
            seasonAttributes.Width = 70;
            // 
            // ageAttributes
            // 
            ageAttributes.HeaderText = "Age";
            ageAttributes.Name = "ageAttributes";
            ageAttributes.Width = 50;
            // 
            // overallAttributes
            // 
            overallAttributes.HeaderText = "Overall";
            overallAttributes.Name = "overallAttributes";
            overallAttributes.Width = 70;
            // 
            // potentialAttributes
            // 
            potentialAttributes.HeaderText = "Potential";
            potentialAttributes.Name = "potentialAttributes";
            potentialAttributes.Width = 75;
            // 
            // layup
            // 
            layup.HeaderText = "Layup";
            layup.Name = "layup";
            layup.Width = 70;
            // 
            // dunk
            // 
            dunk.HeaderText = "Dunk";
            dunk.Name = "dunk";
            dunk.Width = 70;
            // 
            // midRange
            // 
            midRange.HeaderText = "Mid Range";
            midRange.Name = "midRange";
            // 
            // threePoint
            // 
            threePoint.HeaderText = "Three Point";
            threePoint.Name = "threePoint";
            // 
            // freeThrow
            // 
            freeThrow.HeaderText = "Free Throw";
            freeThrow.Name = "freeThrow";
            // 
            // Passing
            // 
            Passing.HeaderText = "Passing";
            Passing.Name = "Passing";
            Passing.Width = 85;
            // 
            // ballHandle
            // 
            ballHandle.HeaderText = "Ball Handle";
            ballHandle.Name = "ballHandle";
            // 
            // defense
            // 
            defense.HeaderText = "Defense";
            defense.Name = "defense";
            // 
            // steal
            // 
            steal.HeaderText = "Steal";
            steal.Name = "steal";
            steal.Width = 80;
            // 
            // block
            // 
            block.HeaderText = "Block";
            block.Name = "block";
            block.Width = 80;
            // 
            // rebound
            // 
            rebound.HeaderText = "Rebound";
            rebound.Name = "rebound";
            // 
            // speed
            // 
            speed.HeaderText = "Speed";
            speed.Name = "speed";
            speed.Width = 85;
            // 
            // stamina
            // 
            stamina.HeaderText = "Stamina";
            stamina.Name = "stamina";
            stamina.Width = 85;
            // 
            // summaryGridView
            // 
            summaryGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            summaryGridView.Columns.AddRange(new DataGridViewColumn[] { gamesPlayed, PTS, REB, AST, FGPCT, TFGPCT });
            summaryGridView.Location = new Point(35, 133);
            summaryGridView.Name = "summaryGridView";
            summaryGridView.Size = new Size(394, 137);
            summaryGridView.TabIndex = 8;
            // 
            // ageLabel
            // 
            ageLabel.AutoSize = true;
            ageLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ageLabel.Location = new Point(447, 252);
            ageLabel.Name = "ageLabel";
            ageLabel.Size = new Size(59, 20);
            ageLabel.TabIndex = 7;
            ageLabel.Text = "Age: 25";
            // 
            // dateOfBirthLabel
            // 
            dateOfBirthLabel.AutoSize = true;
            dateOfBirthLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateOfBirthLabel.Location = new Point(447, 217);
            dateOfBirthLabel.Name = "dateOfBirthLabel";
            dateOfBirthLabel.Size = new Size(135, 20);
            dateOfBirthLabel.TabIndex = 6;
            dateOfBirthLabel.Text = "Date Of Birth: 1999";
            // 
            // playstyleLabel
            // 
            playstyleLabel.AutoSize = true;
            playstyleLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playstyleLabel.Location = new Point(447, 179);
            playstyleLabel.Name = "playstyleLabel";
            playstyleLabel.Size = new Size(123, 20);
            playstyleLabel.TabIndex = 5;
            playstyleLabel.Text = "Playstyle: Finisher";
            // 
            // positionLabel
            // 
            positionLabel.AutoSize = true;
            positionLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            positionLabel.Location = new Point(448, 133);
            positionLabel.Name = "positionLabel";
            positionLabel.Size = new Size(77, 20);
            positionLabel.TabIndex = 4;
            positionLabel.Text = "Position: C";
            // 
            // weightLabel
            // 
            weightLabel.AutoSize = true;
            weightLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            weightLabel.Location = new Point(448, 85);
            weightLabel.Name = "weightLabel";
            weightLabel.Size = new Size(110, 20);
            weightLabel.TabIndex = 3;
            weightLabel.Text = "Weight: 210 lbs";
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            heightLabel.Location = new Point(448, 36);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(80, 20);
            heightLabel.TabIndex = 2;
            heightLabel.Text = "Height: 6'5";
            // 
            // teamNameLabel
            // 
            teamNameLabel.AutoSize = true;
            teamNameLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamNameLabel.Location = new Point(50, 85);
            teamNameLabel.Name = "teamNameLabel";
            teamNameLabel.Size = new Size(213, 20);
            teamNameLabel.TabIndex = 1;
            teamNameLabel.Text = "Team Name: New York Bankers";
            // 
            // playerNameLabel
            // 
            playerNameLabel.AutoSize = true;
            playerNameLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerNameLabel.Location = new Point(50, 36);
            playerNameLabel.Name = "playerNameLabel";
            playerNameLabel.Size = new Size(179, 20);
            playerNameLabel.TabIndex = 0;
            playerNameLabel.Text = "Name: Dontae Thompson";
            // 
            // gamesPlayed
            // 
            gamesPlayed.DataPropertyName = "gamesPlayed";
            gamesPlayed.HeaderText = "GP";
            gamesPlayed.Name = "gamesPlayed";
            gamesPlayed.Width = 50;
            // 
            // PTS
            // 
            PTS.DataPropertyName = "PTS";
            PTS.HeaderText = "PTS";
            PTS.Name = "PTS";
            PTS.Width = 60;
            // 
            // REB
            // 
            REB.DataPropertyName = "REB";
            REB.HeaderText = "REB";
            REB.Name = "REB";
            REB.Width = 60;
            // 
            // AST
            // 
            AST.DataPropertyName = "AST";
            AST.HeaderText = "AST";
            AST.Name = "AST";
            AST.Width = 60;
            // 
            // FGPCT
            // 
            FGPCT.DataPropertyName = "FGPCT";
            FGPCT.HeaderText = "FG%";
            FGPCT.Name = "FGPCT";
            FGPCT.Width = 60;
            // 
            // TFGPCT
            // 
            TFGPCT.DataPropertyName = "TFGPCT";
            TFGPCT.HeaderText = "3P%";
            TFGPCT.Name = "TFGPCT";
            TFGPCT.Width = 60;
            // 
            // PlayerStatsUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "PlayerStatsUserControl";
            Size = new Size(620, 1500);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)playerIdUpDown).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)attributesGridView).EndInit();
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
        private DataGridView attributesGridView;
        private DataGridViewTextBoxColumn seasonAttributes;
        private DataGridViewTextBoxColumn ageAttributes;
        private DataGridViewTextBoxColumn overallAttributes;
        private DataGridViewTextBoxColumn potentialAttributes;
        private DataGridViewTextBoxColumn layup;
        private DataGridViewTextBoxColumn dunk;
        private DataGridViewTextBoxColumn midRange;
        private DataGridViewTextBoxColumn threePoint;
        private DataGridViewTextBoxColumn freeThrow;
        private DataGridViewTextBoxColumn Passing;
        private DataGridViewTextBoxColumn ballHandle;
        private DataGridViewTextBoxColumn defense;
        private DataGridViewTextBoxColumn steal;
        private DataGridViewTextBoxColumn block;
        private DataGridViewTextBoxColumn rebound;
        private DataGridViewTextBoxColumn speed;
        private DataGridViewTextBoxColumn stamina;
        private Label label1;
        public NumericUpDown playerIdUpDown;
        private DataGridViewTextBoxColumn gamesPlayed;
        private DataGridViewTextBoxColumn PTS;
        private DataGridViewTextBoxColumn REB;
        private DataGridViewTextBoxColumn AST;
        private DataGridViewTextBoxColumn FGPCT;
        private DataGridViewTextBoxColumn TFGPCT;
    }
}
