using System;
using System.Linq;
using Aspose.Tasks;

namespace AsposeTasksDemo.Services
{
    public class ProjectServerService
    {
        private readonly ProjectServerManager _manager;

        public ProjectServerService()
        {
            string url = "http://project.crouseco.com/project/PMO";
            string username = "svc-sql";
            string password = "$QL@dm!n1400";

            System.Net.NetworkCredential netCreds = new System.Net.NetworkCredential(username, password);
            ProjectServerCredentials credentials = new ProjectServerCredentials(url, netCreds);
            _manager = new ProjectServerManager(credentials);
        }

        public void UpdateActualWork(Guid projectId, Guid taskId, string username, double hoursLogged)
        {
            // 1. Get Project
            Project project = _manager.GetProject(projectId);
            if (project == null)
            {
                throw new Exception($"Project with ID {projectId} not found.");
            }

            // 2. Get Task
            var taskIdString = taskId.ToString().ToLower();
            var task = project.EnumerateAllChildTasks().FirstOrDefault(t => 
                t.Get(Tsk.Guid) != null && t.Get(Tsk.Guid).ToLower() == taskIdString);
                
            if (task == null)
            {
                throw new Exception($"Task with ID {taskId} not found in project.");
            }

            // 3. Get Resource Assignment for the specific user
            // We match by Resource.Name or Resource.WindowsUserAccount
            var assignment = project.ResourceAssignments.FirstOrDefault(a => 
                a.Get(Asn.Task) != null &&
                a.Get(Asn.Task).Get(Tsk.Guid) != null &&
                a.Get(Asn.Task).Get(Tsk.Guid).ToLower() == taskIdString &&
                a.Get(Asn.Resource) != null && 
                (string.Equals(a.Get(Asn.Resource).Get(Rsc.Name), username, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(a.Get(Asn.Resource).Get(Rsc.WindowsUserAccount), username, StringComparison.OrdinalIgnoreCase)));

            if (assignment == null)
            {
                throw new Exception($"No assignment found for user '{username}' on task '{task.Get(Tsk.Name)}'.");
            }

            // 4. Update Actual Work
            // Convert hours to duration (1 hour = 1 hour TimeSpan)
            var currentWork = assignment.Get(Asn.ActualWork);
            TimeSpan addedWork = TimeSpan.FromHours(hoursLogged);
            
            // Add the new hours to existing ActualWork
            TimeSpan newWork = currentWork.TimeSpan.Add(addedWork);
            assignment.Set(Asn.ActualWork, project.GetDuration(newWork.TotalHours, TimeUnitType.Hour));

            // 5. Update project on server
            _manager.UpdateProject(project);
        }
    }
}
