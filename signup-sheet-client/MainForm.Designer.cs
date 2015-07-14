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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectCardReader = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectCardReader = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.applicationStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.cardReaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.scanForCard = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.displayRegion = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1086, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectCardReader,
            this.disconnectCardReader,
            this.toolStripSeparator1,
            this.setAddress,
            this.toolStripSeparator2,
            this.exit});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // connectCardReader
            // 
            this.connectCardReader.Name = "connectCardReader";
            this.connectCardReader.Size = new System.Drawing.Size(195, 22);
            this.connectCardReader.Text = "Connect card reader";
            this.connectCardReader.Click += new System.EventHandler(this.connectCardReader_Click);
            // 
            // disconnectCardReader
            // 
            this.disconnectCardReader.Name = "disconnectCardReader";
            this.disconnectCardReader.Size = new System.Drawing.Size(195, 22);
            this.disconnectCardReader.Text = "Disconnect card reader";
            this.disconnectCardReader.Visible = false;
            this.disconnectCardReader.Click += new System.EventHandler(this.disconnectCardReader_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // setAddress
            // 
            this.setAddress.Name = "setAddress";
            this.setAddress.Size = new System.Drawing.Size(195, 22);
            this.setAddress.Text = "Set server address";
            this.setAddress.Click += new System.EventHandler(this.setAddress_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(195, 22);
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationStatus,
            this.cardReaderStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 239);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1086, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // applicationStatus
            // 
            this.applicationStatus.AutoSize = false;
            this.applicationStatus.Name = "applicationStatus";
            this.applicationStatus.Size = new System.Drawing.Size(400, 17);
            this.applicationStatus.Text = "Unknown.";
            this.applicationStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardReaderStatus
            // 
            this.cardReaderStatus.AutoSize = false;
            this.cardReaderStatus.Name = "cardReaderStatus";
            this.cardReaderStatus.Size = new System.Drawing.Size(671, 17);
            this.cardReaderStatus.Spring = true;
            this.cardReaderStatus.Text = "-";
            this.cardReaderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scanForCard
            // 
            this.scanForCard.WorkerReportsProgress = true;
            this.scanForCard.WorkerSupportsCancellation = true;
            this.scanForCard.DoWork += new System.ComponentModel.DoWorkEventHandler(this.scanForCard_DoWork);
            this.scanForCard.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.scanForCard_ProgressChanged);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // displayRegion
            // 
            this.displayRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayRegion.Location = new System.Drawing.Point(0, 24);
            this.displayRegion.Name = "displayRegion";
            this.displayRegion.Size = new System.Drawing.Size(1086, 215);
            this.displayRegion.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 261);
            this.Controls.Add(this.displayRegion);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectCardReader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem setAddress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.ToolStripStatusLabel applicationStatus;
        private System.Windows.Forms.ToolStripStatusLabel cardReaderStatus;
        private System.Windows.Forms.ToolStripMenuItem disconnectCardReader;
        private System.ComponentModel.BackgroundWorker scanForCard;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel displayRegion;
    }
}