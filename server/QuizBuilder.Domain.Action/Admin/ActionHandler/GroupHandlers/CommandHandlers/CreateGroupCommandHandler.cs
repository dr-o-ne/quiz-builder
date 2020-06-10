using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Structure;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, GroupCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public CreateGroupCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			IQuizDataProvider quizDataProvider,
			IGroupDataProvider groupDataProvider,
			IStructureDataProvider structureDataProvider ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_groupDataProvider = groupDataProvider;
			_structureDataProvider = structureDataProvider;
		}

		public async Task<GroupCommandResult> HandleAsync( CreateGroupCommand command ) {
			Group model = _mapper.Map<CreateGroupCommand, Group>( command );
			model.UId = _uIdService.GetUId();

			if( !model.IsValid() )
				return new GroupCommandResult { Success = false };

			QuizDto quizDto = await _quizDataProvider.Get( command.QuizUId );
			if( quizDto == null )
				return new GroupCommandResult { Success = false };

			GroupDto dto = _mapper.Map<Group, GroupDto>( model );

			long id = await _groupDataProvider.Add( dto );
			await _structureDataProvider.AddQuizQuestionRelationship( quizDto.Id, id );

			var addedGroup = _mapper.Map<GroupDto, Group>( dto );
			var groupViewModel = _mapper.Map<Group, GroupViewModel>( addedGroup );

			return new GroupCommandResult { Success = true, Group = groupViewModel };
		}

	}
}
