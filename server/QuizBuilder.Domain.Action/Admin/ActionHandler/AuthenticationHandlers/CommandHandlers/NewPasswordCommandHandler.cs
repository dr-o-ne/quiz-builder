using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Common.Services;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class NewPasswordCommandHandler : ICommandHandler<NewPasswordCommand, CommandResult<LoginInfo>> {

		private readonly IJwtTokenFactory _jwtTokenFactory;
		private readonly SignInManager<UserDto> _signInManager;
		private readonly UserManager<UserDto> _userManager;

		public NewPasswordCommandHandler(
			IJwtTokenFactory jwtTokenFactory,
			SignInManager<UserDto> signInManager,
			UserManager<UserDto> userManager
		) {
			_jwtTokenFactory = jwtTokenFactory;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<CommandResult<LoginInfo>> HandleAsync( NewPasswordCommand command ) {

			var user = await _userManager.FindByEmailAsync( command.Email );
			if( user == null )
				return new CommandResult<LoginInfo> { IsSuccess = false };

			var result = await _userManager.ResetPasswordAsync( user, command.Code, command.Password );
			if( !result.Succeeded )
				return new CommandResult<LoginInfo> { IsSuccess = false };

			return await Login( user, command.Password );
		}

		private async Task<CommandResult<LoginInfo>> Login( UserDto user, string password ) {

			SignInResult signInResult = await _signInManager.PasswordSignInAsync( user, password, false, false );
			if( !signInResult.Succeeded )
				return new CommandResult<LoginInfo> { IsSuccess = false };

			string token = _jwtTokenFactory.Create( user );

			var payload = new LoginInfo { Username = user.UserName, Token = token };
			return new CommandResult<LoginInfo> {
				IsSuccess = true,
				Payload = payload
			};

		}

	}

}
