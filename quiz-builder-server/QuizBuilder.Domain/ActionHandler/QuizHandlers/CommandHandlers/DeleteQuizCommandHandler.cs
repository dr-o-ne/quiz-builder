using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Model;

namespace QuizBuilder.Domain.ActionHandler.QuizHandlers.CommandHandlers {

	public sealed class DeleteQuizCommandHandler : ICommandHandler<DeleteQuizCommand, CommandResult> {

		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public DeleteQuizCommandHandler(
			IQuizDataProvider quizDataProvider,
			IQuestionDataProvider questionDataProvider,
			IStructureDataProvider structureDataProvider ) {
			_quizDataProvider = quizDataProvider;
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizCommand command ) {

			IEnumerable<(string uid, int typeId)> result = await _structureDataProvider.DeleteQuizRelationships( command.UId );
			var questionUids = result
				.Where( x => x.typeId == (int)Enums.QuizItemType.Question )
				.Select( x => x.uid );

			foreach( string uid in questionUids ) {
				await _questionDataProvider.Delete( uid );
			}
			//TODO: delete groups

			await _quizDataProvider.Delete( command.UId );

			return new CommandResult( success: true, message: string.Empty );
		}
	}
}
