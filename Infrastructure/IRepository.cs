using System.Linq.Expressions;

namespace Infrastructure
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Get(Guid id);
        T Update(T entity);
        T Delete(Guid id);
        IEnumerable<T> All(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
