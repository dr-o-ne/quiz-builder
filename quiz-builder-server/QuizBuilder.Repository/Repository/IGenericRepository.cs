using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizBuilder.Repository.Repository {

	public interface IGenericRepository<T> {

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByIdAsync( long id );

		Task<T> GetByUIdAsync( string uid );

		Task<int> AddAsync( T entity );

		Task<int> UpdateAsync( T entity );

		Task<int> DeleteAsync( long id );

		Task<int> DeleteAsync( string uid );

		Task<int> DeleteBulkAsync( List<long> ids );

		Task<int> DeleteBulkAsync( List<string> uids );

	}

}
