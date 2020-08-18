using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Common.Services;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, CommandResult<LoginInfo>> {

		private readonly IMapper _mapper;
		private readonly SignInManager<UserDto> _signInManager;
		private readonly UserManager<UserDto> _userManager;
		private readonly IJwtTokenFactory _jwtTokenFactory;

		public LoginUserCommandHandler( IMapper mapper, SignInManager<UserDto> signInManager, UserManager<UserDto> userManager, IJwtTokenFactory jwtTokenFactory ) {
			_mapper = mapper;
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtTokenFactory = jwtTokenFactory;
		}

		public async Task<CommandResult<LoginInfo>> HandleAsync( LoginUserCommand command ) {

			SignInResult signInResult = await _signInManager.PasswordSignInAsync( command.Email, command.Password, false, false );
			if( !signInResult.Succeeded )
				return new CommandResult<LoginInfo> { IsSuccess = false };

			UserDto user = await _userManager.FindByEmailAsync( command.Email );

			string token = _jwtTokenFactory.Create( user );

			var payload = new LoginInfo { Username = user.UserName, Token = token };
			return new CommandResult<LoginInfo> {
				IsSuccess = true,
				Payload = payload
			};

		}

	}

}
