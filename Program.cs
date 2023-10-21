using System;
using System.Diagnostics;


namespace Projeto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            menu.add(new MenuItem("Software", "Software", new Action(software)));

            menu.add(new MenuItem("Hardware", "Hardware", new Action(hardware)));

            menu.add(new MenuItem("Hardware e Software", "Hardware e Software", () =>
            {
                software();
                hardware();

            }));

            menu.run();
        }

        static private void software()
        {
            Console.WriteLine("OS Version: " + Environment.OSVersion);

            Process[] runningProcesses = Process.GetProcesses();

            Console.WriteLine("List of Running Processes:");
            foreach (var process in runningProcesses)
            {
                try
                {
                    Console.WriteLine($"Process Name: {process.ProcessName}");
                    Console.WriteLine($"Process ID: {process.Id}");
                    Console.WriteLine($"Main Module File Name: {process.MainModule.FileName}");
                    Console.WriteLine($"Start Time: {process.StartTime}");
                    Console.WriteLine($"Responding: {process.Responding}");
                    Console.WriteLine($"Memory Usage: {process.WorkingSet64 / 1024} KB");
                    Console.WriteLine($"Total Processor Time: {process.TotalProcessorTime}");
                    Console.WriteLine("-----------------------------------------------------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accessing process information: {ex.Message}");
                }
            }

        }

        static private void hardware()
        {
            Console.WriteLine("Machine Name: " + Environment.MachineName);
            Console.WriteLine("OS Version: " + Environment.OSVersion);
            Console.WriteLine("User Name: " + Environment.UserName);
        }
    }
}