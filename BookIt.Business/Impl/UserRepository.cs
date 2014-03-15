using System.Collections.Generic;

using BookIt.Business.Abstract;
using BookIt.Business.Models;

namespace BookIt.Business.Impl
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>
            {
                new User
                    {
                        Email = "user1@gmail.com",
                        FullName = "John Smith",
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
    }
}