namespace QuizBuilder.Repository.Repository {

	public interface IGenericQueryBuilder<T> {

		string GenerateInsertQuery();

		string GenerateUpdateQuery();

	}

}
