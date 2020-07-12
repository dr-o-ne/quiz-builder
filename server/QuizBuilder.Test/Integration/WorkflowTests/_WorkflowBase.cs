using System;
using QuizBuilder.Api;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Collection( "DB" )]
	public abstract class _WorkflowBase : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		protected readonly ApiClient _apiClient;
		protected readonly TestDatabaseWrapper _db;

		protected _WorkflowBase( TestApplicationFactory<Startup> factory ) {
			_apiClient = new ApiClient( factory.CreateClient() );
			_db = factory.GetTestDatabaseWrapper();
			_db.Cleanup();
		}

		public void Dispose() => _db.Cleanup();

	}
}
