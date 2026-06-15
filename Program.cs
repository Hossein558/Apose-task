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
            Aspose.Tasks.Task task1 = rootTask.Children.Add("Task 1: Requirements Gathering");
            Aspose.Tasks.Task task2 = rootTask.Children.Add("Task 2: Design & Prototyping");
            Aspose.Tasks.Task task3 = rootTask.Children.Add("Task 3: Implementation");
            Aspose.Tasks.Task task4 = rootTask.Children.Add("Task 4: Testing & QA");
            Aspose.Tasks.Task task5 = rootTask.Children.Add("Task 5: Deployment");

            // --- مرحله دوم: زمان‌بندی (تعیین Start و Duration برای هر تسک) ---
            DateTime projectStartDate = new DateTime(2026, 6, 15, 8, 0, 0);
            
            // Task 1: ۳ روز زمان می‌برد
            task1.Set(Tsk.Start, projectStartDate);
            task1.Set(Tsk.Duration, project.GetDuration(3, TimeUnitType.Day));
            // فرض می‌کنیم تسک ۱ کامل انجام شده است (۲۴ ساعت کار واقعی)
            task1.Set(Tsk.ActualWork, project.GetDuration(24, TimeUnitType.Hour));

            // Task 2: بعد از اتمام تسک اول شروع می‌شود و ۵ روز زمان می‌برد
            task2.Set(Tsk.Start, projectStartDate.AddDays(3));
            task2.Set(Tsk.Duration, project.GetDuration(5, TimeUnitType.Day));
            // فرض می‌کنیم نیمی از تسک ۲ انجام شده است (۲۰ ساعت کار واقعی از ۴۰ ساعت کل)
            task2.Set(Tsk.ActualWork, project.GetDuration(20, TimeUnitType.Hour));

            // Task 3: بعد از تسک دوم است و ۱۰ روز زمان می‌برد
            task3.Set(Tsk.Start, projectStartDate.AddDays(8));
            task3.Set(Tsk.Duration, project.GetDuration(10, TimeUnitType.Day));
            // هنوز کاری روی این تسک انجام نشده است
            task3.Set(Tsk.ActualWork, project.GetDuration(0, TimeUnitType.Hour));

            // Task 4: همزمان با روزهای پایانی تسک سوم شروع می‌شود (۲ روز)
            task4.Set(Tsk.Start, projectStartDate.AddDays(16));
            task4.Set(Tsk.Duration, project.GetDuration(2, TimeUnitType.Day));
            task4.Set(Tsk.ActualWork, project.GetDuration(0, TimeUnitType.Hour));

            // Task 5: فاز نهایی (۱ روز)
            task5.Set(Tsk.Start, projectStartDate.AddDays(18));
            task5.Set(Tsk.Duration, project.GetDuration(1, TimeUnitType.Day));
            task5.Set(Tsk.ActualWork, project.GetDuration(0, TimeUnitType.Hour));

            // اتصال تسک‌ها به یکدیگر (اختیاری: برای اینکه در گانت چارت پشت سر هم بیفتند)
            project.TaskLinks.Add(task1, task2, TaskLinkType.FinishToStart);
            project.TaskLinks.Add(task2, task3, TaskLinkType.FinishToStart);
            project.TaskLinks.Add(task3, task4, TaskLinkType.FinishToStart);
            project.TaskLinks.Add(task4, task5, TaskLinkType.FinishToStart);

            // به روز رسانی اتوماتیک تاریخ‌ها بر اساس پیوندها
            project.Recalculate();

            Console.WriteLine("5 Tasks created and scheduled successfully.");

            // Save the project to an MPP format
            string outputPath = "OutputProject.mpp";
            project.Save(outputPath, Aspose.Tasks.Saving.SaveFileFormat.Mpp);

            Console.WriteLine($"Project saved successfully to {outputPath}");
        }
    }
}

