using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Queries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers
{
	public class GetQuizByIdQueryHandler : IQueryHandler<GetQuizByIdQuery, GetQuizByIdDto> {

		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public GetQuizByIdQueryHandler( IQuizMapper quizMapper, IGenericRepository<QuizDto> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<GetQuizByIdDto> HandleAsync( GetQuizByIdQuery query ) {

			QuizDto dto = await _quizRepository.GetByIdAsync( query.Id );

			Quiz entity = _quizMapper.Map( dto );
			if( entity == null )
				return null;

			return new GetQuizByIdDto( entity.Id, entity.Name, DateTime.MinValue, DateTime.MinValue );
		}

	}
}