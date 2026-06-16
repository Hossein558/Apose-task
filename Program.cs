using System;
using Aspose.Tasks;

namespace AsposeTasksDemo
{
    class Program
    {
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
                    if (info.Name != null && info.Name.Contains("سیمرغ") && !info.Name.Contains("سیمرغ 2"))
                    {
                        simorghId = info.Id;
                        Console.WriteLine($"Found Source Project: {info.Name} [{info.Id}]");
                        break;
                    }
                }
                
                if (simorghId != Guid.Empty)
                {
                    Console.WriteLine("Downloading Source project via Aspose.Tasks...");
                    Project project = manager.GetProject(simorghId);
                    
                    Console.WriteLine("Creating exact copy as 'سیمرغ 2'...");
                    
                    var saveOptions = new ProjectServerSaveOptions
                    {
                        ProjectName = "سیمرغ 2"
                    };
                    
                    manager.CreateNewProject(project, saveOptions);
                    Console.WriteLine("Successfully created 'سیمرغ 2' on the server!");
                }
                else
                {
                    Console.WriteLine("Source project 'سیمرغ' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
