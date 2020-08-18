using QuizBuilder.Common.Utils;

namespace QuizBuilder.Common.Services.Default {

	internal sealed class UIdService : IUIdService {

		public string GetUId() => RandomIdGenerator.GetBase62( 10 );

	}

}
