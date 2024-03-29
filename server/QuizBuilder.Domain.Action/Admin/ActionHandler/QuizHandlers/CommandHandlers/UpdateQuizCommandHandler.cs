using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuizHandlers.CommandHandlers {

	public class UpdateQuizCommandHandler : ICommandHandler<UpdateQuizCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;

		public UpdateQuizCommandHandler( IMapper mapper, IQuizDataProvider quizDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuizCommand command ) {
			QuizDto quizDto = await _quizDataProvider.Get( command.OrgId, command.UserId, command.UId );
			if( quizDto == null )
				return CommandResult.Fail();

			Quiz quizModel = _mapper.Map<Quiz>( command );
			QuizDto newQuizDto = _mapper.Map<QuizDto>( quizModel );
			await _quizDataProvider.Update( command.OrgId, command.UserId, newQuizDto );

			return CommandResult.Success();
		}
	}
}
