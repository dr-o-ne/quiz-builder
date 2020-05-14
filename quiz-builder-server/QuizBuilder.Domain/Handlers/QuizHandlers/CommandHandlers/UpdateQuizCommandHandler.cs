using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Extensions;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {

	public class UpdateQuizCommandHandler : ICommandHandler<UpdateQuizCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;

		public UpdateQuizCommandHandler( IMapper mapper, IQuizDataProvider quizDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuizCommand command ) {
			QuizDto quizDto = await _quizDataProvider.Get( command.UId );
			if( quizDto == null )
				return new CommandResult( success: false, message: string.Empty );
			Quiz currentQuiz = _mapper.Map<QuizDto, Quiz>( quizDto );
			Quiz updatedQuiz = _mapper.Map<UpdateQuizCommand, Quiz>( command );
			Quiz mergedQuiz = _mapper.Merge( updatedQuiz, currentQuiz );
			QuizDto mergedQuizDto = _mapper.Map<Quiz, QuizDto>( mergedQuiz );
			await _quizDataProvider.Update( mergedQuizDto );

			return new CommandResult( success: true, message: string.Empty );
		}
	}
}
