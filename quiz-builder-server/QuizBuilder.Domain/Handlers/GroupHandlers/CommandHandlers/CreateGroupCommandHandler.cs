using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Model.Default.Structure;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Handlers.GroupHandlers.CommandHandlers {

	public sealed class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, CreateGroupCommandResult> {

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

		public async Task<CreateGroupCommandResult> HandleAsync( CreateGroupCommand command ) {
			Group model = _mapper.Map<CreateGroupCommand, Group>( command );
			model.UId = _uIdService.GetUId();

			if( !model.IsValid() )
				return new CreateGroupCommandResult( success: false, message: string.Empty );

			QuizDto quizDto = await _quizDataProvider.Get( command.QuizUId );
			if( quizDto == null )
				return new CreateGroupCommandResult( success: false, message: string.Empty );

			GroupDto dto = _mapper.Map<Group, GroupDto>( model );

			long id = await _groupDataProvider.Add( dto );
			await _structureDataProvider.AddQuizQuestionRelationship( quizDto.Id, id );

			return new CreateGroupCommandResult( success: true, message: string.Empty, groupUId: model.UId );
		}

	}
}
