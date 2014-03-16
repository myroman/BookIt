﻿using System.Collections.Generic;

using BookIt.Business.Models;

namespace BookIt.Business.Abstract
{
    public interface IWaitingListRepository
    {
        IEnumerable<WaitingListEntry> GetQueuedUsers();

        WaitingListEntry AppendUserToList(int userId, int resourceId);
    }
}