using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CollectClient
{
    public class ClientNetwork
    {
        const string ip = "127.0.0.1";
        const int port = 80;
        string message;

        public ClientNetwork(string message)
        {
            this.message = message;
        }

        public void Client()
        {
            TcpClient client = null;

            try
            {
                client = new TcpClient(ip, port);
                var stream = client.GetStream();

                byte[] buffer = Encoding.Unicode.GetBytes(message);

                stream.Write(buffer, 0, buffer.Length);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                client.Close();
            }
        }
    }

}

