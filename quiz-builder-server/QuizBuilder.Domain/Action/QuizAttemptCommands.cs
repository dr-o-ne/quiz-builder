using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.ActionResult;

namespace QuizBuilder.Domain.Action {

	public sealed class CreateQuizAttemptCommand : ICommand<QuizAttemptCommandResult> {
	
		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }
	
	}

}
