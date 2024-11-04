namespace LeagueSimulation
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WelcomeLabel = new Label();
            teamNameTextBox = new TextBox();
            saveStateLabel = new Label();
            loadGameButton = new Button();
            createGameButton = new Button();
            saveStateNum = new NumericUpDown();
            saveStateNumberLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)saveStateNum).BeginInit();
            SuspendLayout();
            // 
            // WelcomeLabel
            // 
            WelcomeLabel.Anchor = AnchorStyles.Top;
            WelcomeLabel.AutoSize = true;
            WelcomeLabel.Font = new Font("Segoe UI", 20F);
            WelcomeLabel.Location = new Point(117, 37);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(551, 37);
            WelcomeLabel.TabIndex = 0;
            WelcomeLabel.Text = "Welcome to my Basketball League Simulator!";
            // 
            // teamNameTextBox
            // 
            teamNameTextBox.Location = new Point(74, 168);
            teamNameTextBox.Name = "teamNameTextBox";
            teamNameTextBox.Size = new Size(409, 23);
            teamNameTextBox.TabIndex = 2;
            // 
            // saveStateLabel
            // 
            saveStateLabel.AutoSize = true;
            saveStateLabel.Font = new Font("Segoe UI", 11F);
            saveStateLabel.Location = new Point(74, 132);
            saveStateLabel.Name = "saveStateLabel";
            saveStateLabel.Size = new Size(409, 20);
            saveStateLabel.TabIndex = 3;
            saveStateLabel.Text = "Enter the team name for the team you would like to play for:";
            // 
            // loadGameButton
            // 
            loadGameButton.Font = new Font("Segoe UI", 20F);
            loadGameButton.Location = new Point(191, 229);
            loadGameButton.Name = "loadGameButton";
            loadGameButton.Size = new Size(180, 75);
            loadGameButton.TabIndex = 4;
            loadGameButton.Text = "Load Game";
            loadGameButton.UseVisualStyleBackColor = true;
            loadGameButton.Click += loadGameButton_Click;
            // 
            // createGameButton
            // 
            createGameButton.Font = new Font("Segoe UI", 20F);
            createGameButton.Location = new Point(410, 229);
            createGameButton.Name = "createGameButton";
            createGameButton.Size = new Size(186, 75);
            createGameButton.TabIndex = 5;
            createGameButton.Text = "Create Game";
            createGameButton.UseVisualStyleBackColor = true;
            createGameButton.Click += createGameButton_Click;
            // 
            // saveStateNum
            // 
            saveStateNum.Location = new Point(589, 169);
            saveStateNum.Name = "saveStateNum";
            saveStateNum.Size = new Size(120, 23);
            saveStateNum.TabIndex = 6;
            // 
            // saveStateNumberLabel
            // 
            saveStateNumberLabel.AutoSize = true;
            saveStateNumberLabel.Font = new Font("Segoe UI", 11F);
            saveStateNumberLabel.Location = new Point(589, 132);
            saveStateNumberLabel.Name = "saveStateNumberLabel";
            saveStateNumberLabel.Size = new Size(139, 20);
            saveStateNumberLabel.TabIndex = 7;
            saveStateNumberLabel.Text = "Save State Number:";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(saveStateNumberLabel);
            Controls.Add(saveStateNum);
            Controls.Add(createGameButton);
            Controls.Add(loadGameButton);
            Controls.Add(saveStateLabel);
            Controls.Add(teamNameTextBox);
            Controls.Add(WelcomeLabel);
            Name = "Main";
            Text = "Start Screen";
            ((System.ComponentModel.ISupportInitialize)saveStateNum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label WelcomeLabel;
        private TextBox teamNameTextBox;
        private Label saveStateLabel;
        private Button loadGameButton;
        private Button createGameButton;
        private NumericUpDown saveStateNum;
        private Label saveStateNumberLabel;
    }
}
