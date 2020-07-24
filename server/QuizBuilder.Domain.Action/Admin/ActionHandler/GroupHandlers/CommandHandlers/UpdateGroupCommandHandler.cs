using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IGroupDataProvider _groupDataProvider;

		public UpdateGroupCommandHandler( IMapper mapper, IGroupDataProvider groupDataProvider ) {
			_mapper = mapper;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateGroupCommand command ) {

			GroupDto groupDto = await _groupDataProvider.Get( command.UId );
			if( groupDto == null )
				return CommandResult.Fail();

			Group model = _mapper.Map<Group>( command );
			if( !model.IsValid() ) 
				return CommandResult.Fail();




			throw new System.NotImplementedException();

		}
	}
}
