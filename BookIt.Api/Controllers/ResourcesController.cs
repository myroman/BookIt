using System.Collections.Generic;
using System.Web.Http;

using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/resources")]
    public class ResourcesController : ApiController
    {
        private readonly IHubResourceRepository resourceRepository;

        public ResourcesController(IHubResourceRepository resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        [Route("All")]
        public IEnumerable<HubResource> GetAllResources()
        {
            return resourceRepository.GetList();
        }
    }
}