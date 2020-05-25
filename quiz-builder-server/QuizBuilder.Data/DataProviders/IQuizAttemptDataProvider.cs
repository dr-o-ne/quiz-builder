using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IQuizAttemptDataProvider {

		public Task<AttemptDto> Get( string uid );

		public Task<long> Add( AttemptDto dto );

	}

}
