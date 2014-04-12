using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using BookIt.Api.Models;
using BookIt.Api.ViewModels;
using BookIt.Business.Abstract;
using BookIt.Business.Models;

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

        [AllowAnonymous]
        [Route("GetA")]
        public async Task<ApplicationUser> GetA()
        {
            var u1 = await userRepository.FindCurrentUser();
            var u2 = await userRepository.FindCurrentUser();
            return u1;
        }

        [Route("GetWaitingList")]
        public async Task<IEnumerable<WaitingListEntryViewModel>> GetWaitingList()
        {
            var queuedUsers = waitingListRepository.GetQueuedUsers();

            var tasks = queuedUsers.Select(async foo => await waitinglistHelper.CreateViewModel(foo)).ToList();
            return await Task.WhenAll(tasks);
        }

        [HttpPost]
        public async Task<ApiResult<WaitingListEntryViewModel>> AddToList(int resourceId)
        {
            var user = await userRepository.FindCurrentUser();
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
            var infoAboutNewPosition = await waitinglistHelper.CreateViewModel(entry);
            return new ApiResult<WaitingListEntryViewModel>(infoAboutNewPosition);
        }
    }
}
