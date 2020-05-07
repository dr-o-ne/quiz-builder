using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Repository.Repository.Default {

	public sealed class QuestionRepository : GenericRepository<QuestionDto>, IQuestionRepository {

		public QuestionRepository(IDatabaseConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }



	}
}
