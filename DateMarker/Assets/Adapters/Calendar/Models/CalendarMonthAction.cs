public class CalendarMonthAction : CalendarAction
{
    public void LeftArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentMonth(adapter.GetPreviousMonth());
    }

    public void RightArrowAction(CalendarAdapter adapter)
    {
        adapter.SetCurrentMonth(adapter.GetNextMonth());
    }
}
