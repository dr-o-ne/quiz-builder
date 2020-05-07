using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Queries.QuestionQueries;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.QueryHandlers {

	public sealed class GetQuestionsByQuizIdQueryHandler : IQueryHandler<GetQuestionsByQuizIdQuery, GetAllQuestionsDto> {

		private readonly IMapper _mapper;
		private readonly IQuestionRepository _questionRepository;

		public GetQuestionsByQuizIdQueryHandler( IMapper mapper, IQuestionRepository questionRepository ) {
			_mapper = mapper;
			_questionRepository = questionRepository;
		}

		public Task<GetAllQuestionsDto> HandleAsync( GetQuestionsByQuizIdQuery query ) {
			throw new NotImplementedException();
		}

	}
}
