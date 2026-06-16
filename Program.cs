using System;
using System.Collections.Generic;
using Aspose.Tasks;
using Task = Aspose.Tasks.Task;

namespace AsposeTasksDemo
{
    class Program
    {
        static void PrintTasks(Task task, string indent = "")
        {
            if (task.Get(Tsk.Name) != null)
            {
                Console.WriteLine($"{indent}- {task.Get(Tsk.Name)} : {task.Get(Tsk.PercentComplete)}%");
            }
            foreach (Task child in task.Children)
            {
                PrintTasks(child, indent + "  ");
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Starting Aspose.Tasks Demo...");

            try
            {
                Aspose.Tasks.License license = new Aspose.Tasks.License();
                license.SetLicense(@"lib\Aspose.Total.NET.lic");
                Console.WriteLine("License set successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not set license: " + ex.Message);
            }

            try
            {
                Console.WriteLine("Connecting to Project Server...");
                
                string url = "http://project.crouseco.com/project/PMO";
                string username = "svc-sql";
                string password = "$QL@dm!n1400";
                
                System.Net.NetworkCredential netCreds = new System.Net.NetworkCredential(username, password);
                ProjectServerCredentials credentials = new ProjectServerCredentials(url, netCreds);
                ProjectServerManager manager = new ProjectServerManager(credentials);
                
                Console.WriteLine("Fetching project list...");
                var list = manager.GetProjectList();
                Guid simorghId = Guid.Empty;
                foreach (var info in list)
                {
                    if (info.Name != null && info.Name.Contains("سیمرغ"))
                    {
                        simorghId = info.Id;
                        Console.WriteLine($"Found Simorgh Project: {info.Name} [{info.Id}]");
                        break;
                    }
                }
                
                if (simorghId != Guid.Empty)
                {
                    Console.WriteLine("Downloading Simorgh project via Aspose.Tasks...");
                    Project project = manager.GetProject(simorghId);
                    Console.WriteLine("Project downloaded. Listing tasks:");
                    
                    PrintTasks(project.RootTask);
                }
                else
                {
                    Console.WriteLine("Simorgh project not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
