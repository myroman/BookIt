using System.Collections.Generic;

using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Business.Impl
{
    public class HubResourceRepository : IHubResourceRepository
    {
        private List<HubResource> resources = new List<HubResource>
            {
                new HubResource
                    {
                        Id = 1,
                        Name = "Billiard room",
                        Description = "On the 2nd floor"
                    },
                new HubResource
                    {
                        Id = 2,
                        Name = "Meeting room 323",
                        Description = "Main conference room"
                    }
            };

        public IEnumerable<HubResource> GetList()
        {
            return resources;
        }

        public void Add(HubResource entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(HubResource entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(HubResource entity)
        {
            throw new System.NotImplementedException();
        }
    }
}