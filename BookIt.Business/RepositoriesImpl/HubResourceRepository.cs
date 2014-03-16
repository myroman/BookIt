using System.Collections.Generic;
using System.Linq;

using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Business.RepositoriesImpl
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

        public HubResource Read(int id)
        {
            return resources.SingleOrDefault(x => x.Id == id);
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