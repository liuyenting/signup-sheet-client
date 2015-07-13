/**
 * signup-sheet-client
 * Copyright (C) 2015 Yen-Ting Liu
 * windows.linux.mac@gmail.com
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace signup_sheet_client
{
    public partial class MainForm : Form
    {
        #region Card reader library functions.

        [DllImport("dcrf32.dll")]
        public static extern short dc_init(Int16 port, uint baud);
        [DllImport("dcrf32.dll")]
        public static extern short dc_exit(int icdev);
        [DllImport("dcrf32.dll")]
        public static extern short dc_card(int icdev, char _Mode, ref ulong Snr);
        [DllImport("dcrf32.dll")]
        public static extern short dc_getver(int icdev, [MarshalAs(UnmanagedType.LPStr)] StringBuilder sdata);
        [DllImport("dcrf32.dll")]
        public static extern short dc_beep(int icdev, uint _Msec);

        #endregion

        private Int16 cardReaderPort = -1;
        private int cardReaderId = -1;

        private string serverAddress = "";
        private TCPClient tcpclient = new TCPClient();

        #region Timer related private variables.

        private const int displayTime = 2000;
        // True for the first time.
        private bool canProbe = true;

        #endregion

        public MainForm()
        {
            // GUI initialization.
            InitializeComponent();

            // Change the parent of the invalid user label,
            // in order to decouple it from the panel.
            this.invalidUserLabel.Parent = this;

            // Hide the default user info panel and invalid text.
            UserInfoVisibility(false);
            InvalidUser(false);

            // Set the timers, but not triggered.
            this.userInfoTimer.Interval = this.invalidUserTimer.Interval = displayTime;
        }

        #region Connection related functions.

        /**
         * Initialize the card reader through assigned port, port 100 is hard coded as USB in the API.
         * @arg port The port that the card reader is at.
         * @return bool Indicate whether the initialization successful or not.
         */
        private bool InitializeCardReader(Int16 port)
        {
            SetCardReaderStatus("Connecting...", Color.Black);

            // Initialize the card reader.
            this.cardReaderId = dc_init(port, 115200);

            // Check the status of the card reader.
            if(cardReaderId <= 0)
            {
                SetCardReaderStatus("Failed.", Color.Red);
                //throw new InvalidOperationException("Failed to connect the card reader.");
                return false;
            }
            else
            {
                SetCardReaderStatus("Connected.", Color.Green);

                // Start the background worker.
                cardReaderBackgroundWorker.RunWorkerAsync();

                return true;
            }
        }

        /**
         * Probing card reader's version to indicate whether the card reader is still online.
         * @return True if the reader is still connected.
         */
        private bool CheckReaderStatus()
        {
            StringBuilder version = new StringBuilder(64);
            return (dc_getver(this.cardReaderId, version) == 0);
        }

        private void DisconnectReader()
        {
            // Stop the background worker.
            cardReaderBackgroundWorker.CancelAsync();

            SetCardReaderStatus("Disconnecting...", Color.Black);

            // Initialize the card reader.
            short status = dc_exit(this.cardReaderId);

            // Check the status of the card reader.
            if(status == 0)
            {
                SetCardReaderStatus("Disconnected.", Color.Black);
            }
            else
            {
                SetCardReaderStatus("Fail to disconnect.", Color.Red);
                //throw new InvalidOperationException("Failed to connect the card reader.");
            }

            // Reset the handler.
            this.cardReaderId = -1;
        }

        private void TryServerConnection()
        {
            try
            {
                this.tcpclient.ConnectToServer(this.serverAddress);

                serverStatusLabel.Text = "Connected to " + this.serverAddress;
                serverStatusLabel.ForeColor = Color.Green;
            }
            catch(Exception ex)
            {
                serverStatusLabel.Text = "Failed to connect the server.";
                serverStatusLabel.ForeColor = Color.Red;
            }
        }

        #endregion

        #region Reader background worker.

        private void cardReaderBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while(!(sender as BackgroundWorker).CancellationPending)
            {
                // Stop the probing if the reader is offline.
                if(!CheckReaderStatus())
                {
                    worker.ReportProgress(100, null);
                    break;
                }

                // Prevent multiple scan.
                if(this.canProbe)
                {
                    ulong cardId = 0;
                    // Operate in MODE1, ALL mode.
                    short status = dc_card(this.cardReaderId, (char)0, ref cardId);
                    if(status == 0)
                    {
                        // Stop the probe flag until the timer say so.
                        canProbe = false;

                        string json = string.Empty;
                        try
                        {
                            // TODO: send the cardId to server-side, and get the JSON response.
                            this.tcpclient.SendData(Convert.ToString(cardId));
                            json = this.tcpclient.ReceiveData();
                        }
                        catch
                        {
                            worker.ReportProgress(1, null);
                        }

                        // Parse user data from the response.
                        State newMessage = JsonConvert.DeserializeObject<State>(json);

                        if(newMessage != null)
                        {
                            worker.ReportProgress(0, newMessage);

                            // Beep to notify the user that the reader has acknowledge the card.
                            dc_beep(this.cardReaderId, 10);
                        }
                    }
                }
            }

            e.Cancel = true;
        }

        private void cardReaderBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage == 0)
            {
                State newMessage = e.UserState as State;

                // Show invalid user if the flag bit is set.
                if(newMessage.Valid)
                {
                    // Display acquired card ID.
                    SetCardStatus(newMessage.User.CardId.ToString(), Color.Black);

                    SetUserInfo(newMessage.User);
                }
                else
                {
                    InvalidUser();
                }
            }
            else if(e.ProgressPercentage == 1)
            {
                serverStatusLabel.Text = "Not connected.";
                serverStatusLabel.ForeColor = Color.Red;
            }
            else
            {
                // Stop the probing.
                this.stopReading.PerformClick();
            }
        }

        #endregion

        #region GUI support functions.

        private void SetCardReaderStatus(string statusString, Color stringColor)
        {
            this.cardReaderStatus.Text = statusString;
            this.cardReaderStatus.ForeColor = stringColor;
        }

        private void SetCardStatus(string cardId, Color stringColor)
        {
            this.cardStatus.Text = cardId;
            this.cardStatus.ForeColor = stringColor;
        }
        private void ResetCardStatus()
        {
            SetCardStatus("-", Color.Black);
        }

        #region Invalid user.

        private void InvalidUser()
        {
            // Show the content and start the timer.
            InvalidUser(true);
            invalidUserTimer.Start();
        }
        private void InvalidUser(string reason)
        {
            // Set the text.
            this.invalidUserLabel.Text = reason;

            InvalidUser();

            // Reset the text.
            this.invalidUserLabel.Text = "Invalid";
        }

        private void invalidUserTimer_Tick(object sender, EventArgs e)
        {
            this.invalidUserTimer.Stop();
            InvalidUser(false);
            ResetCardStatus();

            this.canProbe = true;
        }
        private void InvalidUser(bool visible)
        {
            this.invalidUserLabel.Visible = visible;
        }

        #endregion

        #region Valid user.

        private void SetUserInfo(UserInfo newUserInfo)
        {
            // Update the content.
            userId.Text = newUserInfo.RegId;
            userFirstName.Text = newUserInfo.FirstName;
            userLastName.Text = newUserInfo.LastName;

            // Show the content and start the timer.
            UserInfoVisibility(true);
            userInfoTimer.Start();
        }

        private void userInfoTimer_Tick(object sender, EventArgs e)
        {
            this.userInfoTimer.Stop();
            UserInfoVisibility(false);
            ResetCardStatus();

            this.canProbe = true;
        }
        private void UserInfoVisibility(bool visible)
        {
            // Hide the control and its children recursively.
            UserInfoVisibility(visible, this.userInfoPanel);
        }
        private void UserInfoVisibility(bool visible, Control control)
        {
            if(control.HasChildren)
            {
                foreach(Control child in control.Controls)
                {
                    UserInfoVisibility(visible, child);
                }
            }
            control.Visible = visible;
        }

        #endregion

        #endregion

        #region Menu button actions.

        private void connectReader_Click(object sender, EventArgs e)
        {
            // Hard coded the reader as USB.
            cardReaderPort = 100;

            // Initialize the reader through helper function.
            if(InitializeCardReader(cardReaderPort))
            {
                // Enable the stop button if initialization succeed.
                this.stopReading.Visible = true;
                this.connectReader.Visible = false;
            }
        }

        private void stopReading_Click(object sender, EventArgs e)
        {
            DisconnectReader();

            // Enable the connection detail.
            this.connectReader.Visible = true;
            this.stopReading.Visible = false;
        }

        private void setServer_Click(object sender, EventArgs e)
        {
            AskForServerIP();
        }
        private void AskForServerIP()
        {
            // Initiate the prompt dialog.
            using(AskForServer promptDialog = new AskForServer(this.serverAddress))
            {
                if(promptDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.serverAddress = promptDialog.Address;
                }
            }

            TryServerConnection();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.tcpclient.CloseConnection();
            DisconnectReader();
            this.Dispose();
        }

        #endregion   
    }

    public class State
    {
        [JsonProperty("valid")]
        private bool valid = true;
        [JsonIgnore]
        public bool Valid
        {
            get
            {
                return this.valid;
            }
        }

        [JsonProperty("due")]
        private bool due = false;
        [JsonIgnore]
        public bool Due
        {
            get
            {
                return this.due;
            }
        }

        [JsonProperty("user")]
        private UserInfo user;
        [JsonIgnore]
        public UserInfo User
        {
            get
            {
                return this.user;
            }
        }
    }

    public class UserInfo
    {
        [JsonProperty("cardId")]
        private ulong cardId = 0;
        [JsonIgnore]
        public ulong CardId
        {
            get
            {
                return this.cardId;
            }
            // Temporary enable set...
            set
            {
                this.cardId = value;
            }
        }

        [JsonProperty("regId")]
        private string regId = string.Empty;
        [JsonIgnore]
        public string RegId
        {
            get
            {
                return this.regId;
            }
        }

        [JsonProperty("firstName")]
        private string firstName = string.Empty;
        [JsonIgnore]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
        }

        [JsonProperty("lastName")]
        private string lastName = string.Empty;
        [JsonIgnore]
        public string LastName
        {
            get
            {
                return this.lastName;
            }
        }
    }
}
