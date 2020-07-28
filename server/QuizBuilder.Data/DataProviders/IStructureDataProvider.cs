using System.Collections.Immutable;
using System.Threading.Tasks;

namespace QuizBuilder.Data.DataProviders {

	public interface IStructureDataProvider {

		Task AddQuizQuestionRelationship( long quizId, long quizItemId );

		Task UpdateGroupQuizItemRelationship( long groupId, string quizItemUId );

		Task DeleteQuizQuestionRelationship( string quizUId, string quizItemUId );

		Task<ImmutableArray<(string, int)>> DeleteQuizRelationships( string quizUId );

		Task<int> RemoveQuizItemRelationships( string quizItemUId );

		Task<int> DeleteQuizQuizItemRelationships( string quizItemUId );

	}
}
