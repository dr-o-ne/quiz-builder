﻿using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action;

namespace QuizBuilder.Domain.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionCommand, CommandResult> {

		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public DeleteQuestionCommandHandler( IQuestionDataProvider questionDataProvider, IStructureDataProvider structureDataProvider ) {
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuestionCommand command ) {

			await _structureDataProvider.DeleteQuizQuestionRelationship( command.QuizUId, command.UId );
			await _questionDataProvider.Delete( command.UId );

			return new CommandResult( success: true, message: string.Empty );
		}
	}
}