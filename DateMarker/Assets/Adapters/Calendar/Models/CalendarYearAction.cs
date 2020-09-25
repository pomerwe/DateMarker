public class CalendarYearAction : CalendarAction
{
    public void LeftArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentYear(adapter.currentYear - 1);
    }

    public void RightArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentYear(adapter.currentYear + 1);
    }
}
