public class CalendarYearAction : CalendarAction
{
    public override void LeftArrowAction(CalendarAdapter adapter)
    {
        adapter.currentYear -= 1;
        ReloadCalendar(adapter);
    }

    public override void RightArrowAction(CalendarAdapter adapter)
    {
        adapter.currentYear += 1;
        ReloadCalendar(adapter);
    }

    public override void ReloadCalendar(CalendarAdapter adapter)
    {
        adapter.ClearMonths();

        adapter.LoadCalendar();
        adapter.LoadMonths();
    }
}
