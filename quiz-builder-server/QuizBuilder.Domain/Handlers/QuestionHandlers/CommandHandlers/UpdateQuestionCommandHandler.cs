﻿using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {

	public sealed class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public UpdateQuestionCommandHandler( IMapper mapper, IGenericRepository<QuestionDto> questionRepository ) {
			_mapper = mapper;
			_questionRepository = questionRepository;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuestionCommand command ) {

			QuestionDto currentQuestionDto = await _questionRepository.GetByUIdAsync( command.UId );

			Question question = _mapper.Map<UpdateQuestionCommand, Question>( command );

			question.Id = currentQuestionDto.Id;
			QuestionDto questionDto = _mapper.Map<Question, QuestionDto>( question );
			int rowsAffected = await _questionRepository.UpdateAsync( questionDto );

			return new CommandResult( success: rowsAffected.GreaterThanZero(), message: string.Empty );

		}
	}
}
