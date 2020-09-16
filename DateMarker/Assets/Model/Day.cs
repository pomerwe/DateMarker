using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day
{
    public string Event { get; set; }
    public DayOfWeek DayOfWeek { get; set; }    
    public int DayNumber { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }

    public Day(DayOfWeek weekDay, int dayNumber, int year, int month)
    {
        Year = year;
        Month = month;
        DayOfWeek = weekDay;
        DayNumber = dayNumber;
    }
}
