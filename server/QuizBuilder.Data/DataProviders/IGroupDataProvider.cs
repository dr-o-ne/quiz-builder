using System.Collections.Immutable;
using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IGroupDataProvider {

		public Task<long> Add( long quizId, GroupDto dto );

		public Task<int> Update( GroupDto dto );

		public Task<int> Delete( string uid );

		public Task<GroupDto> Get( string uid );

		public Task<ImmutableArray<GroupDto>> GetByQuiz( string uid );
	}

}
