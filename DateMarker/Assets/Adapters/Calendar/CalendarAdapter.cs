using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class CalendarAdapter : MonoBehaviour
{
    public Text yearNumber;
    public Text monthName;

    public GameObject monthBody;
    public GameObject monthPrefab;

    public GameObject daysBody;
    public GameObject dayPrefab;

    public List<GameObject> monthObjectList;
    public List<GameObject> daysObjectList;

    public int currentYear;
    public Month currentMonth;
    public Day currentDay;

    public Calendar currentCalendar;

    public CalendarMode currentCalendarMode;

    public void Start()
    {
        monthObjectList = new List<GameObject>();
        daysObjectList = new List<GameObject>();
        currentYear = DateTime.Now.Year;
        currentCalendar = new Calendar(currentYear);

        currentMonth = currentCalendar.Months
                        .Where(m => m.MonthNumber == DateTime.Now.Month)
                        .FirstOrDefault();

        currentDay = currentMonth.Days
                        .Where(d => d.DayNumber == DateTime.Now.Day)
                        .FirstOrDefault();

        LoadCalendar();
        LoadMonths();
        LoadDays();

        currentCalendarMode = CalendarMode.Month;
    }

    public void LoadCalendar()
    {
        yearNumber.text = currentYear.ToString();
        currentCalendar = new Calendar(currentYear);
    }

    public void LoadMonths()
    {
        currentCalendar.Months.ForEach(m =>
        {
            var instance = Instantiate(monthPrefab, monthBody.transform, false);
            MonthViewModel monthViewModel = instance.GetComponent<MonthViewModel>();
            monthViewModel.SetModel(m);
            monthObjectList.Add(instance);
        });
    }

    public void ClearMonths()
    {
        ClearObjectList(monthObjectList);
    }

    public void LoadDays()
    {
        PreviousDaysFill();

        monthName.text = currentMonth.MonthName;
        currentMonth.Days.ForEach(d =>
        {
            DayViewModel dayViewModel = InstantiateDayPrefab();
            dayViewModel.SetModel(d);
        });

        NextDaysFill();
    }

    public void ClearDays()
    {
        ClearObjectList(daysObjectList);
    }

    public void ClearObjectList(List<GameObject> gameObjectList)
    {
        for(int i = 0; i < gameObjectList.Count; i++)
        {
            var gameObject = gameObjectList[i];
            Destroy(gameObject);
        }

        gameObjectList.Clear();
    }

    public void PreviousDaysFill()
    {
        int fillBaseNumber = GetMonthFillBaseNumber();
        int fillQuantity = GetPreviousDaysFillQuantity();
        for (int i = 0; i < fillQuantity; i++)
        {
            DayViewModel dayViewModel = InstantiateDayPrefab();
            int fillNumber = fillBaseNumber - (fillQuantity - 1) + i;
            dayViewModel.SetFillNumber(fillNumber);
        }
    }

    public void NextDaysFill()
    {
        int fillQuantity = GetNextDaysFillQuantity();
        for (int i = 1; i <= fillQuantity; i++)
        {
            DayViewModel dayViewModel = InstantiateDayPrefab();
            dayViewModel.SetFillNumber(i);
        }
    }

    public DayViewModel InstantiateDayPrefab()
    {
        var instance = Instantiate(dayPrefab, daysBody.transform, false);
        daysObjectList.Add(instance);
        return instance.GetComponent<DayViewModel>();
    }

    public Month GetNextMonth()
    {
        int prevMonthNumber = CalendarUtils.GetNextMonthNumber(currentMonth.MonthNumber);
        return currentCalendar.GetMonth(prevMonthNumber);
    }

    public Month GetPreviousMonth()
    {
        int prevMonthNumber = CalendarUtils.GetPreviousMonthNumber(currentMonth.MonthNumber);
        return currentCalendar.GetMonth(prevMonthNumber);
    }

    public int GetMonthFillBaseNumber()
    {
        return GetPreviousMonth().Days.Count;
    }

    public int GetPreviousDaysFillQuantity()
    {
        return (int)currentMonth.Days.OrderBy(d => d.DayNumber).FirstOrDefault().DayOfWeek;
    }

    public int GetNextDaysFillQuantity()
    {
        return 42 - currentMonth.Days.Count - GetPreviousDaysFillQuantity();
    }

    public void LeftArrow()
    {
        var calendarAction = GetCalendarAction();
        calendarAction.LeftArrowAction(this);
    }

    public void RightArrow()
    {
        var calendarAction = GetCalendarAction();
        calendarAction.RightArrowAction(this);
    }

    public CalendarAction GetCalendarAction()
    {
        var calendarActionFactory = new CalendarActionFactory();
        return calendarActionFactory.CreateCalendarAction(currentCalendarMode);
    }
}

public enum CalendarMode
{
    Year,
    Month
}
