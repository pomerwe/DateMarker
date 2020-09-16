using System;
public class CalendarActionFactory : ICalendarActionFactory
{
    public CalendarAction CreateCalendarAction(CalendarMode mode)
    {
        switch (mode)
        {
            case CalendarMode.Month:
                {
                    return new CalendarYearAction();
                }
            case CalendarMode.Days:
                {
                    return new CalendarMonthAction();
                }
            default:
                {
                    throw new Exception("Calendar mode not implemented");
                }
        }
    }
}