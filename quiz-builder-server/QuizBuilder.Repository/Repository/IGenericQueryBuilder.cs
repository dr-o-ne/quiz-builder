namespace QuizBuilder.Repository.Repository {

	public interface IGenericQueryBuilder<T> {

		string GetInsertQuery();

		string GetUpdateQuery();

	}

}
