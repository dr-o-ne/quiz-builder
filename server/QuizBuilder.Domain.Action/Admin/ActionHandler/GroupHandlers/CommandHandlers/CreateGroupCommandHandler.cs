using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Utils.Services;
using Group = QuizBuilder.Domain.Model.Default.Structure.Group;

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

			var quizDto = await _quizDataProvider.Get( command.QuizUId );
			if( quizDto == null )
				return new GroupCommandResult { IsSuccess = false };

			ImmutableArray<GroupDto> groupDtos = await _groupDataProvider.GetByQuiz( command.QuizUId );

			var model = new Group {
				UId = _uIdService.GetUId(),
				Name = GetNextGroupName(groupDtos)
			};

			if( !model.IsValid() )
				return new GroupCommandResult { IsSuccess = false };

			var dto = _mapper.Map<GroupDto>( model );

			long id = await _groupDataProvider.Add( quizDto.Id, dto );
			await _structureDataProvider.AddQuizQuestionRelationship( quizDto.Id, id );

			var addedGroup = _mapper.Map<Group>( dto );
			var groupViewModel = _mapper.Map<GroupViewModel>( addedGroup );

			return new GroupCommandResult { IsSuccess = true, Group = groupViewModel };
		}

		private string GetNextGroupName( ImmutableArray<GroupDto> dtos ) {
			Regex regExp = new Regex( @"^Group \d+$" );

			int defaultNumber = dtos.Where( x => regExp.IsMatch( x.Name ) )
				.Select( x => x.Name.Substring( 6 ) )
				.Select( x => Convert.ToInt32( x ) )
				.Max();

			return $"Group {Math.Max( defaultNumber, dtos.Length ) + 1}";
		}

	}
}
