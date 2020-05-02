using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Domain.Queries.QuizQueries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.QueryHandlers {
	public class GetAllQuizzesQueryHandler : IQueryHandler<GetAllQuizzesQuery, GetAllQuizzesDto> {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public GetAllQuizzesQueryHandler( IMapper mapper, IGenericRepository<QuizDto> quizRepository ) {
			_mapper = mapper;
			_quizRepository = quizRepository;
		}

		public async Task<GetAllQuizzesDto> HandleAsync( GetAllQuizzesQuery query ) {
			IEnumerable<QuizDto> quizDtos = await _quizRepository.GetAllAsync();
			IEnumerable<Quiz> quizzes = _mapper.Map<IEnumerable<QuizDto>, IEnumerable<Quiz>>( quizDtos );
			IEnumerable<QuizViewModel> quizViewModels = _mapper.Map<IEnumerable<Quiz>, IEnumerable<QuizViewModel>>( quizzes );

			return new GetAllQuizzesDto( quizViewModels );
		}
	}
}
