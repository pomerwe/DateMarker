public class CalendarMonthAction : CalendarAction
{
    public override void LeftArrowAction(CalendarAdapter adapter)
    {
        adapter.currentMonth = adapter.GetPreviousMonth();
        ReloadCalendar(adapter);
    }

    public override void RightArrowAction(CalendarAdapter adapter)
    {
        adapter.currentMonth = adapter.GetNextMonth();
        ReloadCalendar(adapter);
    }

    public override void ReloadCalendar(CalendarAdapter adapter)
    {
        adapter.ClearMonths();
        adapter.ClearDays();
        adapter.LoadMonths();
        adapter.LoadDays();
    }
}
