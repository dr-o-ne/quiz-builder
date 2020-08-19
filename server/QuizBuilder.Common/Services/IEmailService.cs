using System.Threading.Tasks;

namespace QuizBuilder.Common.Services {

	public interface IEmailService {

		Task<bool> SendEmail( string address, string subject, string message );

	}

}
