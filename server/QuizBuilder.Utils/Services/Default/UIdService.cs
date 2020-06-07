using QuizBuilder.Utils.Utils;

namespace QuizBuilder.Utils.Services.Default {

	internal sealed class UIdService : IUIdService {

		public string GetUId() => RandomIdGenerator.GetBase62( 10 );

	}

}
