using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Common.Services;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.UserHandlers.CommandHandlers {

	public sealed class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, AuthenticateUserResult> {

		private readonly IMapper _mapper;
		private readonly SignInManager<UserDto> _signInManager;
		private readonly UserManager<UserDto> _userManager;
		private readonly IJwtTokenFactory _jwtTokenFactory;

		public AuthenticateUserCommandHandler( IMapper mapper, SignInManager<UserDto> signInManager, UserManager<UserDto> userManager, IJwtTokenFactory jwtTokenFactory ) {
			_mapper = mapper;
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtTokenFactory = jwtTokenFactory;
		}

		public async Task<AuthenticateUserResult> HandleAsync( AuthenticateUserCommand command ) {

			SignInResult signInResult = await _signInManager.PasswordSignInAsync( command.Email, command.Password, false, false );
			if( !signInResult.Succeeded )
				return new AuthenticateUserResult{ IsSuccess = false };

			UserDto user = await _userManager.FindByEmailAsync( command.Email );

			string token = _jwtTokenFactory.Create( user );

			return new AuthenticateUserResult {
				IsSuccess = true,
				Username = user.UserName,
				Token = token
			};

		}

	}

}
