namespace LeagueSimulation
{
    partial class MenuForm
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
            menuFormLayoutPanel = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // menuFormLayoutPanel
            // 
            menuFormLayoutPanel.AutoScroll = true;
            menuFormLayoutPanel.FlowDirection = FlowDirection.TopDown;
            menuFormLayoutPanel.Location = new Point(12, 12);
            menuFormLayoutPanel.Name = "menuFormLayoutPanel";
            menuFormLayoutPanel.Size = new Size(776, 426);
            menuFormLayoutPanel.TabIndex = 0;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuFormLayoutPanel);
            Name = "MenuForm";
            Text = "MenuForm";
            ResumeLayout(false);
        }

        #endregion

        public FlowLayoutPanel menuFormLayoutPanel;
    }
}