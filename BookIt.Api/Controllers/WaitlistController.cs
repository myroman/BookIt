using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using BookIt.Api.ViewModels;
using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Api.Controllers
{
    [Authorize]
    public class WaitlistController : ApiController
    {
        private readonly IWaitingListRepository waitingListRepository;

        private readonly WaitinglistHelper waitinglistHelper;

        public WaitlistController(IWaitingListRepository waitingListRepository, WaitinglistHelper waitinglistHelper)
        {
            this.waitingListRepository = waitingListRepository;
            this.waitinglistHelper = waitinglistHelper;
        }

        public IEnumerable<WaitingListEntryViewModel> GetWaitingList()
        {
            return waitingListRepository.GetQueuedUsers().Select(x => waitinglistHelper.CreateViewModel(x));
        }

        [HttpPost]
        public WaitingListEntryViewModel AddToList(User user, int resourceId)
        {
            var entry = waitingListRepository.AppendUserToList(user, resourceId);
            if (entry == null)
            {
                return null;
            }
            var infoAboutNewPosition = waitinglistHelper.CreateViewModel(entry);
            return infoAboutNewPosition;
        }
    }
}
