using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Model;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Utils.Extensions;
using QuizBuilder.Utils.Services;
using QuestionDto = QuizBuilder.Repository.Dto.QuestionDto;
using QuizItemDto = QuizBuilder.Repository.Dto.QuizItemDto;
using QuizQuizItemDto = QuizBuilder.Repository.Dto.QuizQuizItemDto;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {

	public sealed class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IGenericRepository<QuizQuizItemDto> _quizQuizItemRepository;
		private readonly IGenericRepository<QuizItemDto> _quizItemRepository;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public CreateQuestionCommandHandler( IMapper mapper, IUIdService uIdService, IQuizDataProvider quizDataProvider, IGenericRepository<QuizQuizItemDto> quizQuizItemRepository, IGenericRepository<QuizItemDto> quizItemRepository, IGenericRepository<QuestionDto> questionRepository ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_quizQuizItemRepository = quizQuizItemRepository;
			_quizItemRepository = quizItemRepository;
			_questionRepository = questionRepository;
		}

		public async Task<CommandResult> HandleAsync( CreateQuestionCommand command ) {

			Question question = _mapper.Map<CreateQuestionCommand, Question>( command );
			question.UId = _uIdService.GetUId();

			if(!question.IsValid())
				return new CommandResult( success: false, message: string.Empty );

			QuestionDto questionDto = _mapper.Map<Question, QuestionDto>( question );

			QuizDto quizDto = await _quizDataProvider.Get( command.QuizUId );

			int rowsAffected = 0;
			if( string.IsNullOrWhiteSpace( command.GroupUId ) ) {

				rowsAffected = await _questionRepository.AddAsync( questionDto );
				long questionId = ( await _questionRepository.GetByUIdAsync( question.UId ) ).Id;

				await _quizItemRepository.AddAsync( new QuizItemDto {
					UId = question.UId,
					ParentQuizItemId = null,
					QuestionId = questionId,
					QuizItemTypeId = (int)Enums.QuizItemType.Question
				} );
				long quizItemId = ( await _quizItemRepository.GetByUIdAsync( question.UId ) ).Id;

				await _quizQuizItemRepository.AddAsync( new QuizQuizItemDto {QuizItemId = quizItemId, QuizId = quizDto.Id} );


			} else {

			}


			return new CommandResult( success: rowsAffected.GreaterThanZero(), message: string.Empty );
		}
	}
}
