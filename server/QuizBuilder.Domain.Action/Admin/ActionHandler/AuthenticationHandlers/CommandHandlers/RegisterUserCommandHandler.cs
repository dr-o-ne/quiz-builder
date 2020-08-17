using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly UserManager<UserDto> _userManager;

		public RegisterUserCommandHandler( IMapper mapper, UserManager<UserDto> userManager ) {
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<CommandResult> HandleAsync( RegisterUserCommand userCommand ) {

			UserDto userDto = _mapper.Map<UserDto>( userCommand );
			if( string.IsNullOrEmpty( userDto.UserName ) )
				userDto.UserName = userDto.Email;

			IdentityResult result = await _userManager.CreateAsync( userDto, userCommand.Password );
			if( !result.Succeeded ) {
				return CommandResult.Fail();
			}

			return CommandResult.Success();
		}

	}

}
