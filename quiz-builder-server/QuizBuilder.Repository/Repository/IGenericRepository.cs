using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizBuilder.Repository.Repository {

	public interface IGenericRepository<T> {

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByIdAsync( long id );

		Task<int> AddAsync( T entity );

		Task<int> UpdateAsync( T entity );

		Task<int> DeleteAsync( long id );

	}

}
