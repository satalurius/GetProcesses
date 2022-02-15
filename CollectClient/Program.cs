using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using CollectProcesses;

namespace CollectClient
{
    internal class Program
    {
         static void Main(string[] args)
        {

            if (args.Length == 0)
                BaseHelpMenu();
            else
                MenuBaseOperations(args);
        }

        static void BaseHelpMenu()
        {
            const string helpMessageText = "/all \t- вывести список процессов \n" +
                "/threads (pid) \t- вывести потоки по pid процесса \n" +
                "/module (pid) - вывести список модулей процесса по pid\t";

            Console.WriteLine(helpMessageText);
        }
        static void MenuBaseOperations(string[] args)
        {
            const string getAll = "/all";
            const string getThreads = "/threads";
            const string getModule = "/module";
            int pid = 0;
            var workPr = new WorkWithProcesses();

            // Если передано два аргумента, работаем с методами, использующими PID
            if (args.Length == 2)
            {
                try
                {
                    pid = int.Parse(args[1]);

                    switch(args[0].ToString())
                    {
                        case getThreads:
                            workPr.GetThreadsByPID(pid);
                            break;
                        case getModule:
                            workPr.InfoByModuleProcByPID(pid);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            if ( args[0].ToString() == getAll)
            {
                workPr.GetAllProcesses();
            }
            else if(args.Length == 1)
            {
                Console.WriteLine("Необходим PID вторым аргументом");
            }
        }
    }
}
