using System;
using AsposeTasksDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AsposeTasksDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Set up Aspose.Tasks License
            try
            {
                Aspose.Tasks.License license = new Aspose.Tasks.License();
                license.SetLicense(@"lib\Aspose.Total.NET.lic");
                Console.WriteLine("Aspose.Tasks License set successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not set Aspose.Tasks license: " + ex.Message);
            }

            // Add services to the container.
            builder.Services.AddControllers();
            
            // Register ProjectServerService as a Singleton (or Scoped if preferred)
            builder.Services.AddSingleton<ProjectServerService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
