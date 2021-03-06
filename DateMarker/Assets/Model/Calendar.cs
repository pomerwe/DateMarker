﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Calendar
{
  public List<Month> Months { get; set; }
  public int Year { get; set; }

  public Calendar(int year)
  {
    Months = new List<Month>();
    FillCalendar(year);
  }

  public void FillCalendar(int year)
  {
    Year = year;
    for (int i = 1; i <= 12; i++)
    {
      var auxDate = new DateTime(year, i, 1);
      string monthName = auxDate.ToString("MMMM");
      Month month = new Month(monthName, i, year);
      Months.Add(month);
    }
  }

  public void FillEvents(List<DateMarkerEvent> dateMarkerEvents)
  {
    for(int i = 0; i < Months.Count; i++)
    {
      var m = Months[i];
      m.FillEvents(dateMarkerEvents.Where(d => d.Start.Month == m.MonthNumber).ToList());
    }
  }

  public Month GetMonth(int month)
  {
    return Months.FirstOrDefault(m => m.MonthNumber == month);
  }
}

