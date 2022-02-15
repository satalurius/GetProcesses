using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace CollectServer
{
    public class ClientObj
    {
        public TcpClient client;

        public ClientObj(TcpClient client)
        {
            this.client = client;
        }

        public void Process()
        {
            NetworkStream stream = null;

            try
            {
                stream = client.GetStream();
                byte[] buffer = new byte[65536];


                var builder = new StringBuilder();
                int bytes = 0;

                do
                {
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                } while (stream.DataAvailable);

                string message = builder.ToString();
                // Обрезаем имя машины, с которой пришли данные, для формирования в дальнейшем названия файла
                string machineName = message.Substring(0, message.IndexOf(":"));
                string fileNameBase = machineName + " " + DateTime.Today.ToShortDateString().ToString();
                
                
                message = message.Substring(message.IndexOf(": ")).Remove(0, 2);

                //Обрезать имя операции, для формирования файла
                string processesText = message.Substring(0, message.IndexOf(": "));
                message = message.Substring(message.IndexOf(": ")).Remove(0, 2);
                //Console.WriteLine(machineName);
                Console.WriteLine(message);
                Console.WriteLine(processesText);

                string fileProcessName = fileNameBase + " " + processesText + ".txt";
                Directory.CreateDirectory("data/");
                using (var sw = new StreamWriter(@$"./data/{fileProcessName}", false, Encoding.UTF8))
                {
                    sw.WriteLine(message);
                };



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                client?.Close();
                stream?.Close();
            }
        }
    }
}