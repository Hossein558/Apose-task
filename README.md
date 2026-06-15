# AsposeTasksDemo

A simple console application demonstrating how to use the **Aspose.Tasks** library in C# to create and schedule project tasks, and export them to an `.mpp` (Microsoft Project) format.

## Features
- Initializes a new Project.
- Creates a root task with 5 child tasks representing different phases of a software project.
- Sets start dates, durations, and actual work for tasks.
- Links tasks together sequentially (Finish-to-Start).
- Automatically recalculates dates based on task dependencies.
- Saves the resulting project as `OutputProject.mpp`.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later.
- A valid Aspose license (`Aspose.Total.NET.lic`) placed inside the `lib/` folder (optional, but without it, saving the MPP file might trigger Evaluation Mode limitations).

## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/Hossein558/Apose-task.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Apose-task
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## Output
After a successful run, a file named `OutputProject.mpp` will be generated in the project's root directory, which you can open with Microsoft Project.
