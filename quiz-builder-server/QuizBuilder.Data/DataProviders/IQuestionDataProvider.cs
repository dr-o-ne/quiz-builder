using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IQuestionDataProvider {

		public Task Add( QuizDto dto );

		public Task<List<QuizDto>> GetAll();

		public Task Update( QuizDto dto );

		public Task Delete( string uid );

		public Task Delete( List<string> uids );

	}

}
