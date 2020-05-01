﻿using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {
	public class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionCommand, CommandResult> {

		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public DeleteQuestionCommandHandler( IGenericRepository<QuestionDto> questionRepository ) {
			_questionRepository = questionRepository;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuestionCommand command ) {
			int rowsAffected = await _questionRepository.DeleteAsync( command.Id );
			return new CommandResult( success: rowsAffected > 0, message: string.Empty );
		}
	}
}
