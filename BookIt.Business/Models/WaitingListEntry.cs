using System;

namespace BookIt.Business.Models
{
    public class WaitingListEntry
    {
        public string UserId { get; set; }

        public int ResourceId { get; set; }

        public DateTime TimeOfApply { get; set; }

        public DateTime StartOfUsingResource { get; set; }
    }
}