using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.QueryHandlers {
	public class GetAllQuestionQueryHandler : IQueryHandler<GetAllQuestionQuery, GetAllQuestionsDto> {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public GetAllQuestionQueryHandler( IMapper mapper, IGenericRepository<QuestionDto> questionRepository ) {
			_mapper = mapper;
			_questionRepository = questionRepository;
		}

		public async Task<GetAllQuestionsDto> HandleAsync( GetAllQuestionQuery query ) {
			IEnumerable<QuestionDto> questionDtos = await _questionRepository.GetAllAsync();
			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );
			IEnumerable<QuestionViewModel> questionViewModels = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>( questions );

			return new GetAllQuestionsDto( questionViewModels );
		}
	}
}
