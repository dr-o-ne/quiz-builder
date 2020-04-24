using System.Collections.Generic;

namespace QuizBuilder.Repository.Repository {
	public interface IGenericRepository<T> {

		T GetById( long id );

		IEnumerable<T> GetAll();

		long Add( T entity );

		void Delete( long id );

		void Edit( T entity );
	}
}
