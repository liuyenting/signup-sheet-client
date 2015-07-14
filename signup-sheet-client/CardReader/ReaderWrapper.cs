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
            public static extern short dc_init(Int16 port, uint baud);
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

        private Int16 readerPort;
        private int handler;

        private const int defaultBaudRate = 115200;

        public bool Open(Int16 port)
        {
            // Initialize the card reader.
            this.handler =NativeMethods. dc_init(port, defaultBaudRate);

            return (handler > 0);
        }
    }
}
