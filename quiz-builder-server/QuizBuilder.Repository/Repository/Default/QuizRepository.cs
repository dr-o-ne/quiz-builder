using QuizBuilder.Model.Model.Default;

namespace QuizBuilder.Repository.Repository.Default {
	public class QuizRepository : GenericRepository<Quiz> {

		public QuizRepository( string connectionString ) : base( connectionString ) {}
	}
}
