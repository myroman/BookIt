using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BookIt.Business.Abstract;
using BookIt.Business.Models;

using Microsoft.AspNet.Identity;

namespace BookIt.Business.RepositoriesImpl
{
    public class UserRepository : IUserRepository
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        private List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                    {
                        Email = "user1@gmail.com",
                        FullName = "John",
                        Password = "qwerty"
                    },
                new ApplicationUser
                    {
                        Email = "user2@gmail.com",
                        FullName = "Mary",
                        Password = "123456"
                    }
            };

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public IEnumerable<ApplicationUser> GetList()
        {
            return users;
        }

        public ApplicationUser Read(int id)
        {
            return null;
        }

        public void Add(ApplicationUser entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ApplicationUser entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ApplicationUser entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApplicationUser> FindCurrentUser()
        {
            var id = Thread.CurrentPrincipal.Identity.GetUserId();

            return await Read(id);
        }

        public async Task<ApplicationUser> Read(string id)
        {
            var foundUser = await UserManager.FindByIdAsync(id);
            return foundUser;
        }
    }
}