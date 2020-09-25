using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GoogleCalendarEventUtils
{
  public static List<DateMarkerEvent> ToDateMarkerEvents(this Events googleEvents)
  {
    List<DateMarkerEvent> dateMarkerEvents = new List<DateMarkerEvent>();
    googleEvents.Items.ToList().ForEach(i =>
    {
      dateMarkerEvents.Add(new DateMarkerEvent(i));
    });

    return dateMarkerEvents;
  }

  public static DateMarkerEvent ToDateMarkerEvent (this Event googleEvent)
  {
    return new DateMarkerEvent(googleEvent);
  }
}

