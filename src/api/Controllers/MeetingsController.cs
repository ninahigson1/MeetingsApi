using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing.Template;

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
    public IActionResult Get([FromQuery]DateOnly meetingDate)
    {
        var meetings = _context.Meetings.Where(x => x.Date == meetingDate).ToList();
        return Ok(meetings);
    }

    [HttpGet(template: "{meetingId:int}", Name = "GetMeeting")]

    public IActionResult Get([FromRoute] int meetingId)
    {
        var meeting = _context.Meetings.SingleOrDefault(x => x.ID == meetingId);
        if (meeting == null)
        {
            return NotFound();
        }
        return Ok(meeting);
    }

    [HttpPost(Name = "CreateMeeting")]
    public IActionResult Post([FromBody] NewMeeting newMeeting)
    {
        if (newMeeting.Attendees.Count > 5)
        {
            return BadRequest("Meeting cant have more than 5 attendees");
        }

        var meeting = new Meeting()
        {
            Title = newMeeting.Title,
            Date = newMeeting.Date,
            Time = newMeeting.Time,
            Attendees = newMeeting.Attendees
        };
        _context.Meetings.Add(meeting);
        _context.SaveChanges();

        return Ok(meeting);
    }

    [HttpDelete(template:"{meetingId:int}", Name = "Remove Meeting")]
    public IActionResult DeleteMeeting([FromRoute] int meetingId)
    {
        var meeting = _context.Meetings.SingleOrDefault(x => x.ID == meetingId);
        if (meeting == null)
        {
            return NotFound();
        }
        _context.Meetings.Remove(meeting);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut(template: "{meetingId:int}", Name = "Update Meeting")]
    public IActionResult UpdateMeeting([FromRoute] int meetingId, [FromBody] UpdateMeeting updateMeeting)
    {
        var meeting = _context.Meetings.SingleOrDefault(x => x.ID == meetingId);
        if (meeting == null)
        {
            return NotFound();
        }
        meeting.Title = updateMeeting.Title;
        _context.SaveChanges();
        return Ok();
    }
}
