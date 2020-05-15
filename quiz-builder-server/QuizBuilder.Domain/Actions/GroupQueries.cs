using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Actions {

	public sealed class GetAllGroupsByQuizQuery : IQuery<ImmutableArray<GroupViewDto>> {

		[Required]
		[JsonPropertyName( "Id" )]
		public string QuizUId { get; set; }

	}

}
