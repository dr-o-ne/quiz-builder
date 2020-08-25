using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Action.Common.Services;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, CommandResult<LoginViewModel>> {

		private readonly SignInManager<UserDto> _signInManager;
		private readonly UserManager<UserDto> _userManager;
		private readonly IJwtTokenFactory _jwtTokenFactory;

		public LoginUserCommandHandler( SignInManager<UserDto> signInManager, UserManager<UserDto> userManager, IJwtTokenFactory jwtTokenFactory ) {
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtTokenFactory = jwtTokenFactory;
		}

		public async Task<CommandResult<LoginViewModel>> HandleAsync( LoginUserCommand command ) {

			UserDto user = await _userManager.FindByEmailAsync( command.Email );
			if( user == null )
				return new CommandResult<LoginViewModel> { IsSuccess = false };

			return await Login( user, command.Password );
		}

		private async Task<CommandResult<LoginViewModel>> Login( UserDto user, string password ) {

			SignInResult signInResult = await _signInManager.PasswordSignInAsync( user, password, false, false );
			if( !signInResult.Succeeded )
				return new CommandResult<LoginViewModel> { IsSuccess = false };

			string token = _jwtTokenFactory.Create( user );

			var payload = new LoginViewModel { Username = user.UserName, Token = token };
			return new CommandResult<LoginViewModel> {
				IsSuccess = true,
				Payload = payload
			};

		}

	}

}
