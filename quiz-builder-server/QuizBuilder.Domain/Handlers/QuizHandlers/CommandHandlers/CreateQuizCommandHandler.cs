using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {

	public sealed class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IUIdService _uIdService;

		public CreateQuizCommandHandler( IMapper mapper, IQuizDataProvider quizDataProvider, IUIdService uIdService ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_uIdService = uIdService;
		}

		public async Task<CommandResult> HandleAsync( CreateQuizCommand command ) {
			Quiz quiz = _mapper.Map<CreateQuizCommand, Quiz>( command );
			quiz.UId = _uIdService.GetUId();

			QuizDto quizDto = _mapper.Map<Quiz, QuizDto>( quiz );
			await _quizDataProvider.Add( quizDto );

			return new CommandResult( success: true, message: string.Empty );
		}
	}
}
