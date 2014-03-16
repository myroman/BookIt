namespace BookIt.Business.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }
    }
}