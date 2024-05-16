using Data;
using System;
using System.Linq.Expressions;

namespace Infrastructure
{
    public abstract class GenericRepository<T>
        : IRepository<T> where T : class
    {
        private readonly RentingContext context;

        public GenericRepository(RentingContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual T Add(T entity)
        {
            return context.Add(entity).Entity;
        }

        public virtual IEnumerable<T> All(T entity)
        {
            return context.Set<T>().ToList();
        }
        public virtual T Update(T entity)
        {
            return context.Add(entity).Entity;
        }

        public virtual T Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToList();
        }

        public virtual T Get(Guid id)
        {
            return context.Find<T>(id);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }


    }
}
