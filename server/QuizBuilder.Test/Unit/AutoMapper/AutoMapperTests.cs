using AutoMapper;
using QuizBuilder.Domain.Action.Mapper;
using Xunit;

namespace QuizBuilder.Test.Unit.AutoMapper {

	public sealed class AutoMapperTests {

		[Fact]
		public void ProfileConfiguration_Test() {
			var sut = new MapperConfiguration( cfg => cfg.AddProfile<QuizBuilderProfile>() );
			sut.AssertConfigurationIsValid();
		}
	}
}
