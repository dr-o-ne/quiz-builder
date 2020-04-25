using System.Threading.Tasks;
using QuizBuilder.Common.Types;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Common.Handlers.Default {

	public sealed class GetQuestionByIdDto {
	
		public long Id { get; }
		public string Name { get; }
	
		public GetQuestionByIdDto( long id, string name ) {
			Id = id;
			Name = name;
		}
	}
	
	public sealed class GetQuestionByIdQuery : IQuery<GetQuestionByIdDto> {
		public long Id { get; set; }
	}
	
	public sealed class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, GetQuestionByIdDto> {
	
		private readonly IQuestionMapper _questionMapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public GetQuestionByIdQueryHandler( IQuestionMapper questionMapper, IGenericRepository<QuestionDto> questionRepository ) {
			_questionMapper = questionMapper;
			_questionRepository = questionRepository;
		}

		public async Task<GetQuestionByIdDto> HandleAsync( GetQuestionByIdQuery query ) {
	
			QuestionDto dto = await _questionRepository.GetByIdAsync( query.Id );
	
			Question entity = _questionMapper.Map( dto );
			if( entity == null )
				return null;
	
			return new GetQuestionByIdDto( entity.Id, entity.Name );
		}
	
	}
}
