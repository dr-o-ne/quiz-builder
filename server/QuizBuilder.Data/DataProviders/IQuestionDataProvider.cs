using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IQuestionDataProvider {

		public Task<IEnumerable<QuestionDto>> GetByQuiz( string uid );

		public Task<IEnumerable<QuestionDto>> GetByGroup( string uid );

		public Task<QuestionDto> Get( string uid );

		public Task<(long, long)> Add( long groupId, QuestionDto dto );

		public Task Update( QuestionDto dto );

		public Task Delete( string uid );

	}

}
