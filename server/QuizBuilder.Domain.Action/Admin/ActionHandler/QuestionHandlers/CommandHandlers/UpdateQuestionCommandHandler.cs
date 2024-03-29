﻿using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public UpdateQuestionCommandHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuestionCommand command ) {

			QuestionDto currentQuestionDto = await _questionDataProvider.Get( command.UId );

			Question question = _mapper.Map<Question>( command );

			if( !question.IsValid() )
				return CommandResult.Fail();

			question.Id = currentQuestionDto.Id;
			QuestionDto questionDto = _mapper.Map<QuestionDto>( question );
			await _questionDataProvider.Update( questionDto );

			return CommandResult.Success();
		}
	}
}
