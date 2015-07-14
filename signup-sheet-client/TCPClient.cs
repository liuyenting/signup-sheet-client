using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace signup_sheet_client
{
    class TCPClient
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream;

        public TCPClient()
        {
        }

        public void ConnectToServer(string address)
        {
            string[] detail = address.Split(':');

            int port = (detail[1].Length == 0) ? 12000 : Int16.Parse(detail[1]);
            this.clientSocket.Connect(detail[0], port);
        }
        public void CloseConnection()
        {
            this.clientSocket.Close();
        }

        public void SendData(string dataTosend)
        {
            // Don't waste the time sending if the payload is empty.
            if(string.IsNullOrEmpty(dataTosend))
            {
                return;
            }

            // Get the stream in order to initiate the conversation.
            this.serverStream = this.clientSocket.GetStream();

            // Convert the stream for output format, byte stream.
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(dataTosend);

            // Perform the communication.
            this.serverStream.Write(outStream, 0, outStream.Length);
            this.serverStream.Flush();
        }

        public string ReceiveData()
        {
            // Start to construct the input data stream storage.
            StringBuilder message = new StringBuilder();
            this.serverStream = this.clientSocket.GetStream();

            this.serverStream.ReadTimeout = 100;

            // The loop should continue until no more data is coming.
            while(true)
            {
                if(this.serverStream.DataAvailable)
                {
                    int read = this.serverStream.ReadByte();
                    if(read > 0)
                        message.Append((char)read);
                    else
                        break;
                }
                else if(message.ToString().Length > 0)
                    break;
            }
            return message.ToString();
        }
    }
}
