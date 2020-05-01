using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Domain.Queries;
using QuizBuilder.Domain.Queries.QuizQueries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.QueryHandlers {
	public class GetQuizByIdQueryHandler : IQueryHandler<GetQuizByIdQuery, GetQuizByIdDto> {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public GetQuizByIdQueryHandler( IMapper mapper, IGenericRepository<QuizDto> quizRepository ) {
			_mapper = mapper;
			_quizRepository = quizRepository;
		}

		public async Task<GetQuizByIdDto> HandleAsync( GetQuizByIdQuery query ) {
			QuizDto quizDto = await _quizRepository.GetByIdAsync( query.Id );
			Quiz quiz = _mapper.Map<QuizDto, Quiz>( quizDto );
			QuizViewModel quizViewModel = _mapper.Map<Quiz, QuizViewModel>( quiz );

			return quizViewModel is null
				? null
				: new GetQuizByIdDto( quizViewModel );
		}
	}
}
