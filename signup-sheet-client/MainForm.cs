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
               
            // Primary functions initialization.
            //InitializeReader();
            //ConncetWithServer();
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
            }
        }

        private void ConncetWithServer()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Support functions.

        private void SetCardReaderStatus(string statusString, Color stringColor)
        {
            this.cardReaderStatus.Text = statusString;
            this.cardReaderStatus.ForeColor = stringColor;
        }

        private string[] ScanValidCOMPorts()
        {
            return SerialPort.GetPortNames();
        }

        public void AskForServerIP()
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
}
