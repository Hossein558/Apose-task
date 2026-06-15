using System;

namespace DotNet_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Aspose.Total for .NET Licenses");
            Console.WriteLine("--------------------------------------");

            string projectDir = System.IO.Directory.GetCurrentDirectory();
            string licenseFile = System.IO.Path.Combine(projectDir, "lib", "Aspose.Total.NET.lic");
            string tasksLicenseFile = System.IO.Path.Combine(projectDir, "lib", "Aspose.Tasks.NET.lic");
            int successCount = 0;
            int failCount = 0;

            // List of fully qualified License classes
            string[] licenseClasses = new string[]
            {
                "Aspose.ThreeD.License, Aspose.3D",
                "Aspose.Cells.License, Aspose.Cells",
                "Aspose.Diagram.License, Aspose.Diagram",
                "System.Drawing.AsposeDrawing.License, Aspose.Drawing",
                "Aspose.Email.License, Aspose.Email",
                "Aspose.Font.License, Aspose.Font",
                "Aspose.Html.License, Aspose.HTML",
                "Aspose.Imaging.License, Aspose.Imaging",
                "Aspose.Page.License, Aspose.Page",
                "Aspose.Pdf.License, Aspose.PDF",
                "Aspose.PSD.License, Aspose.PSD",
                "Aspose.Pub.License, Aspose.PUB",
                "Aspose.Slides.License, Aspose.Slides",
                "Aspose.Svg.License, Aspose.SVG",
                "Aspose.Tasks.License, Aspose.Tasks",
                "Aspose.TeX.License, Aspose.TeX"
            };

            foreach (var cls in licenseClasses)
            {
                string productName = cls.Split(',')[1].Trim();
                try
                {
                    Type type = Type.GetType(cls);
                    if (type != null)
                    {
                        object licenseObj = Activator.CreateInstance(type);
                        var method = type.GetMethod("SetLicense", new Type[] { typeof(string) });
                        string licToUse = (productName == "Aspose.Tasks") ? tasksLicenseFile : licenseFile;
                        method.Invoke(licenseObj, new object[] { licToUse });
                        Console.WriteLine($"[SUCCESS] {productName,-20} | License applied successfully.");
                        successCount++;
                    }
                    else
                    {
                        Console.WriteLine($"[ERROR]   {productName,-20} | Could not find License class.");
                        failCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[FAILED]  {productName,-20} | {ex.InnerException?.Message ?? ex.Message}");
                    failCount++;
                }
            }

            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Successful: {successCount}");
            Console.WriteLine($"Failed: {failCount}");

            if (failCount > 0)
            {
                Environment.Exit(1);
            }
        }
    }
}
