using System;
using Aspose.Tasks;

namespace AsposeTasksDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Aspose.Tasks Demo...");

            // اعمال لایسنس (اگر فایل لایسنس دارید، مسیر آن را در اینجا قرار دهید)
            // در صورت عدم تنظیم لایسنس، ذخیره فایل به فرمت MPP با خطای Evaluation Mode مواجه می‌شود
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

            // Create a new project
            Project project = new Project();

            // Add a root task
            Aspose.Tasks.Task rootTask = project.RootTask;
            rootTask.Set(Tsk.Name, "My Main Project");

            // Add 5 tasks to the root task
            for (int i = 1; i <= 5; i++)
            {
                Aspose.Tasks.Task task = rootTask.Children.Add($"Task {i}");
                
                // Set some properties for the task
                task.Set(Tsk.Start, new DateTime(2025, 1, i, 8, 0, 0));
                task.Set(Tsk.Duration, project.GetDuration(1, TimeUnitType.Day));
                
                Console.WriteLine($"Added {task.Get(Tsk.Name)}");
            }

            // Save the project to an MPP format
            string outputPath = "OutputProject.mpp";
            project.Save(outputPath, Aspose.Tasks.Saving.SaveFileFormat.Mpp);

            Console.WriteLine($"Project saved successfully to {outputPath}");
        }
    }
}

