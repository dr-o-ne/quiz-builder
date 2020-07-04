using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizBuilder.Data.DataProviders {

	public interface IStructureDataProvider {

		Task AddQuizQuestionRelationship( long quizId, long quizItemId );

		Task AddGroupQuestionRelationship( long? groupId, long questionId );

		Task DeleteQuizQuestionRelationship( string quizUId, string quizItemUId );

		Task<IEnumerable<(string, int)>> DeleteQuizRelationships( string quizUId );

		Task<int> RemoveQuizItemRelationships( string quizItemUId );

		Task<int> DeleteQuizQuizItemRelationships( string quizItemUId );

		Task<long> GetQuizItemIdByQuestionUid( string uid );

	}
}
