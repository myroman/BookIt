using System.Linq;

using BookIt.Api.ViewModels;
using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Api.Controllers
{
    public class WaitinglistHelper
    {
        private readonly IHubResourceRepository hubResourceRepository;

        private readonly IUserRepository userRepository;

        private readonly IWaitingListRepository waitingListRepository;

        public WaitinglistHelper(IHubResourceRepository hubResourceRepository, IWaitingListRepository waitingListRepository, IUserRepository userRepository)
        {
            this.hubResourceRepository = hubResourceRepository;
            this.waitingListRepository = waitingListRepository;
            this.userRepository = userRepository;
        }

        public WaitingListEntryViewModel CreateViewModel(WaitingListEntry model)
        {
            var user = userRepository.Read(model.UserId);
            return new WaitingListEntryViewModel
                {
                    HubResource = hubResourceRepository.Read(model.ResourceId),
                    User = user,
                    PositionInList = waitingListRepository.GetQueuedUsers().ToList().IndexOf(model) + 1,
                    BookedTime = model.TimeOfApply.ToString()
                };
        }
    }
}