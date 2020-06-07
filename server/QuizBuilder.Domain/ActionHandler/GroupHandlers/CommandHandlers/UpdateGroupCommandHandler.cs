using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Model.Default.Structure;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.ActionHandler.GroupHandlers.CommandHandlers {
	public sealed class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand, CommandResult> {
		private readonly IMapper _mapper;
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public UpdateGroupCommandHandler(
			IMapper mapper,
			IGroupDataProvider groupDataProvider,
			IStructureDataProvider structureDataProvider ) {
			_mapper = mapper;
			_groupDataProvider = groupDataProvider;
			_structureDataProvider = structureDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateGroupCommand command ) {
			GroupDto currentGroupDto = await _groupDataProvider.Get( command.UId );
			Group group = _mapper.Map<UpdateGroupCommand, Group>( command );
			group.Id = currentGroupDto.Id;
			GroupDto groupDto = _mapper.Map<Group, GroupDto>( group );
			int rowsAffected  = await _groupDataProvider.Update( groupDto );

			return new CommandResult( success: rowsAffected > 0, message: string.Empty );
		}
	}
}
