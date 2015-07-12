namespace signup_sheet_client
{
    partial class MainForm
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
            if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.serverStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cardReaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.cardStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopReadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userInfoPanel = new System.Windows.Forms.Panel();
            this.userId = new System.Windows.Forms.Label();
            this.invalidUserLabel = new System.Windows.Forms.Label();
            this.userLastName = new System.Windows.Forms.Label();
            this.userFirstName = new System.Windows.Forms.Label();
            this.cardReaderBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.userInfoTimer = new System.Windows.Forms.Timer(this.components);
            this.invalidUserTimer = new System.Windows.Forms.Timer(this.components);
            this.mainStatusStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.userInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.AllowMerge = false;
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverStatusLabel,
            this.cardReaderStatus,
            this.cardStatus});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 200);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(863, 22);
            this.mainStatusStrip.SizingGrip = false;
            this.mainStatusStrip.TabIndex = 0;
            // 
            // serverStatusLabel
            // 
            this.serverStatusLabel.AutoSize = false;
            this.serverStatusLabel.Name = "serverStatusLabel";
            this.serverStatusLabel.Size = new System.Drawing.Size(300, 17);
            this.serverStatusLabel.Text = "Disconnected";
            this.serverStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardReaderStatus
            // 
            this.cardReaderStatus.AutoSize = false;
            this.cardReaderStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cardReaderStatus.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.cardReaderStatus.Name = "cardReaderStatus";
            this.cardReaderStatus.Size = new System.Drawing.Size(100, 17);
            this.cardReaderStatus.Text = "Disconnected";
            this.cardReaderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardStatus
            // 
            this.cardStatus.AutoSize = false;
            this.cardStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cardStatus.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.cardStatus.Name = "cardStatus";
            this.cardStatus.Size = new System.Drawing.Size(100, 17);
            this.cardStatus.Text = "-";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(863, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "MainMenu";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cardReaderToolStripMenuItem,
            this.stopReadingToolStripMenuItem,
            this.serverToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // cardReaderToolStripMenuItem
            // 
            this.cardReaderToolStripMenuItem.Name = "cardReaderToolStripMenuItem";
            this.cardReaderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cardReaderToolStripMenuItem.Text = "Card Reader...";
            this.cardReaderToolStripMenuItem.Click += new System.EventHandler(this.cardReaderToolStripMenuItem_Click);
            this.cardReaderToolStripMenuItem.MouseHover += new System.EventHandler(this.cardReaderToolStripMenuItem_MouseHover);
            // 
            // stopReadingToolStripMenuItem
            // 
            this.stopReadingToolStripMenuItem.Name = "stopReadingToolStripMenuItem";
            this.stopReadingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopReadingToolStripMenuItem.Text = "Stop reading";
            this.stopReadingToolStripMenuItem.Visible = false;
            this.stopReadingToolStripMenuItem.Click += new System.EventHandler(this.stopReadingToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.serverToolStripMenuItem.Text = "Server...";
            this.serverToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // userInfoPanel
            // 
            this.userInfoPanel.Controls.Add(this.userId);
            this.userInfoPanel.Controls.Add(this.invalidUserLabel);
            this.userInfoPanel.Controls.Add(this.userLastName);
            this.userInfoPanel.Controls.Add(this.userFirstName);
            this.userInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userInfoPanel.Location = new System.Drawing.Point(0, 24);
            this.userInfoPanel.Name = "userInfoPanel";
            this.userInfoPanel.Size = new System.Drawing.Size(863, 176);
            this.userInfoPanel.TabIndex = 2;
            // 
            // userId
            // 
            this.userId.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userId.Location = new System.Drawing.Point(20, 49);
            this.userId.Name = "userId";
            this.userId.Size = new System.Drawing.Size(225, 65);
            this.userId.TabIndex = 5;
            this.userId.Text = "ID";
            this.userId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // invalidUserLabel
            // 
            this.invalidUserLabel.AutoSize = true;
            this.invalidUserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invalidUserLabel.Location = new System.Drawing.Point(358, 66);
            this.invalidUserLabel.Name = "invalidUserLabel";
            this.invalidUserLabel.Size = new System.Drawing.Size(117, 39);
            this.invalidUserLabel.TabIndex = 4;
            this.invalidUserLabel.Text = "Invalid";
            // 
            // userLastName
            // 
            this.userLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.userLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLastName.Location = new System.Drawing.Point(251, 92);
            this.userLastName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 30);
            this.userLastName.Name = "userLastName";
            this.userLastName.Size = new System.Drawing.Size(600, 80);
            this.userLastName.TabIndex = 3;
            this.userLastName.Text = "LASTNAME";
            this.userLastName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userFirstName
            // 
            this.userFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userFirstName.Location = new System.Drawing.Point(251, 4);
            this.userFirstName.Name = "userFirstName";
            this.userFirstName.Size = new System.Drawing.Size(600, 80);
            this.userFirstName.TabIndex = 2;
            this.userFirstName.Text = "abcdefg";
            this.userFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardReaderBackgroundWorker
            // 
            this.cardReaderBackgroundWorker.WorkerReportsProgress = true;
            this.cardReaderBackgroundWorker.WorkerSupportsCancellation = true;
            this.cardReaderBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.cardReaderBackgroundWorker_DoWork);
            this.cardReaderBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.cardReaderBackgroundWorker_ProgressChanged);
            // 
            // userInfoTimer
            // 
            this.userInfoTimer.Tick += new System.EventHandler(this.userInfoTimer_Tick);
            // 
            // invalidUserTimer
            // 
            this.invalidUserTimer.Tick += new System.EventHandler(this.invalidUserTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 222);
            this.Controls.Add(this.userInfoPanel);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Signup Sheet - Client";
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.userInfoPanel.ResumeLayout(false);
            this.userInfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cardReaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel serverStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel cardReaderStatus;
        private System.Windows.Forms.ToolStripStatusLabel cardStatus;
        private System.Windows.Forms.Panel userInfoPanel;
        private System.ComponentModel.BackgroundWorker cardReaderBackgroundWorker;
        private System.Windows.Forms.Label userLastName;
        private System.Windows.Forms.Label userFirstName;
        private System.Windows.Forms.Label invalidUserLabel;
        private System.Windows.Forms.Timer userInfoTimer;
        private System.Windows.Forms.Timer invalidUserTimer;
        private System.Windows.Forms.ToolStripMenuItem stopReadingToolStripMenuItem;
        private System.Windows.Forms.Label userId;
    }
}

