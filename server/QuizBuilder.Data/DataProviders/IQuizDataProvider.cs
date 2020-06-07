using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IQuizDataProvider {

		public Task<IEnumerable<QuizDto>> GetAll();

		public Task<QuizDto> Get( long id );

		public Task<QuizDto> Get( string uid );

		public Task<long> Add( QuizDto dto );

		public Task Update( QuizDto dto );

		public Task Delete( string uid );

		public Task Delete( List<string> uids );

	}

}
