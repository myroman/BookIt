using System.Collections.Generic;

using BookIt.Business.Models;

namespace BookIt.Business.Abstract
{
    public interface IWaitingListService
    {
        IEnumerable<WaitingList> GetQueuedUsers();

        void AppendUserToList(User user);
    }
}