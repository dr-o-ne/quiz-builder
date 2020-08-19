using System.Threading.Tasks;

namespace QuizBuilder.Common.Services {

	public interface IEmailService {

		Task SendEmail( string address, string subject, string message );

	}

}
