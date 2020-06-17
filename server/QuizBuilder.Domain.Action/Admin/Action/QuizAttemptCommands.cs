using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class StartQuizAttemptCommand : ICommand<StartQuizAttemptCommandResult> {
	
		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }
	
	}

}
