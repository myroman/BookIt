namespace BookIt.Business.Models
{
    public class HubResource : DomainObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}