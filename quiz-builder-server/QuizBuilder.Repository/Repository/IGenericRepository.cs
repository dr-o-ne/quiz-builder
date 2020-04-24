using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizBuilder.Repository.Repository {

	public interface IGenericRepository<T> {

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByIdAsync( long id );

		Task<long> AddAsync( T entity );

		Task UpdateAsync( T entity );

		Task DeleteAsync( long id );

	}

}
