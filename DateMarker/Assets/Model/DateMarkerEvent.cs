using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class DateMarkerEvent
{
  private string title;
  public string Title 
  { 
    get
    {
      return title;
    }
    set
    {
      title = value;
      GoogleEvent.Summary = title;
    }
  }

  private DateTime start;
  public DateTime Start
  {
    get
    {
      return start;
    }
    set
    {
      start = value;
      GoogleEvent.Start.DateTime = start;
    }
  }

  public DateTime end;
  public DateTime End
  {
    get
    {
      return end;
    }
    set
    {
      end = value;
      GoogleEvent.End.DateTime = end;
    }
  }

  public string description;
  public string Description
  {
    get
    {
      return description;
    }
    set
    {
      description = value;
      GoogleEvent.Description = description;
    }
  }
  public Event GoogleEvent { get; }

  public DateMarkerEvent(string title, DateTime start, DateTime end, string description)
  {
    Title = title;
    Start = start;
    End = end;
    Description = description;
    GoogleEvent = new Event()
    {
      Start = new EventDateTime() { DateTime = Start },
      End = new EventDateTime() { DateTime = End },
      Description = Description,
      Summary = Title
    };
  }

  public DateMarkerEvent(Event googleEvent)
  {
    GoogleEvent = googleEvent;
    Title = googleEvent.Summary;
    Start = (DateTime)googleEvent.Start.DateTime;
    End = (DateTime)googleEvent.End.DateTime;
    Description = googleEvent.Description;
  }
}
