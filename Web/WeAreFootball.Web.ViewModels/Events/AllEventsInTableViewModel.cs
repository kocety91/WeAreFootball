namespace WeAreFootball.Web.ViewModels.Events
{
    using System.Collections.Generic;

    public class AllEventsInTableViewModel : DashboardPagingViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
