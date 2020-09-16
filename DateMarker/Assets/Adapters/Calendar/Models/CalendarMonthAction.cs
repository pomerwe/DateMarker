public class CalendarMonthAction : CalendarAction
{
    public override void LeftArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentMonth(adapter.GetPreviousMonth());
    }

    public override void RightArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentMonth(adapter.GetNextMonth());
    }
}
