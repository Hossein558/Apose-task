using System;
using System.Text.Json.Serialization;

namespace AsposeTasksDemo.Models
{
    public class JiraWorklogPayload
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("project_id")]
        public Guid ProjectId { get; set; }

        [JsonPropertyName("task_id")]
        public Guid TaskId { get; set; }

        [JsonPropertyName("hours_logged")]
        public double HoursLogged { get; set; }

        [JsonPropertyName("log_date")]
        public DateTime LogDate { get; set; }
    }
}
