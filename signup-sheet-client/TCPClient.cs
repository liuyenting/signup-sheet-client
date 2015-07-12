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
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream;

        public TCPClient()
        {
        }

        public void ConnectToServer(string address)
        {
            string[] detail = address.Split(':');

            int port = (detail[1].Length == 0)?12000: Int16.Parse(detail[1]);
            clientSocket.Connect(detail[0], port);
        }

        public void SendData(string dataTosend)
        {
            if(string.IsNullOrEmpty(dataTosend))
                return;
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(dataTosend);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public void CloseConnection()
        {
            clientSocket.Close();
        }
        public string ReceiveData()
        {
            StringBuilder message = new StringBuilder();
            NetworkStream serverStream = clientSocket.GetStream();
            serverStream.ReadTimeout = 100;
            //the loop should continue until no dataavailable to read and message string is filled.
            //if data is not available and message is empty then the loop should continue, until
            //data is available and message is filled.
            while(true)
            {
                if(serverStream.DataAvailable)
                {
                    int read = serverStream.ReadByte();
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
