using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using BookIt.Api.ViewModels;
using BookIt.Business.Abstract;

namespace BookIt.Api.Controllers
{
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

        public WaitingListEntryViewModel PostAddToList(int userId, int resourceId)
        {
            var entry = waitingListRepository.AppendUserToList(userId, resourceId);

            if (entry == null)
            {
                return null;
            }
            var infoAboutNewPosition = waitinglistHelper.CreateViewModel(entry);
            return infoAboutNewPosition;
        }
    }
}
