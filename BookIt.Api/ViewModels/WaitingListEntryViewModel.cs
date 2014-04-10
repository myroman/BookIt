using BookIt.Business.Models;

namespace BookIt.Api.ViewModels
{
    public class WaitingListEntryViewModel
    {
        public int PositionInList { get; set; }

        public User User { get; set; }

        public HubResource HubResource { get; set; }

        public string BookedTime { get; set; }
    }
}