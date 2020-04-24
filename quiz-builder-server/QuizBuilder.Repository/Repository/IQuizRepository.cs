using System.Collections.Generic;
using QuizBuilder.Model.Model.Default;

namespace QuizBuilder.Repository.Repository
{
    public interface IQuizRepository
    {
		IEnumerable<Quiz> GetAll();

		Quiz GetById(long id);

		Quiz Add( Quiz entity );

		void Delete( long id );

		void Edit( Quiz entity );
	}
}
