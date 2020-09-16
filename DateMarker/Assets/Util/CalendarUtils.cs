using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class CalendarUtils 
{
    public static int GetMonthNumberOfDays(DateTime date)
    {
        if(date.Month == 2 )
        {
            return DateTime.IsLeapYear(date.Year) ? 29 : 28;
        }
        else
        {
            if(date.Month <= 7)
            {
                return date.Month % 2 != 0 ? 31 : 30;
            }
            else
            {
                return date.Month % 2 != 0 ? 30 : 31;
            }
        }
    }

    public static int GetPreviousMonthNumber(int month)
    {
        return month == 1 ? 12 : month - 1;
    }

    public static int GetNextMonthNumber(int month)
    {
        return month == 12 ? 1 : month + 1;
    }
}
