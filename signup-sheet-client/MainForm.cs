using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public static extern int dc_init(Int16 port, long baud);
        [DllImport("dcrf32.dll")]
        public static extern int dc_exit(int icdev);

        #endregion

        private int cardReaderID = -1;

        public MainForm()
        {
            // GUI initialization.
            InitializeComponent();
               
            // Primary functions initialization.
            InitializeReader();
            ConncetWithServer();
        }

        #region Initialization functions.

        private void InitializeReader()
        {
            SetCardReaderStatus("Connecting...", Color.Black);

            // Initialize the card reader.
            cardReaderID = dc_init(1, 115200);

            // Check the status of the card reader.
            if(cardReaderID <= 0)
            {
                SetCardReaderStatus("Failes.", Color.Red);
                throw new InvalidOperationException("Failed to connect the card reader.");
            }
            else
            {
                SetCardReaderStatus("Success!", Color.Green);
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

        #endregion
    }
}
