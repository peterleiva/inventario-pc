using System;
using System.Management;


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
                content = $"\n\n----------Software------------\n\n";
                content += software();
                content = $"\n\n----------Hardware-------------\n\n";
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
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            ManagementObjectCollection collection = searcher.Get();

            string info = string.Empty;

            info += "\n\n" + "Informações sobre o Processador: " + '\n';

            foreach (ManagementObject obj in collection)
            {
                info += "Nome do Processador: " + obj["Name"] + '\n';
                info += "Fabricante: " + obj["Manufacturer"] + '\n';
                info += "Núcleos: " + obj["NumberOfCores"] + '\n';
            }

            info += "\n\n" + "Informações sobre Memória: " + '\n';

            ManagementObjectSearcher memorySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectCollection memoryCollection = memorySearcher.Get();

            foreach (ManagementObject obj in memoryCollection)
            {
                var memoriaEmBytes = (ulong)obj["TotalPhysicalMemory"];
                double memoriaEmMegabytes = (double)memoriaEmBytes / (1024 * 1024);
                string memoriaFormatada = memoriaEmMegabytes.ToString("0.##") + "MB";
                info += "Memória em Uso: " + memoriaFormatada + '\n';
            }

            ManagementObjectSearcher memoryUsageSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection memoryUsageCollection = memoryUsageSearcher.Get();

            foreach (ManagementObject obj in memoryUsageCollection)
            {
                var memoriaEmBytes = (ulong)obj["TotalVisibleMemorySize"];
                double memoriaEmMegabytes = (double)memoriaEmBytes / (1024 * 1024);
                string memoriaFormatada = memoriaEmMegabytes.ToString("0.##") + "MB";
                info += "Memória em Uso: " + memoriaFormatada + '\n';
            }

            info += "\n\n" + "Dispositivos de Entrada e Saída: " + '\n';


            // Informações sobre dispositivos de entrada e saída
            ManagementObjectSearcher ioDeviceSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");
            ManagementObjectCollection ioDeviceCollection = ioDeviceSearcher.Get();

            info += "\nDispositivos de Entrada e Saída:";
            foreach (ManagementObject obj in ioDeviceCollection)
            {
                info += "Dispositivo: " + obj["Description"] + '\n';
            }


            Console.WriteLine(info);
            return info;
        }
    }
}