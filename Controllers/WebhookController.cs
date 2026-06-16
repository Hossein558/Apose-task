using System;
using Microsoft.AspNetCore.Mvc;
using AsposeTasksDemo.Models;
using AsposeTasksDemo.Services;
using Microsoft.Extensions.Logging;

namespace AsposeTasksDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ProjectServerService _projectServerService;
        private readonly ILogger<WebhookController> _logger;

        public WebhookController(ProjectServerService projectServerService, ILogger<WebhookController> logger)
        {
            _projectServerService = projectServerService;
            _logger = logger;
        }

        [HttpPost("jira")]
        public IActionResult ReceiveJiraWorklog([FromBody] JiraWorklogPayload payload)
        {
            if (payload == null)
            {
                return BadRequest("Invalid payload.");
            }

            try
            {
                _logger.LogInformation($"Received worklog from Jira: {payload.HoursLogged} hours for user {payload.Username} on Task {payload.TaskId}");

                _projectServerService.UpdateActualWork(
                    projectId: payload.ProjectId,
                    taskId: payload.TaskId,
                    username: payload.Username,
                    hoursLogged: payload.HoursLogged
                );

                _logger.LogInformation("Successfully updated Actual Work in Project Server.");
                return Ok(new { message = "Worklog synchronized successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error synchronizing worklog to Project Server.");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
