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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private bool InitializeReader(Int16 port)
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
            throw new NotImplementedException();
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
                    worker.ReportProgress(1, null);
                    break;
                }

                // Prevent multiple scan.
                if(this.canProbe)
                {
                    UserInfo newInfo = new UserInfo();

                    ulong cardId = 0;
                    // Operate in MODE1, ALL mode.
                    short status = dc_card(this.cardReaderId, (char)0, ref cardId);
                    if(status == 0)
                    {
                        // Stop the probe flag until the timer say so.
                        canProbe = false;

                        // TODO: Request for info from server side.
                        // TODO: Valid or not depend on server response.
                        newInfo.Valid = true;

                        // Write new data into the object.
                        newInfo.CardId = cardId;

                        worker.ReportProgress(0, newInfo);

                        // Beep to notify the user that the reader has acknowledge the card.
                        dc_beep(this.cardReaderId, 10);
                    }
                }
            }
        }

        private void cardReaderBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage == 0)
            {
                UserInfo newInfo = e.UserState as UserInfo;

                // Display acquired card ID.
                SetCardStatus(newInfo.CardId.ToString(), Color.Black);

                // Show invalid user if the flag bit is set.
                if(newInfo.Valid)
                {
                    SetUserInfo(newInfo);
                }
                else
                {
                    InvalidUser();
                }
            }
            else
            {
                // Stop the probing.
                this.stopReadingToolStripMenuItem.PerformClick();
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

        private void SetUserInfo(UserInfo newInfo)
        {
            // Update the content.
            userAvatar.Image = newInfo.Avatar;
            userFirstName.Text = newInfo.FirstName;
            userLastName.Text = newInfo.LastName;

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

        private string[] ScanValidCOMPorts()
        {
            return SerialPort.GetPortNames();
        }

        private void AskForServerIP()
        {
            // Initiate the prompt dialog.
            AskForServer promptDialog = new AskForServer();
            
            if(promptDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.serverAddress = promptDialog.Address;
            }

            // Close the dialog.
            promptDialog.Dispose();

            TryServerConnection();
        }

        #endregion

        #region Menu button actions.

        private void cardReaderToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            string[] validPorts = ScanValidCOMPorts();

            // Wipe previous scan result in the menu.
            this.cardReaderToolStripMenuItem.DropDownItems.Clear();

            // Use dummy content if no valid COM port exists...
            ToolStripMenuItem usbToolStripMenuItem = new ToolStripMenuItem("USB");
            // ...hard coded to 100, reference the manual.
            usbToolStripMenuItem.Tag = 100;
            usbToolStripMenuItem.Click += portToolStripMenuItem_Click;
            this.cardReaderToolStripMenuItem.DropDownItems.Add(usbToolStripMenuItem);

            // Add the COM port scan results.
            foreach(string port in validPorts)
            {
                // Create a temporary object.
                ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem(port);
                // Set the tag of the menu item.
                newToolStripMenuItem.Tag = port.Substring(3);
                // Assign the click event to the menu item.
                newToolStripMenuItem.Click += portToolStripMenuItem_Click;

                // Add the item into the menu.
                this.cardReaderToolStripMenuItem.DropDownItems.Add(newToolStripMenuItem);
            }
        }

        private void portToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to parse the tag in the menu item.
            cardReaderPort = -1;
            if(!Int16.TryParse((sender as ToolStripMenuItem).Tag.ToString(), out cardReaderPort))
            {
                throw new InvalidOperationException("Unable to parse the tag, please notify the designer.");
            }

            // Disconnect the reader first if it's still active.
            if(this.cardReaderId != -1)
            {
                DisconnectReader();
            }

            // Initialize the reader through helper function.
            if(InitializeReader(cardReaderPort))
            {
                // Enable the stop button if initialization succeed.
                this.stopReadingToolStripMenuItem.Visible = true;
                this.cardReaderToolStripMenuItem.Visible = false;
            }
        }

        private void stopReadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisconnectReader();

            // Enable the connection detail.
            this.cardReaderToolStripMenuItem.Visible = true;
            this.stopReadingToolStripMenuItem.Visible = false;
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskForServerIP();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisconnectReader();
            this.Dispose();
        }

        #endregion
    }

    public class UserInfo
    {
        private bool valid = true;
        private ulong cardId = 0;
        private Image avatar;
        private String[] name = {"FIRST", "NAME"};

        #region Encapsulations.

        public ulong CardId
        {
            get
            {
                return cardId;
            }
            set
            {
                cardId = value;
            }
        }

        public bool Valid
        {
            get
            {
                return valid;
            }
            set
            {
                valid = value;
            }
        }

        public Image Avatar
        {
            get
            {
                return avatar;
            }
            set
            {
                avatar = value;
            }
        }

        public string FirstName
        {
            get
            {
                return name[0];
            }
            set
            {
                name[0] = value;
            }
        }

        public string LastName
        {
            get
            {
                return name[1];
            }
            set
            {
                name[1] = value;
            }
        }
    
        #endregion
    }
}
