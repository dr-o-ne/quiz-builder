using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Queries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers
{
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