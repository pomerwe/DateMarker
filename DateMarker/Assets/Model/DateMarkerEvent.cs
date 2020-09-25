using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DateMarkerEvent
{
  public string Title { get; set; }
  public DateTime Start { get; set; }
  public DateTime End { get; set; }
  public string Description { get; set; }
  public Event GoogleEvent { get; set; }

  public DateMarkerEvent(string title, DateTime start, DateTime end, string description, Event googleEvent = null)
  {
    Title = title;
    Start = start;
    End = end;
    Description = description;
    GoogleEvent = googleEvent;
  }
}
