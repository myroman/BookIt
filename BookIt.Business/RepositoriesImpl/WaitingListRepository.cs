using System;
using System.Collections.Generic;
using System.Linq;

using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Business.RepositoriesImpl
{
    public class WaitingListRepository : IWaitingListRepository
    {
        private readonly IUserRepository userRepository;

        private readonly IHubResourceRepository hubResourceRepository;

        public WaitingListRepository(IUserRepository userRepository, IHubResourceRepository hubResourceRepository)
        {
            this.userRepository = userRepository;
            this.hubResourceRepository = hubResourceRepository;
        }

        private List<WaitingListEntry> waitingList = new List<WaitingListEntry>();

        public IEnumerable<WaitingListEntry> GetQueuedUsers()
        {
            return waitingList.OrderBy(x => x.TimeOfApply);
        }

        public WaitingListEntry AppendUserToList(User user, int resourceId)
        {
            var foundUser = userRepository.FindUserByCredentials(user);
            if (foundUser == null)
            {
                return null;
            }
            var userId = foundUser.Id;
            if (IfAlreadyRegistered(userId, resourceId))
            {
                return null;
            }

            if (userRepository.Read(userId) != null && hubResourceRepository.Read(resourceId) != null)
            {
                var newItem = new WaitingListEntry
                    {
                        UserId = userId,
                        ResourceId = resourceId,
                        TimeOfApply = DateTime.Now
                    };
                waitingList.Add(newItem);
                return newItem;
            }
            return null;
        }

        private bool IfAlreadyRegistered(int userId, int resourceId)
        {
            return GetQueuedUsers().Any(x => x.UserId == userId && x.ResourceId == resourceId);
        }
    }
}