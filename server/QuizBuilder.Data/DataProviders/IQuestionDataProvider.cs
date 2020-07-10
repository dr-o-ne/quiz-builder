using System.Collections.Immutable;
using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IQuestionDataProvider {

		public Task<ImmutableArray<QuestionDto>> GetByQuiz( string uid );

		public Task<ImmutableArray<QuestionDto>> GetByGroup( string uid );

		public Task<QuestionDto> Get( string uid );

		public Task<(long, long)> Add( long groupId, QuestionDto dto );

		public Task Update( QuestionDto dto );

		public Task Delete( string uid );

	}

}
