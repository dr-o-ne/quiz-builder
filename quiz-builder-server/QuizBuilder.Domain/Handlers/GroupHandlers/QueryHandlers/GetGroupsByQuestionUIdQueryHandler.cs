using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Handlers.GroupHandlers.QueryHandlers {

	public sealed class GetGroupsByQuestionUIdQueryHandler : IQueryHandler<GetAllGroupsByQuizQuery, ImmutableArray<GroupViewDto>> {

		public Task<ImmutableArray<GroupViewDto>> HandleAsync( GetAllGroupsByQuizQuery query ) {
			throw new NotImplementedException();
		}

	}
}
