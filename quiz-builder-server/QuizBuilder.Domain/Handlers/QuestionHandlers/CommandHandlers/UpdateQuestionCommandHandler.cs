﻿using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Extensions;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {
	public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public UpdateQuestionCommandHandler( IMapper mapper, IGenericRepository<QuestionDto> questionRepository ) {
			_mapper = mapper;
			_questionRepository = questionRepository;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuestionCommand command ) {
			QuestionDto questionDto = await _questionRepository.GetByIdAsync( command.Id );
			Question originalQuestion = _mapper.Map<QuestionDto, Question>( questionDto );
			Question modifiedQuestion = _mapper.Map<UpdateQuestionCommand, Question>( command );
			Question mergedQuestion = _mapper.Merge( modifiedQuestion, originalQuestion );
			QuestionDto mergedQuestionDto = _mapper.Map<Question, QuestionDto>( mergedQuestion );
			int rowsAffected = await _questionRepository.UpdateAsync( mergedQuestionDto );

			return new CommandResult( success: rowsAffected.GreaterThanZero(), message: string.Empty );
		}
	}
}
