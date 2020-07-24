using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuizHandlers.CommandHandlers {

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
				return CommandResult.Fail();

			Quiz currentQuiz = _mapper.Map<Quiz>( quizDto );
			Quiz updatedQuiz = _mapper.Map<Quiz>( command );
			Quiz mergedQuiz = _mapper.Merge( updatedQuiz, currentQuiz );
			QuizDto mergedQuizDto = _mapper.Map<QuizDto>( mergedQuiz );
			await _quizDataProvider.Update( mergedQuizDto );

			return new CommandResult( isSuccess: true, message: string.Empty );
		}
	}
}
