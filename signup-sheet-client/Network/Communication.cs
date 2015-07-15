using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace signup_sheet_client.Network
{
    class Communication : IDisposable
    {
        private TcpClient clientSocket;
        private NetworkStream serverStream;

        private const int retryTimes = 5;

        private string address;
        private short port;

        public bool Connect(string rawAddress)
        {
            // Split the string according to the ':'.
            string[] address = rawAddress.Split(':');

            this.address = address[0];
            this.port = short.Parse(address[1]);

            // The TcpClient is disposed whenever it fails.
            this.clientSocket = new TcpClient();

            try
            {
                this.clientSocket.Connect(this.address, this.port);
            }
            catch(SocketException)
            {
                return false;
            }
            return this.clientSocket.Connected;
        }
        public void Disconnect()
        {
            Dispose();
        }
        public void Dispose()
        {
            if(this.clientSocket != null)
            {
                this.clientSocket.Close();
            }
        }

        public bool Send(string data)
        {
            // Don't waste the time to send if the payload is empty.
            if(string.IsNullOrEmpty(data))
            {
                return true;
            }

            // Get the stream in order to initaiate the conversation.
            try
            {
                this.serverStream = this.clientSocket.GetStream();

                // Convert the stream to bytes and transmit it.
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                this.serverStream.Write(byteData, 0, byteData.Length);
                this.serverStream.Flush();
            }
            catch(ObjectDisposedException)
            {
                // Server not connected.
                return false;
            }

            return true;
        }
        public bool Receive(out string payload)
        {
            payload = string.Empty;

            if(!this.clientSocket.Connected)
            {
                return false;
            }

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
            payload = data.ToString();
            return true;
        }
    }
}
