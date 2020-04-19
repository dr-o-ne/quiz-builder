using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizBuilder.Model.Model;

namespace QuizBuilder.Repository.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        protected readonly QuizBuilderDataContext _entities;
        protected readonly DbSet<T> _dbset;

        public GenericRepository(QuizBuilderDataContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() => _dbset.AsEnumerable<T>();

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
            => _dbset.Where(predicate).AsEnumerable();

        public virtual T Add(T entity) => _dbset.Add(entity)?.Entity;

        public virtual T Delete(T entity) => _dbset.Remove(entity)?.Entity;

        public virtual void Edit(T entity) => _entities.Entry(entity).State = EntityState.Modified;

        public virtual void Save() => _entities.SaveChanges();
    }
}
