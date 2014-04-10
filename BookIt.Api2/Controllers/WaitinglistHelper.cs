using System.Linq;
using System.Threading.Tasks;

using BookIt.Api2.ViewModels;
using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Api2.Controllers
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

        public async Task<WaitingListEntryViewModel> CreateViewModel(WaitingListEntry model)
        {
            var user = await userRepository.Read(model.UserId);
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