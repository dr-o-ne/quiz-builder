using System.Collections.Immutable;
using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IQuizDataProvider {

		public Task<ImmutableArray<QuizDto>> GetAll( long orgId, string userId );

		public Task<QuizDto> Get( long orgId, string userId, long id );

		public Task<QuizDto> Get( long orgId, string userId, string uid );

		public Task<long> Add( long orgId, string userId, QuizDto dto );

		public Task Update( long orgId, string userId, QuizDto dto );

		public Task Delete( long orgId, string userId, string uid );

	}

}
