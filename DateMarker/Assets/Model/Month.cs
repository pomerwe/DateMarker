using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Month
{
  public string MonthName { get; set; }
  public int MonthNumber { get; set; }
  public List<Day> Days { get; set; }
  public int Year { get; set; }

  public Month(string monthName, int month, int year)
  {
    Year = year;
    MonthName = char.ToUpper(monthName[0]) + monthName.Substring(1);
    MonthNumber = month;
    Days = new List<Day>();
    FillDays(year);
  }

  public void FillDays(int year)
  {
    var auxDate = new DateTime(year, MonthNumber, 1);

    for (int i = 1; i <= CalendarUtils.GetMonthNumberOfDays(auxDate); i++)
    {
      auxDate = new DateTime(year, MonthNumber, i);
      int dayOfWeek = (int)auxDate.DayOfWeek;

      Day day = new Day((DayOfWeek)dayOfWeek, i, year, MonthNumber);
      Days.Add(day);
    }
  }

  public void FillEvents(List<DateMarkerEvent> dateMarkerEvents)
  {
    dateMarkerEvents.ForEach(d => 
    {
      GetDay(d.Start.Day).InsertEvent(d);
    });
  }

  public Day GetDay(int number)
  {
    return Days.FirstOrDefault(d => d.DayNumber == number);
  }
}
