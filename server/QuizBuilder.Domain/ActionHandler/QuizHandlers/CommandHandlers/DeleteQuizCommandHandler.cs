using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Model;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.ActionHandler.QuizHandlers.CommandHandlers {

	public sealed class DeleteQuizCommandHandler : ICommandHandler<DeleteQuizCommand, CommandResult> {

		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public DeleteQuizCommandHandler(
			IQuizDataProvider quizDataProvider,
			IGroupDataProvider groupDataProvider,
			IQuestionDataProvider questionDataProvider,
			IStructureDataProvider structureDataProvider ) {
			_quizDataProvider = quizDataProvider;
			_groupDataProvider = groupDataProvider;
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizCommand command ) {

			List<(string uid, int typeId)> result = (await _structureDataProvider.DeleteQuizRelationships( command.UId )).ToList();
			var questionUids = result
				.Where( x => x.typeId == (int)Enums.QuizItemType.Question )
				.Select( x => x.uid );

			var groupUIds = result
				.Where( x => x.typeId == (int)Enums.QuizItemType.Group )
				.Select( x => x.uid );

			foreach( string uid in questionUids ) {
				await _questionDataProvider.Delete( uid );
			}
			foreach( string uid in groupUIds ) {
				await _groupDataProvider.Delete( uid );
			}

			await _quizDataProvider.Delete( command.UId );

			return new CommandResult( success: true, message: string.Empty );
		}
	}
}
