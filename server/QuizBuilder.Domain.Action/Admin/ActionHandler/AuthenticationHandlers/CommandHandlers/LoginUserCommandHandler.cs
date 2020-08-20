﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Common.Services;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, CommandResult<LoginInfo>> {

		private readonly SignInManager<UserDto> _signInManager;
		private readonly UserManager<UserDto> _userManager;
		private readonly IJwtTokenFactory _jwtTokenFactory;

		public LoginUserCommandHandler( SignInManager<UserDto> signInManager, UserManager<UserDto> userManager, IJwtTokenFactory jwtTokenFactory ) {
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtTokenFactory = jwtTokenFactory;
		}

		public async Task<CommandResult<LoginInfo>> HandleAsync( LoginUserCommand command ) {
			return await Login( command.Email, command.Password );
		}

		private async Task<CommandResult<LoginInfo>> Login( string email, string password ) {

			SignInResult signInResult = await _signInManager.PasswordSignInAsync( email, password, false, false );
			if( !signInResult.Succeeded )
				return new CommandResult<LoginInfo> { IsSuccess = false };

			UserDto user = await _userManager.FindByEmailAsync( email );

			string token = _jwtTokenFactory.Create( user );

			var payload = new LoginInfo { Username = user.UserName, Token = token };
			return new CommandResult<LoginInfo> {
				IsSuccess = true,
				Payload = payload
			};

		}

	}

}
