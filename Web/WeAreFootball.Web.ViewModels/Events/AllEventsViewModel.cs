namespace WeAreFootball.Web.ViewModels.Events
{
    using System.Collections.Generic;

    public class AllEventsViewModel : PagingViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
