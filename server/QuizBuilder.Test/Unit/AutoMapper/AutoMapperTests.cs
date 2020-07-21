using AutoMapper;
using Xunit;

namespace QuizBuilder.Test.Unit.AutoMapper {

	public sealed class AutoMapperTests {

		[Fact]
		public void AdminMapperProfileConfiguration_Test() {
			var sut = new MapperConfiguration( cfg => cfg.AddProfile<Domain.Action.Admin.Map.MapperProfile>() );
			sut.AssertConfigurationIsValid();
		}

		[Fact]
		public void ClientMapperProfileConfiguration_Test() {
			var sut = new MapperConfiguration( cfg => cfg.AddProfile<Domain.Action.Client.Map.MapperProfile>() );
			sut.AssertConfigurationIsValid();
		}
	}
}
