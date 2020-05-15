using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {
	public interface IGroupDataProvider {

		public Task<long> Add( GroupDto dto );

		public Task Update( GroupDto dto );

		public Task Delete( string uid );

	}
}
