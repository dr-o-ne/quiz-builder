using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Domain.Queries.QuestionQueries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.QueryHandlers {
	public sealed class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, GetQuestionByIdDto> {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public GetQuestionByIdQueryHandler( IMapper mapper, IGenericRepository<QuestionDto> questionRepository ) {
			_mapper = mapper;
			_questionRepository = questionRepository;
		}

		public async Task<GetQuestionByIdDto> HandleAsync( GetQuestionByIdQuery query ) {
			QuestionDto questionDto = await _questionRepository.GetByUIdAsync( query.UId );
			Question question = _mapper.Map<QuestionDto, Question>( questionDto );
			QuestionViewModel questionViewModel = _mapper.Map<Question, QuestionViewModel>( question );

			return questionViewModel is null
				? null
				: new GetQuestionByIdDto( questionViewModel );
		}
	}
}
