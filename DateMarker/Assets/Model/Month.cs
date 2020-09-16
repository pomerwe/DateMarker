using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Month 
{
    public string MonthName { get; set; }
    public int MonthNumber { get; set; }
    public List<Day> Days { get; set; }

    public Month(string monthName, int month, int year)
    {
        
        MonthName = char.ToUpper(monthName[0]) + monthName.Substring(1);
        MonthNumber = month;
        Days = new List<Day>();
        FillCalendar(year);
    }

    public void FillCalendar(int year)
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
}
