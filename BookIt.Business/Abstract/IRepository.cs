using System.Collections.Generic;

namespace BookIt.Business.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();

        T Read(int id);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}