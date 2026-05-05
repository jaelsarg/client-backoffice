using Howestprime.Backoffice.Components.Planning;

namespace Howestprime.Backoffice.Components.Pages;

public partial class Planning
{
    string month = DateTime.Now.ToString("MMMM");
    int year = DateTime.Now.Year;
    private MonthNavigator MonthNavigator { get; set; }
    private CalendarGrid CalendarGrid { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await RenderCalendar();
        }
    }

    protected async Task RenderCalendar()
    {
        MonthNavigator.UpdateMonthYearHeader(month, year);
        await CalendarGrid.RenderCalendarDays(month, year);
    }

    protected async Task OnMonthChange(DateTime newDate)
    {
        month = newDate.ToString("MMMM");
        year = newDate.Year;
        await RenderCalendar();
    }
}