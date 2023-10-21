using System;
using System.Diagnostics;


namespace Projeto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            menu.add(new MenuItem("Software", "Software", new Func<string>(software)));

            menu.add(new MenuItem("Hardware", "Hardware", new Func<string>(hardware)));

            menu.add(new MenuItem("Hardware_Software", "Hardware e Software", () =>
            {
                string content = string.Empty;
                content += software();
                content += hardware();

                return content;

            }));

            menu.run();
        }

        static string software()
        {
            Console.WriteLine("OS Version: " + Environment.OSVersion);

            Process[] runningProcesses = Process.GetProcesses();

            string info = "List of Running Processes:\n";

            foreach (var process in runningProcesses)
            {
                try
                {
                    info += "Process Name: " + process.ProcessName + '\n';
                    info += "Process ID: " + process.Id + '\n';
                    info += "Main Module File Name: " + process.MainModule.FileName + '\n';
                    info += "Start Time: " + process.StartTime + '\n';
                    info += "Responding: " + process.Responding + '\n';
                    info += "Memory Usage: " + process.WorkingSet64 / 1024 + " KB" + '\n';
                    info += "Total Processor Time: " + process.TotalProcessorTime + '\n';
                    info += "----------------------- " + '\n';
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accessing process information: {ex.Message}");
                }
            }

            Console.WriteLine(info);
            return info;

        }

        static private string hardware()
        {
            string info = "Hardware Information:\n";
            info += "Machine Name: " + Environment.MachineName + '\n';
            info += "OS Version: " + Environment.OSVersion + '\n';
            info += "User Name: " + Environment.UserName + '\n';

            return info;
        }
    }
}