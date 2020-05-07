using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizBuilder.Repository.Repository {

	public interface IGenericRepository<T> {

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByIdAsync( Guid id );

		Task<int> AddAsync( T entity );

		Task<int> UpdateAsync( T entity );

		Task<int> DeleteAsync( Guid id );

		Task<int> DeleteBulkAsync( List<Guid> ids );

	}

}
