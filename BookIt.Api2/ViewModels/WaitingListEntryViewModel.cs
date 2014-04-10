using BookIt.Business.Models;

namespace BookIt.Api2.ViewModels
{
    public class WaitingListEntryViewModel
    {
        public int PositionInList { get; set; }

        public ApplicationUser User { get; set; }

        public HubResource HubResource { get; set; }

        public string BookedTime { get; set; }
    }
}