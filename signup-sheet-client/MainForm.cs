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
        public static extern int dc_init(Int16 port, uint baud);
        [DllImport("dcrf32.dll")]
        public static extern int dc_exit(int icdev);

        #endregion

        private int cardReaderID = -1;
        private string serverAddress = "";

        public MainForm()
        {
            // GUI initialization.
            InitializeComponent();

            // Hide the default user info panel.
            UserInfoVisibility(false);
        }

        #region Connection related functions.

        private void InitializeReader(Int16 port)
        {
            SetCardReaderStatus("Connecting...", Color.Black);

            // Initialize the card reader.
            this.cardReaderID = dc_init(port, 115200);

            // Check the status of the card reader.
            if(cardReaderID <= 0)
            {
                SetCardReaderStatus("Failed.", Color.Red);
                //throw new InvalidOperationException("Failed to connect the card reader.");
            }
            else
            {
                SetCardReaderStatus("Connected.", Color.Green);
            }
        }

        private void DisconnectReader()
        {
            SetCardReaderStatus("Disconnecting...", Color.Black);

            // Initialize the card reader.
            int status = dc_exit(this.cardReaderID);

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
        }

        private void ConncetWithServer()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Reader background worker.

        private void cardReaderBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for(int i = 1; (i <= 10); i++)
            {
                if((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress((i * 10));
                }
            }
        }

        private void cardReaderBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        #endregion

        #region GUI support functions.

        private void SetCardReaderStatus(string statusString, Color stringColor)
        {
            this.cardReaderStatus.Text = statusString;
            this.cardReaderStatus.ForeColor = stringColor;
        }

        private void SetUserInfo(UserInfo newInfo)
        {
            // Hide the panel first...
            UserInfoVisibility(false);

            // Update the content.
            userAvatar.Image = newInfo.Avatar;
            userFirstName.Text = newInfo.FirstName;
            userLastName.Text = newInfo.LastName;

            // ...restore the visibility of the panel.
            UserInfoVisibility(true);
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
            Int16 port = -1;
            if(!Int16.TryParse((sender as ToolStripMenuItem).Tag.ToString(), out port))
            {
                throw new InvalidOperationException("Unable to parse the tag, please notify the designer.");
            }

            // Initialize the reader through helper function.
            InitializeReader(port);
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
        private Image avatar;
        private String[] name;
        
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
    }
}
