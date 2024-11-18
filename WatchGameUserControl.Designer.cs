namespace LeagueSimulation
{
    partial class WatchGameUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatchGameUserControl));
            label1 = new Label();
            playbackSpeed = new NumericUpDown();
            label2 = new Label();
            game1WatchGameButton = new Button();
            simToEndButton = new Button();
            teamsPlayingLabel = new Label();
            seasonDayLabel = new Label();
            scoreLabel = new Label();
            panel2 = new Panel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label3 = new Label();
            commentatorPhrasesLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)playbackSpeed).BeginInit();
            panel2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(523, 0);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 0;
            label1.Text = "Watch Game Menu";
            // 
            // playbackSpeed
            // 
            playbackSpeed.DecimalPlaces = 1;
            playbackSpeed.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            playbackSpeed.Location = new Point(32, 18);
            playbackSpeed.Maximum = new decimal(new int[] { 10001, 0, 0, 0 });
            playbackSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 262144 });
            playbackSpeed.Name = "playbackSpeed";
            playbackSpeed.Size = new Size(120, 23);
            playbackSpeed.TabIndex = 1;
            playbackSpeed.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(191, 15);
            label2.TabIndex = 2;
            label2.Text = "Game Playback Speed (1.6 = 160%)";
            // 
            // game1WatchGameButton
            // 
            game1WatchGameButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            game1WatchGameButton.Location = new Point(14, 59);
            game1WatchGameButton.Name = "game1WatchGameButton";
            game1WatchGameButton.Size = new Size(170, 33);
            game1WatchGameButton.TabIndex = 4;
            game1WatchGameButton.Text = "Pause Simulation";
            game1WatchGameButton.UseVisualStyleBackColor = true;
            game1WatchGameButton.Click += game1WatchGameButton_Click;
            // 
            // simToEndButton
            // 
            simToEndButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            simToEndButton.Location = new Point(190, 59);
            simToEndButton.Name = "simToEndButton";
            simToEndButton.Size = new Size(170, 33);
            simToEndButton.TabIndex = 5;
            simToEndButton.Text = "Simulate To End";
            simToEndButton.UseVisualStyleBackColor = true;
            simToEndButton.Click += simToEndButton_Click;
            // 
            // teamsPlayingLabel
            // 
            teamsPlayingLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            teamsPlayingLabel.Location = new Point(247, 35);
            teamsPlayingLabel.Name = "teamsPlayingLabel";
            teamsPlayingLabel.Size = new Size(385, 21);
            teamsPlayingLabel.TabIndex = 6;
            teamsPlayingLabel.Text = "New York Bankers vs. Philadelphia Hawks";
            teamsPlayingLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // seasonDayLabel
            // 
            seasonDayLabel.AutoSize = true;
            seasonDayLabel.Location = new Point(225, 0);
            seasonDayLabel.Name = "seasonDayLabel";
            seasonDayLabel.Size = new Size(73, 15);
            seasonDayLabel.TabIndex = 7;
            seasonDayLabel.Text = "Season Day: ";
            // 
            // scoreLabel
            // 
            scoreLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            scoreLabel.Location = new Point(375, 66);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(108, 21);
            scoreLabel.TabIndex = 8;
            scoreLabel.Text = "100-86";
            scoreLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(scoreLabel);
            panel2.Controls.Add(seasonDayLabel);
            panel2.Controls.Add(teamsPlayingLabel);
            panel2.Controls.Add(simToEndButton);
            panel2.Controls.Add(game1WatchGameButton);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(playbackSpeed);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(635, 107);
            panel2.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoScroll = true;
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(label3);
            flowLayoutPanel2.Controls.Add(commentatorPhrasesLabel);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(17, 116);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(599, 1563);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(593, 66);
            label3.TabIndex = 0;
            label3.Text = "Play-by-Play Description";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // commentatorPhrasesLabel
            // 
            commentatorPhrasesLabel.AutoSize = true;
            commentatorPhrasesLabel.Location = new Point(3, 66);
            commentatorPhrasesLabel.MaximumSize = new Size(610, 0);
            commentatorPhrasesLabel.Name = "commentatorPhrasesLabel";
            commentatorPhrasesLabel.Size = new Size(593, 45);
            commentatorPhrasesLabel.TabIndex = 1;
            commentatorPhrasesLabel.Text = resources.GetString("commentatorPhrasesLabel.Text");
            // 
            // WatchGameUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowLayoutPanel2);
            Controls.Add(panel2);
            Name = "WatchGameUserControl";
            Size = new Size(641, 4000);
            ((System.ComponentModel.ISupportInitialize)playbackSpeed).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        public NumericUpDown playbackSpeed;
        private Label label2;
        private Button game1WatchGameButton;
        private Button simToEndButton;
        private Label teamsPlayingLabel;
        private Label seasonDayLabel;
        public Label scoreLabel;
        public Panel panel2;
        public FlowLayoutPanel flowLayoutPanel2;
        private Label label3;
        public Label commentatorPhrasesLabel;
    }
}
