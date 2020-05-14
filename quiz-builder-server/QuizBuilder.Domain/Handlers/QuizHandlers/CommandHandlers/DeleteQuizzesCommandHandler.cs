﻿using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Actions;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {

	public sealed class DeleteQuizzesCommandHandler : ICommandHandler<DeleteQuizzesCommand, CommandResult> {

		private readonly IQuizDataProvider _quizDataProvider;

		public DeleteQuizzesCommandHandler( IQuizDataProvider quizDataProvider ) {
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizzesCommand command ) {
			await _quizDataProvider.Delete( command.UIds.ToList() );
			return new CommandResult( success: true, message: string.Empty );
		}

	}

}
