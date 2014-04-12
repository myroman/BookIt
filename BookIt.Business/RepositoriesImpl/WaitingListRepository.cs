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

        public WaitingListRepository(IHubResourceRepository hubResourceRepository, IUserRepository userRepository)
        {
            this.hubResourceRepository = hubResourceRepository;
            this.userRepository = userRepository;
        }

        private List<WaitingListEntry> waitingList = new List<WaitingListEntry>();

        public IEnumerable<WaitingListEntry> GetQueuedUsers()
        {
            return waitingList.OrderBy(x => x.TimeOfApply);
        }

        public WaitingListEntry AppendUserToList(ApplicationUser user, HubResource resource)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            var userId = user.Id;
            if (IfAlreadyRegistered(userId, resource.Id))
            {
                return null;
            }

            var newItem = new WaitingListEntry
            {
                UserId = userId,
                ResourceId = resource.Id,
                TimeOfApply = DateTime.Now
            };
            waitingList.Add(newItem);
            return newItem;
        }

        private bool IfAlreadyRegistered(string userId, int resourceId)
        {
            return GetQueuedUsers().Any(x => x.UserId == userId && x.ResourceId == resourceId);
        }
    }
}