using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QuizBuilder.Model.Model;
using QuizBuilder.Model.Model.Default.Base;

namespace QuizBuilder.Repository.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
