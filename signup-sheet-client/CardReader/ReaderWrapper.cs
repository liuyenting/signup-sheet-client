using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace signup_sheet_client
{
    class ReaderWrapper
    {
        #region Native functions.

        class NativeMethods
        {
            [DllImport("dcrf32.dll")]
            public static extern short dc_init(short port, uint baud);
            [DllImport("dcrf32.dll")]
            public static extern short dc_exit(int icdev);
            [DllImport("dcrf32.dll")]
            public static extern short dc_card(int icdev, char _Mode, ref ulong Snr);
            [DllImport("dcrf32.dll")]
            public static extern short dc_getver(int icdev, [MarshalAs(UnmanagedType.LPStr)] StringBuilder sdata);
            [DllImport("dcrf32.dll")]
            public static extern short dc_beep(int icdev, uint _Msec);
        }
        
        #endregion

        #region Card reader communication related variables.

        private short readerPort = -1;
        private int handler = -1;

        private const int defaultBaudRate = 115200;

        private const char readMode = (char)0;

        #endregion

        private const int beepDuration = 10;
        private short status;

        public bool Open(short port)
        {
            // Store the port variable.
            this.readerPort = port;

            // Initialize the card reader.
            this.handler = NativeMethods. dc_init(this.readerPort, defaultBaudRate);

            return (this.handler > 0);
        }
        public bool Close()
        {
            this.status = NativeMethods.dc_exit(this.handler);

            // Reset the card reader NOW, otherwise it will trigger unexpected actions.
            this.readerPort = -1;
            this.handler = -1;

            return (this.status == 0);
        }

        public bool Alive
        {
            get
            {
                // Using version number to ping for system status.
                // Default string length is 8 bytes.
                return (NativeMethods.dc_getver(this.handler, new StringBuilder(64)) == 0);
            }
        }

        public bool Beep()
        {
            this.status = NativeMethods.dc_beep(this.handler, beepDuration);
            return (this.status == 0);
        }

        public bool TryRead(out string cardId)
        {
            // Set default value.
            cardId = string.Empty;

            ulong rawIdValue = 0;
            this.status = NativeMethods.dc_card(this.handler, readMode, ref rawIdValue);

            if(this.status == 0)
            {
                // Write the converted value, only if the read operation succeed.
                cardId = Convert.ToString(rawIdValue);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
