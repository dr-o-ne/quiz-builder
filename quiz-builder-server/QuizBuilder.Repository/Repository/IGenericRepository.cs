using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizBuilder.Repository.Repository {
	public interface IGenericRepository<T> {

		Task<T> GetByIdAsync( long id );

		Task<IEnumerable<T>> GetAllAsync();

		Task<long> AddAsync( T entity );

		void DeleteAsync( long id );

		void EditAsync( T entity );
	}
}
