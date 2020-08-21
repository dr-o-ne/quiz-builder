using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Common.Services;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, CommandResult> {

		private readonly UserManager<UserDto> _userManager;
		private readonly IEmailService _emailService;

		public ForgotPasswordCommandHandler( UserManager<UserDto> userManager, IEmailService emailService ) {
			_userManager = userManager;
			_emailService = emailService;
		}

		public async Task<CommandResult> HandleAsync( ForgotPasswordCommand command ) {

			UserDto user = await _userManager.FindByEmailAsync( command.Email );
			if( user == null ) {
				// Don't reveal that the user does not exist
				return CommandResult.Success();
			}

			string code = await _userManager.GeneratePasswordResetTokenAsync( user );
			string link = HtmlEncoder.Default.Encode( $"http://localhost:5200/new-password/{code}" ); //TODO:

			bool result = await _emailService.SendEmail( //TODO:
				command.Email,
				"QuizBuilder password forgotten",
				$"You requested a password change. Click on the following link to reset your password: <a href='{link}'>{link}</a>."
			);

			return CommandResult.Success();
		}

	}

}
