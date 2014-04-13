using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using BookIt.Api.ViewModels;
using BookIt.Business.Abstract;

namespace BookIt.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Waitlist")]
    public class WaitlistController : ApiController
    {
        private readonly IWaitingListRepository waitingListRepository;

        private readonly WaitinglistHelper waitinglistHelper;

        private readonly IUserRepository userRepository;

        private readonly IHubResourceRepository hubResourceRepository;

        public WaitlistController(IWaitingListRepository waitingListRepository, WaitinglistHelper waitinglistHelper, IUserRepository userRepository, IHubResourceRepository hubResourceRepository)
        {
            this.waitingListRepository = waitingListRepository;
            this.waitinglistHelper = waitinglistHelper;
            this.userRepository = userRepository;
            this.hubResourceRepository = hubResourceRepository;
        }

        [Route("GetWaitingList")]
        public Task<IEnumerable<WaitingListEntryViewModel>> GetWaitingList()
        {
            var queuedUsers = waitingListRepository.GetQueuedUsers();
            var waitlistItems = queuedUsers.Select(waitlistItem => waitinglistHelper.CreateViewModel(waitlistItem));
            return Task.FromResult(waitlistItems);
        }

        [HttpPost]
        public ApiResult<WaitingListEntryViewModel> AddToList(int resourceId)
        {
            var user = userRepository.FindCurrentUser();
            if (user == null)
            {
                return new ApiResult<WaitingListEntryViewModel>("There's no user with such ID");
            }
            var res = hubResourceRepository.Read(resourceId);
            if (res == null)
            {
                return new ApiResult<WaitingListEntryViewModel>("Wrong resource ID");
            }

            var entry = waitingListRepository.AppendUserToList(user, res);
            if (entry == null)
            {
                var errorMsg = string.Format("User {0} has already been added to resource {1}", user.UserName, resourceId);
                return new ApiResult<WaitingListEntryViewModel>(errorMsg);
            }
            var infoAboutNewPosition = waitinglistHelper.CreateViewModel(entry);
            infoAboutNewPosition.User = user;
            return new ApiResult<WaitingListEntryViewModel>(infoAboutNewPosition);
        }
    }
}
