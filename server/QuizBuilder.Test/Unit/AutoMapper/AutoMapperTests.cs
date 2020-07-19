using AutoMapper;
using QuizBuilder.Domain.Action.Client.Map;
using QuizBuilder.Domain.Action.Mapper;
using Xunit;

namespace QuizBuilder.Test.Unit.AutoMapper {

	public sealed class AutoMapperTests {

		[Fact]
		public void AdminMapperProfileConfiguration_Test() {
			var sut = new MapperConfiguration( cfg => cfg.AddProfile<QuizBuilderProfile>() );
			sut.AssertConfigurationIsValid();
		}

		[Fact]
		public void ClientMapperProfileConfiguration_Test() {
			var sut = new MapperConfiguration( cfg => cfg.AddProfile<MapperProfile>() );
			sut.AssertConfigurationIsValid();
		}
	}
}
