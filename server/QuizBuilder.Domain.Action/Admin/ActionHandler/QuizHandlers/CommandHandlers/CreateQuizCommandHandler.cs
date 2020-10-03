using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Common.Services;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default;
using static QuizBuilder.Domain.Model.Default.Enums.PageSettings;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuizHandlers.CommandHandlers {

	public sealed class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CommandResult<QuizViewModel>> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IUIdService _uIdService;

		public CreateQuizCommandHandler( IMapper mapper, IQuizDataProvider quizDataProvider, IUIdService uIdService ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_uIdService = uIdService;
		}

		public async Task<CommandResult<QuizViewModel>> HandleAsync( CreateQuizCommand command ) {

			var quiz = new Quiz {
				UId = _uIdService.GetUId(),
				Name = command.Name,
				IsPrevButtonEnabled = true,
				PageSettings = PagePerGroup,
				QuestionsPerPage = 5,
				IsStartPageEnabled = true,
				HeaderColor = "#1a202e",
				BackgroundColor = "#fff",
				SideColor = "#f5f5f8",
				FooterColor = "#fff",
			};

			QuizDto quizDto = _mapper.Map<QuizDto>( quiz );
			await _quizDataProvider.Add( command.OrgId, command.UserId, quizDto );

			var quizViewModel = _mapper.Map<QuizViewModel>( quiz );

			return new CommandResult<QuizViewModel> {
				IsSuccess = true,
				Message = string.Empty,
				Payload = quizViewModel
			};

		}
	}
}
