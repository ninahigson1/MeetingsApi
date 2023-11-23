namespace meetingsApi;

public class Meeting
{
    public DateOnly Date { get; set; }

    public int ID { get; set; }

    public string Title { get; set; }

    public string Time { get; set; }

    public List<string> Attendees { get; set; }
}

public class NewMeeting
{
    public DateOnly Date { get; set; }

    public string Time { get; set; }

    public List<string> Attendees { get; set; }

    public string Title { get; set; }
}

public class UpdateMeeting
{
    public string Title { get; set; }
}