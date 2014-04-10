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

        public WaitinglistHelper(IHubResourceRepository hubResourceRepository, IUserRepository userRepository, IWaitingListRepository waitingListRepository)
        {
            this.hubResourceRepository = hubResourceRepository;
            this.userRepository = userRepository;
            this.waitingListRepository = waitingListRepository;
        }

        public WaitingListEntryViewModel CreateViewModel(WaitingListEntry model)
        {
            return new WaitingListEntryViewModel
                {
                    HubResource = hubResourceRepository.Read(model.ResourceId),
                    User = userRepository.Read(model.UserId),
                    PositionInList = waitingListRepository.GetQueuedUsers().ToList().IndexOf(model) + 1,
                    BookedTime = model.TimeOfApply.ToString()
                };
        }
    }
}