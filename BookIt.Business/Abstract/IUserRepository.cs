using BookIt.Business.Models;

namespace BookIt.Business.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUserByCredentials(User user);
    }
}