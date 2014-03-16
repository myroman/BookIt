using System.Collections.Generic;
using System.Linq;

using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Business.RepositoriesImpl
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>
            {
                new User
                    {
                        Email = "user1@gmail.com",
                        FullName = "John",
                        Id = 1,
                        Password = "qwerty"
                    },
                new User
                    {
                        Email = "user2@gmail.com",
                        FullName = "Mary",
                        Id = 2,
                        Password = "123456"
                    }
            };

        public IEnumerable<User> GetList()
        {
            return users;
        }

        public User Read(int id)
        {
            return users.SingleOrDefault(x => x.Id == id);
        }

        public void Add(User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(User entity)
        {
            throw new System.NotImplementedException();
        }

        public User FindUserByCredentials(User user)
        {
            return users.SingleOrDefault(x => x.Email == user.Email && x.Password == user.Password);
        }
    }
}