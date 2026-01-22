using Microsoft.AspNetCore.Mvc;
using Records.MessageRecords;

[Route("/api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
   public readonly DateTime StartTime = DateTime.Now;
   List<Message> logs = [new (DateOnly.FromDateTime(DateTime.Now), "SERVER", ["SERVER INITIALIZED"])];

   [HttpPost("")]
   public IActionResult SendMessage([FromBody] Message msg) {
        return Ok(new AllLogs(logs));
   }

   [HttpGet("logs")]
   public ActionResult<AllLogs> GetLogs() {
        return Ok(new AllLogs(logs));
   }

   [HttpGet("uptime")]
   public ActionResult<Uptime> GetUptime() {
        DateTime Current = DateTime.Now;
        return Ok(new Uptime(StartTime, Current, StartTime - Current));
   }
}
