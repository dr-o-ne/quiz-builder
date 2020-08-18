using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders {

	public interface IOrganizationDataProvider {

		public Task<OrganizationDto> Add( OrganizationDto dto );

		public Task Delete( long id );

	}

}
