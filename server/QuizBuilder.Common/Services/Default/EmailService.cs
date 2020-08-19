using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Mailgun;
using Microsoft.Extensions.Configuration;

namespace QuizBuilder.Common.Services.Default {

	internal sealed class EmailService : IEmailService {

		private readonly IConfiguration _config;

		public EmailService( IConfiguration config ) {
			_config = config;

			string key = _config.GetValue<string>( "MAILGUN_KEY" );
			string domain = _config.GetValue<string>( "MAILGUN_DOMAIN" );

			Email.DefaultSender = new MailgunSender( domain, key );
		}

		public async Task<bool> SendEmail( string address, string subject, string message ) {

			string from = _config.GetValue<string>( "MAILGUN_FROM" );

			IFluentEmail email = Email
				.From( from )
				.To( address )
				.Subject( subject )
				.Body( message, isHtml: true );

			SendResponse result = await email.SendAsync();

			return result.Successful;
		}

	}

}
