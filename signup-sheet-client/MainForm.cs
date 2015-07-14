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
        private DisplayTextMessage displayMessage = new DisplayTextMessage();
        private DisplayUserInfo displayUserInfo = new DisplayUserInfo();

        public MainForm()
        {
            InitializeComponent();

            // Setup the forms.
            this.displayMessage.Dock = DockStyle.Fill;
            this.displayMessage.Visibility = false;
            this.displayMessage.Show();

            this.displayUserInfo.Dock = DockStyle.Fill;
            this.displayUserInfo.Visibility = false;
            this.displayUserInfo.Show();

            // Setup timer.
            this.timer.Interval = displayInterval;

            // Set the status bar.
            this.cardReaderStatus.Text = "Disconnected.";
            this.cardReaderStatus.ForeColor = Color.Black;
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
                this.cardReaderStatus.ForeColor = Color.Black;

                // Change menu state if connected.
                this.disconnectCardReader.Visible = this.cardReaderConnected;
                this.connectCardReader.Visible = !this.disconnectCardReader.Visible;
            
                // Start the background worker.

            }
            else
            {
                this.cardReaderStatus.Text = "Fail to connect.";
                this.cardReaderStatus.ForeColor = Color.Red;
            }
        }

        private void disconnectCardReader_Click(object sender, EventArgs e)
        {
            // Stop the background worker.
            this.scanForCard.RunWorkerAsync();

            // Disconnect.
            this.cardReaderConnected = !this.cardReader.Close();

            // Check if successfully closed.
            if(!this.cardReaderConnected)
            {
                // Change menu state if connected.
                this.disconnectCardReader.Visible = this.cardReaderConnected;
                this.connectCardReader.Visible = !this.disconnectCardReader.Visible;

                this.cardReaderStatus.Text = "Disconnected.";
                this.cardReaderStatus.ForeColor = Color.Black;
            }
            else
            {
                this.cardReaderStatus.Text = "Fail to disconnect.";
                this.cardReaderStatus.ForeColor = Color.Red;
            }
        }

        #endregion

        private string serverAddress = string.Empty;

        private void setAddress_Click(object sender, EventArgs e)
        {
            using(AskForServer dialog = new AskForServer(this.serverAddress))
            {
                
            }
        }

        #region Program stop.

        private void exit_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        #endregion

        #region Background worker.

        private Communication network = new Communication();
        private Payload response;

        private void scanForCard_DoWork(object sender, DoWorkEventArgs e)
        {
            string cardId;
            while(!this.scanForCard.CancellationPending)
            {
                cardId = string.Empty;
                if(this.cardReader.TryRead(out cardId))
                {
                    // Print the card ID.
                    this.cardReaderStatus.Text = cardId;
                    this.cardReaderStatus.ForeColor = Color.Blue;

                    this.network.Send(cardId);
                    this.response = new Payload(this.network.Receive());
                }
            }
        }

        

        private const int displayInterval = 2000;

        private void PayloadParser(Payload payload)
        {
            if((!payload.Valid) || (!payload.Due))
            {
                // Set the display message.
                if(!payload.Valid)
                {
                    this.displayMessage.Text = "Invalid user.";
                    this.displayMessage.Color = Color.Red;
                }
                else
                {
                    this.displayMessage.Text = "Due.";
                    this.displayMessage.Color = Color.Black;
                }

                this.displayMessage.Visibility = true;
            }
            else
            {
                this.displayUserInfo.RegId = payload.User.RegId;
                this.displayUserInfo.FirstName = payload.User.FirstName;
                this.displayUserInfo.LastName = payload.User.LastName;

                this.displayUserInfo.Visibility = true;
            }

            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            this.displayMessage.Visibility = false;
            this.displayUserInfo.Visibility = false;
        }

        private void scanForCard_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        #endregion
    }
}
