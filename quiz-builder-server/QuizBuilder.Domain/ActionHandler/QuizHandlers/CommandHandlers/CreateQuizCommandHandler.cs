using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.ActionResult;
using QuizBuilder.Domain.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.ActionHandler.QuizHandlers.CommandHandlers {

	public sealed class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, QuizCommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IUIdService _uIdService;

		public CreateQuizCommandHandler( IMapper mapper, IQuizDataProvider quizDataProvider, IUIdService uIdService ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_uIdService = uIdService;
		}

		public async Task<QuizCommandResult> HandleAsync( CreateQuizCommand command ) {
			Quiz quiz = _mapper.Map<CreateQuizCommand, Quiz>( command );
			quiz.UId = _uIdService.GetUId();

			QuizDto quizDto = _mapper.Map<Quiz, QuizDto>( quiz );
			await _quizDataProvider.Add( quizDto );

			var addedQuiz = _mapper.Map<QuizDto, Quiz>( quizDto );
			var quizViewModel = _mapper.Map<Quiz, QuizViewModel>( addedQuiz );

			return new QuizCommandResult {
				Success = true,
				Message = string.Empty,
				Data = new QuizQueryResult {Quiz = quizViewModel}
			};
		}
	}
}
