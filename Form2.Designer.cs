namespace LeagueSimulation
{
    partial class Form2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            saveStateLabel = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            backToStartMenuToolStripMenuItem = new ToolStripMenuItem();
            quitGameMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            menuStrip2 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            dashboardMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            leagueStandingsMenuItem = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            rosterMenuItem = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            fullScheduleMenuItem = new ToolStripMenuItem();
            toolStripMenuItem16 = new ToolStripMenuItem();
            toolStripMenuItem17 = new ToolStripMenuItem();
            playerStatsMenuItem = new ToolStripMenuItem();
            toolStripMenuItem19 = new ToolStripMenuItem();
            leagueLeadersToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem20 = new ToolStripMenuItem();
            displayPanel = new FlowLayoutPanel();
            label1 = new Label();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // saveStateLabel
            // 
            saveStateLabel.AutoSize = true;
            saveStateLabel.Location = new Point(625, 9);
            saveStateLabel.Name = "saveStateLabel";
            saveStateLabel.Size = new Size(114, 15);
            saveStateLabel.TabIndex = 0;
            saveStateLabel.Text = "League Save State: n";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backToStartMenuToolStripMenuItem, quitGameMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // backToStartMenuToolStripMenuItem
            // 
            backToStartMenuToolStripMenuItem.Name = "backToStartMenuToolStripMenuItem";
            backToStartMenuToolStripMenuItem.Size = new Size(174, 22);
            backToStartMenuToolStripMenuItem.Text = "Back to Start Menu";
            // 
            // quitGameMenuItem
            // 
            quitGameMenuItem.Name = "quitGameMenuItem";
            quitGameMenuItem.Size = new Size(174, 22);
            quitGameMenuItem.Text = "Quit Game";
            quitGameMenuItem.Click += quitGameToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(menuStrip2);
            panel1.Location = new Point(0, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(138, 411);
            panel1.TabIndex = 2;
            // 
            // menuStrip2
            // 
            menuStrip2.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem20 });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(138, 31);
            menuStrip2.TabIndex = 1;
            menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { dashboardMenuItem, toolStripMenuItem3, toolStripMenuItem8, fullScheduleMenuItem, toolStripMenuItem16, toolStripMenuItem17 });
            toolStripMenuItem1.Font = new Font("Segoe UI", 12.4F);
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(126, 27);
            toolStripMenuItem1.Text = "League Menu";
            // 
            // dashboardMenuItem
            // 
            dashboardMenuItem.Name = "dashboardMenuItem";
            dashboardMenuItem.Size = new Size(180, 28);
            dashboardMenuItem.Text = "Dashboard";
            dashboardMenuItem.Click += dashboardMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.DropDownItems.AddRange(new ToolStripItem[] { leagueStandingsMenuItem, toolStripMenuItem5 });
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(180, 28);
            toolStripMenuItem3.Text = "League";
            // 
            // leagueStandingsMenuItem
            // 
            leagueStandingsMenuItem.Name = "leagueStandingsMenuItem";
            leagueStandingsMenuItem.Size = new Size(154, 28);
            leagueStandingsMenuItem.Text = "Standings";
            leagueStandingsMenuItem.Click += leagueStandingsMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(154, 28);
            toolStripMenuItem5.Text = "Playoffs";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.DropDownItems.AddRange(new ToolStripItem[] { rosterMenuItem, toolStripMenuItem10 });
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(180, 28);
            toolStripMenuItem8.Text = "Team";
            // 
            // rosterMenuItem
            // 
            rosterMenuItem.Name = "rosterMenuItem";
            rosterMenuItem.Size = new Size(149, 28);
            rosterMenuItem.Text = "Roster";
            rosterMenuItem.Click += rosterMenuItem_Click;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(149, 28);
            toolStripMenuItem10.Text = "Schedule";
            // 
            // fullScheduleMenuItem
            // 
            fullScheduleMenuItem.Name = "fullScheduleMenuItem";
            fullScheduleMenuItem.Size = new Size(180, 28);
            fullScheduleMenuItem.Text = "Full Schedule";
            fullScheduleMenuItem.Click += fullScheduleMenuItem_Click;
            // 
            // toolStripMenuItem16
            // 
            toolStripMenuItem16.Name = "toolStripMenuItem16";
            toolStripMenuItem16.Size = new Size(180, 28);
            toolStripMenuItem16.Text = "Award Races";
            // 
            // toolStripMenuItem17
            // 
            toolStripMenuItem17.DropDownItems.AddRange(new ToolStripItem[] { playerStatsMenuItem, toolStripMenuItem19, leagueLeadersToolStripMenuItem });
            toolStripMenuItem17.Name = "toolStripMenuItem17";
            toolStripMenuItem17.Size = new Size(180, 28);
            toolStripMenuItem17.Text = "Stats";
            // 
            // playerStatsMenuItem
            // 
            playerStatsMenuItem.Name = "playerStatsMenuItem";
            playerStatsMenuItem.Size = new Size(198, 28);
            playerStatsMenuItem.Text = "Player Stats";
            playerStatsMenuItem.Click += playerStatsMenuItem_Click;
            // 
            // toolStripMenuItem19
            // 
            toolStripMenuItem19.Name = "toolStripMenuItem19";
            toolStripMenuItem19.Size = new Size(198, 28);
            toolStripMenuItem19.Text = "Team Stats";
            // 
            // leagueLeadersToolStripMenuItem
            // 
            leagueLeadersToolStripMenuItem.Name = "leagueLeadersToolStripMenuItem";
            leagueLeadersToolStripMenuItem.Size = new Size(198, 28);
            leagueLeadersToolStripMenuItem.Text = "League Leaders";
            leagueLeadersToolStripMenuItem.Click += leagueLeadersToolStripMenuItem_Click;
            // 
            // toolStripMenuItem20
            // 
            toolStripMenuItem20.Name = "toolStripMenuItem20";
            toolStripMenuItem20.Size = new Size(12, 27);
            // 
            // displayPanel
            // 
            displayPanel.AutoScroll = true;
            displayPanel.FlowDirection = FlowDirection.TopDown;
            displayPanel.Location = new Point(141, 27);
            displayPanel.Name = "displayPanel";
            displayPanel.Size = new Size(647, 411);
            displayPanel.TabIndex = 3;
            displayPanel.WrapContents = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(144, 9);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 4;
            label1.Text = "User Team: ";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(displayPanel);
            Controls.Add(panel1);
            Controls.Add(saveStateLabel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form2";
            Text = "Basketball League Simulator";
            FormClosing += Form2_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label saveStateLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitGameMenuItem;
        private Panel panel1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem dashboardMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem leagueStandingsMenuItem;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem rosterMenuItem;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem fullScheduleMenuItem;
        private ToolStripMenuItem toolStripMenuItem16;
        private ToolStripMenuItem toolStripMenuItem17;
        private ToolStripMenuItem playerStatsMenuItem;
        private ToolStripMenuItem toolStripMenuItem19;
        private ToolStripMenuItem toolStripMenuItem20;
        private ToolStripMenuItem backToStartMenuToolStripMenuItem;
        private FlowLayoutPanel displayPanel;
        private ToolStripMenuItem leagueLeadersToolStripMenuItem;
        private Label label1;
    }
}