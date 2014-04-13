using BookIt.Business.Models;

namespace BookIt.Business.Abstract
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser FindCurrentUser();

        ApplicationUser Read(string id);
    }
}