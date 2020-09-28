using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day
{
    public string Event { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int DayNumber { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public List<DateMarkerEvent> CalendarEvents { get; set; }

    public Day(DayOfWeek weekDay, int dayNumber, int year, int month)
    {
        Year = year;
        Month = month;
        DayOfWeek = weekDay;
        DayNumber = dayNumber;
        CalendarEvents = new List<DateMarkerEvent>();
    }

    public void InsertEvent(DateMarkerEvent dateMarkerEvent)
    {
        CalendarEvents.Add(dateMarkerEvent);
    }
    public void RemoveEvent(DateMarkerEvent dateMarkerEvent)
    {
        var d = CalendarEvents.Where(c => c.GoogleEvent.Id == dateMarkerEvent.GoogleEvent.Id).FirstOrDefault();

        if (d != null)
        {
            CalendarEvents.Remove(d);
        }
    }
}
