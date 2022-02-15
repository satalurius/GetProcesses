using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CollectServer
{
    class Program
    {
        const string ip = "127.0.0.1";
        const int port = 80;
        static TcpListener tcpListener;
        
        static void Main(string[] args)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse(ip), port);
                //tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                Console.WriteLine("Ждем подключения...");

                while (true)
                {
                    var client = tcpListener.AcceptTcpClient();
                    var clientObj = new ClientObj(client);

                    var clientThread = new Thread(new ThreadStart(clientObj.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                tcpListener?.Stop();
            }

        }
    }
}