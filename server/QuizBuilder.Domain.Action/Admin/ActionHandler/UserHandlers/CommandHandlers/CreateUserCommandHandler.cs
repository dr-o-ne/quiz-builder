using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.UserHandlers.CommandHandlers {

	public sealed class CreateUserCommandHandler : ICommandHandler<SignUpCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly UserManager<UserDto> _userManager;

		public CreateUserCommandHandler( IMapper mapper, UserManager<UserDto> userManager ) {
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<CommandResult> HandleAsync( SignUpCommand command ) {

			UserDto userDto = _mapper.Map<UserDto>( command );

			var result = await _userManager.CreateAsync( userDto, command.Password );

			return CommandResult.Success();
		}

	}

}
