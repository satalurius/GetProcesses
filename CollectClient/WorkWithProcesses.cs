using CollectClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CollectProcesses
{
    public class WorkWithProcesses
    {
        ClientNetwork clientNetwork = null;
        public void GetAllProcesses()
        {
           
            var getProcesses = from proc in Process.GetProcesses()
                               orderby proc.Id
                               select proc;

            string message = $"{Environment.MachineName}: processes: {"PID",-10} \t {"Name",-10} \n";
            foreach (var pr in getProcesses)
            {
                message = message.Insert(message.Length, $"-> {pr.Id,-10} \t {pr.ProcessName,-10} \n");
            }
            message = message.Insert(message.Length, $"Процессов запущено: {getProcesses.Count()}");
            clientNetwork = new ClientNetwork(message);
            clientNetwork.Client();
        }

        public void GetThreadsByPID(int pid)
        {
           
            Process proc = null;

            try
            {
                proc = Process.GetProcessById(pid);
                ProcessThreadCollection threads = proc.Threads;

                string message = $"{Environment.MachineName}: threads: Потоки процесса {proc.ProcessName}\n";
                foreach (ProcessThread pt in threads)
                {
                    message = message.Insert(message.Length, $"-> Thread ID: {pt.Id}\t " +
                        $"Время: {pt.StartTime.ToShortTimeString()}\t " +
                        $"Приоритет: {pt.PriorityLevel} \n");
                }
                clientNetwork = new ClientNetwork(message);
                clientNetwork.Client();

                          }
            catch (Exception ex)
            {
                Console.WriteLine($"Нет процесса с pid: {pid}");
            }
        }

       public void InfoByModuleProcByPID(int pid)
        {
          
            Process process = null;

            try
            {
                process = Process.GetProcessById(pid);
                ProcessModuleCollection moduleCollection = process.Modules;
                string message = $"{Environment.MachineName}: moduleProcess: Подключаемые модули процесса {process.ProcessName}\n";

                foreach(ProcessModule pm in moduleCollection)
                {
                    message = message.Insert(message.Length, $"-> Имя модуля: {pm.ModuleName}\n");
                }

                ClientNetwork client = new ClientNetwork(message);
                client.Client();
             
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
