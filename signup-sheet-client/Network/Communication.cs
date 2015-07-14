using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace signup_sheet_client.Network
{
    class Communication
    {
        private TcpClient clientSocket = new TcpClient();
        private NetworkStream serverStream;

        public void Connect(string rawAddress)
        {
            // Split the string according to the ':'.
            string[] address = rawAddress.Split(':');

            short parsedPortNumber = short.Parse(address[1]);
            this.clientSocket.Connect(address[0], parsedPortNumber);
        }
        public void Disconnect()
        {
            this.clientSocket.Close();
        }

        public void Send(string data)
        {
            // Don't waste the time to send if the payload is empty.
            if(string.IsNullOrEmpty(data))
            {
                return;
            }

            // Get the stream in order to initaiate the conversation.
            this.serverStream = this.clientSocket.GetStream();

            // Convert the stream to bytes and transmit it.
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            this.serverStream.Write(byteData, 0, byteData.Length);
            this.serverStream.Flush();
        }
        public string Receive()
        {
            // Create the buffer to store the input.
            StringBuilder data = new StringBuilder();
            this.serverStream = this.clientSocket.GetStream();

            this.serverStream.ReadTimeout = 100;

            // The loop should continue until no more data needed.
            int input;
            while(true)
            {
                if(this.serverStream.DataAvailable)
                {
                    input = this.serverStream.ReadByte();
                    if(input > 0)
                    {
                        data.Append((char)input);
                    }
                    else
                    {
                        break;
                    }
                }
                else if(data.ToString().Length > 0)
                {
                    break;
                }
            }

            // Convert back to string object from StringBuilder.
            return data.ToString();
        }
    }
}
