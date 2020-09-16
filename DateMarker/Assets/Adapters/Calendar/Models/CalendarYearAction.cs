public class CalendarYearAction : CalendarAction
{
    public override void LeftArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentYear(adapter.currentYear - 1);
    }

    public override void RightArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentYear(adapter.currentYear + 1);
    }
}
