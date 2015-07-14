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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.applicationStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.cardReaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.scanForCard = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.displayRegion = new System.Windows.Forms.Panel();
            this.disconnectCardReader = new System.Windows.Forms.ToolStripMenuItem();
            this.setServerAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.connectCardReader = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectCardReader,
            this.disconnectCardReader,
            this.setServerAddress,
            this.exit});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1086, 24);
            this.menuStrip.TabIndex = 0;
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
            this.scanForCard.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.scanForCard_RunWorkerCompleted);
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
            // disconnectCardReader
            // 
            this.disconnectCardReader.Name = "disconnectCardReader";
            this.disconnectCardReader.Size = new System.Drawing.Size(140, 20);
            this.disconnectCardReader.Text = "Disconnect card reader";
            this.disconnectCardReader.Visible = false;
            this.disconnectCardReader.Click += new System.EventHandler(this.disconnectCardReader_Click);
            // 
            // setServerAddress
            // 
            this.setServerAddress.Name = "setServerAddress";
            this.setServerAddress.Size = new System.Drawing.Size(112, 20);
            this.setServerAddress.Text = "Set server address";
            this.setServerAddress.Click += new System.EventHandler(this.setAddress_Click);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(37, 20);
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // connectCardReader
            // 
            this.connectCardReader.Name = "connectCardReader";
            this.connectCardReader.Size = new System.Drawing.Size(126, 20);
            this.connectCardReader.Text = "Connect card reader";
            this.connectCardReader.Click += new System.EventHandler(this.connectCardReader_Click);
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
        private System.Windows.Forms.ToolStripStatusLabel applicationStatus;
        private System.Windows.Forms.ToolStripStatusLabel cardReaderStatus;
        private System.ComponentModel.BackgroundWorker scanForCard;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel displayRegion;
        private System.Windows.Forms.ToolStripMenuItem disconnectCardReader;
        private System.Windows.Forms.ToolStripMenuItem setServerAddress;
        private System.Windows.Forms.ToolStripMenuItem connectCardReader;
        private System.Windows.Forms.ToolStripMenuItem exit;
    }
}