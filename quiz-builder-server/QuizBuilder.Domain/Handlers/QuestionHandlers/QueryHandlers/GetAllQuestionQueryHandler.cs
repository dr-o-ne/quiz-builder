using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Queries.QuestionQueries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.QueryHandlers {
	public class GetAllQuestionQueryHandler : IQueryHandler<GetAllQuestionQuery, GetAllQuestionDto> {
		private readonly IQuestionMapper _questionMapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public GetAllQuestionQueryHandler( IQuestionMapper questionMapper, IGenericRepository<QuestionDto> questionRepository ) {
			_questionMapper = questionMapper;
			_questionRepository = questionRepository;
		}

		public async Task<GetAllQuestionDto> HandleAsync( GetAllQuestionQuery query ) {
			IEnumerable<QuestionDto> entities = await _questionRepository.GetAllAsync();
			IEnumerable<Question> dtos = entities.Select( _questionMapper.Map );

			return new GetAllQuestionDto( dtos );
		}
	}
}
