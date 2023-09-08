using Microsoft.AspNetCore.Mvc;

namespace meetingsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MeetingsController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<MeetingsController> _logger;
    private readonly MeetingContext _context;

    public MeetingsController(ILogger<MeetingsController> logger, MeetingContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetMeetings")]
    public IEnumerable<Meeting> Get([FromQuery]DateOnly meetingDate)
    {
        var meetings = _context.Meetings.Where(x => x.Date == meetingDate).ToList();
        return meetings;
    }

    [HttpPost(Name = "CreateMeeting")]
    public IActionResult Post([FromBody] NewMeeting newMeeting)
    {
        if (newMeeting.Attendees.Length > 5)
        {
            return BadRequest("Meeting cant have more than 5 attendees");
        }
        return Ok(1);
    }

    [HttpDelete(template:"{meetingId:int}", Name = "Remove Meeting")]
    public IActionResult DeleteMeeting([FromRoute] int meetingId)
    {
        return Ok();
    }

    [HttpPut(template: "{meetingId:int}", Name = "Update Meeting")]
    public IActionResult UpdateMeeting([FromRoute] int meetingId, [FromBody] UpdateMeeting updateMeeting)
    {
        return Ok();
    }
}
