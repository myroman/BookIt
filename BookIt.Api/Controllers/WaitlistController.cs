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

        public WaitlistController(IWaitingListRepository waitingListRepository, WaitinglistHelper waitinglistHelper, IUserRepository userRepository)
        {
            this.waitingListRepository = waitingListRepository;
            this.waitinglistHelper = waitinglistHelper;
            this.userRepository = userRepository;
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
        public async Task<WaitingListEntryViewModel> AddToList(int resourceId)
        {
            var user = await userRepository.FindCurrentUser();
            var entry = waitingListRepository.AppendUserToList(user, resourceId);
            if (entry == null)
            {
                return null;
            }
            var infoAboutNewPosition = await waitinglistHelper.CreateViewModel(entry);
            return infoAboutNewPosition;
        }
    }
}
