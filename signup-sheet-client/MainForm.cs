using signup_sheet_client.Network;
using signup_sheet_client.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace signup_sheet_client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Setup the forms.
            this.displayMessage.AutoSize = false;
            this.displayMessage.Dock = DockStyle.Fill;
            this.displayMessage.Show();

            this.displayUserInfo.AutoSize = false;
            this.displayUserInfo.Dock = DockStyle.Fill;
            this.displayUserInfo.Show();

            // Setup timer.
            this.timer.Interval = displayInterval;

            // Set the status bar.
            this.cardReaderStatus.Text = "Disconnected.";
            this.applicationStatus.Text = string.Empty;
        }

        #region Card reader related functions.

        private ReaderWrapper cardReader = new ReaderWrapper();
        // USB = 100, hard-coded.
        private const short defaultCardReaderPort = 100;
        private bool cardReaderConnected = false;

        private void connectCardReader_Click(object sender, EventArgs e)
        {
            // Connect.
            this.cardReaderConnected = this.cardReader.Open(defaultCardReaderPort);

            // Set the status bar.
            if(this.cardReaderConnected)
            {
                this.cardReaderStatus.Text = "Connected.";

                // Change menu state if connected.
                this.disconnectCardReader.Visible = this.cardReaderConnected;
                this.connectCardReader.Visible = !this.disconnectCardReader.Visible;

                this.setServerAddress.Enabled = false;

                // Start the background worker.
                this.scanForCard.RunWorkerAsync();
            }
            else
            {
                this.cardReaderStatus.Text = "Failed to connect.";
            }
        }

        private void disconnectCardReader_Click(object sender, EventArgs e)
        {
            // Stop the background worker.
            this.scanForCard.CancelAsync();

            // Disconnect.
            this.cardReaderConnected = !this.cardReader.Close();

            // Check if successfully closed.
            if(!this.cardReaderConnected)
            {
                // Change menu state if connected.
                this.disconnectCardReader.Visible = this.cardReaderConnected;
                this.connectCardReader.Visible = !this.disconnectCardReader.Visible;

                this.setServerAddress.Enabled = true;

                this.cardReaderStatus.Text = "Disconnected.";
            }
            else
            {
                this.cardReaderStatus.Text = "Fail to disconnect.";
            }
        }

        #endregion

        private string serverAddress = "localhost:12000"; //string.Empty;

        private void setAddress_Click(object sender, EventArgs e)
        {
            using(AskForServer dialog = new AskForServer(this.serverAddress))
            {
                DialogResult status = dialog.ShowDialog();
                if(status == DialogResult.OK)
                {
                    // Update current server address.
                    this.serverAddress = dialog.Address;
                }
            }
        }

        #region Program stop.

        private void exit_Click(object sender, EventArgs e)
        {
            // Stop the background worker.
            if(this.scanForCard.IsBusy)
            {
                this.scanForCard.CancelAsync();
            }

            // Close the application.
            this.Dispose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Prevent blocking Windows shutdown.
            if(e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            // Cancel the original close operation.
            e.Cancel = true;
            this.exit.PerformClick();
        }

        private void scanForCard_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stop the reader.
            this.disconnectCardReader.PerformClick();

            // Stop the network.
            this.network.Disconnect();
        }

        #endregion

        #region Background worker.

        private Communication network = new Communication();

        private bool blockReading = false;

        private void scanForCard_DoWork(object sender, DoWorkEventArgs e)
        {
            bool status;

            // Try to initiate the network connection.
            status = this.network.Connect(this.serverAddress);
            if(!status)
            {
                this.applicationStatus.Text = "Server out of reach.";

                e.Cancel = true;
                return;
            }
            else
            {
                this.applicationStatus.Text = string.Empty;
            }

            string cardId;
            while(!this.scanForCard.CancellationPending)
            {
                if(!this.blockReading)
                {
                    cardId = string.Empty;
                    if(this.cardReader.TryRead(out cardId))
                    {
                        // Prevent multiple read.
                        this.blockReading = true;

                        // Print the card ID.
                        this.cardReaderStatus.Text = cardId;

                        Console.WriteLine("cardId = " + cardId.ToString());

                        // Network.
                        string data;
                        if(this.network.Send(cardId) && this.network.Receive(out data))
                        {
                            this.scanForCard.ReportProgress(0, new Payload(data));
                        }
                        else
                        {
                            this.applicationStatus.Text = "Failed to communicate with the server.";
                        }
                    }
                }
            }
        }

        private DisplayTextMessage displayMessage = new DisplayTextMessage();
        private DisplayUserInfo displayUserInfo = new DisplayUserInfo();

        private const int displayInterval = 2000;

        private void scanForCard_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Payload payload = e.UserState as Payload;
            PayloadParser(payload);
        }
        private void PayloadParser(Payload payload)
        {
            if((!payload.Valid) || payload.Due)
            {
                // Set the display message.
                if(!payload.Valid)
                {
                    this.displayMessage.Text = "Invalid user.";
                }
                else
                {
                    this.displayMessage.Text = "Signup time passed.";
                }

                this.displayRegion.Controls.Add(this.displayMessage);
            }
            else
            {
                this.displayUserInfo.RegId = payload.User.RegId;
                this.displayUserInfo.FirstName = payload.User.FirstName;
                this.displayUserInfo.LastName = payload.User.LastName;

                this.displayRegion.Controls.Add(this.displayUserInfo);
            }

            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            // Clear the display region.
            this.displayRegion.Controls.Clear();
            // Clear the status bar.
            this.applicationStatus.Text = string.Empty;
            // TODO: hard-coded the card reader status as connected for now.
            this.cardReaderStatus.Text = "Connected.";

            Console.WriteLine("timer_Tick() raised.");

            // Unblock reading.
            this.blockReading = false;
        }

        #endregion
    }
}
