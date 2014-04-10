using System.Threading.Tasks;

using BookIt.Business.Models;

namespace BookIt.Business.Abstract
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> FindCurrentUser();

        Task<ApplicationUser> Read(string id);
    }
}