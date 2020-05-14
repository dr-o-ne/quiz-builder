using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders.Default {

	internal sealed class QuestionDataProvider : IQuestionDataProvider {

		public Task Add( QuizDto dto ) {
			throw new NotImplementedException();
		}

		public Task<List<QuizDto>> GetAll() {
			throw new NotImplementedException();
		}

		public Task Update( QuizDto dto ) {
			throw new NotImplementedException();
		}

		public Task Delete( string uid ) {
			throw new NotImplementedException();
		}

		public Task Delete( List<string> uids ) {
			throw new NotImplementedException();
		}
	}
}
