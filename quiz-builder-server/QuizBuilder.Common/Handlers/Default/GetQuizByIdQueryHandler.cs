using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Types;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Common.Handlers.Default {

	public class GetQuizByIdDto {

		public long Id { get; }
		public string Name { get; }
		public DateTime CreatedDate { get; }
		public DateTime UpdatedDate { get; }

		public GetQuizByIdDto( long id, string name, DateTime createdDate, DateTime updatedDate ) {
			Id = id;
			Name = name;
			CreatedDate = createdDate;
			UpdatedDate = updatedDate;
		}
	}

	public class GetQuizByIdQuery : IQuery<GetQuizByIdDto> {
		public long Id { get; set; }
	}

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
