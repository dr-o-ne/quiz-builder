using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.QueryHandlers {

	public sealed class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, GetQuestionByIdDto> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public GetQuestionByIdQueryHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<GetQuestionByIdDto> HandleAsync( GetQuestionByIdQuery query ) {
			QuestionDto questionDto = await _questionDataProvider.Get( query.UId );
			Question question = _mapper.Map<QuestionDto, Question>( questionDto );
			QuestionViewModel questionViewModel = _mapper.Map<Question, QuestionViewModel>( question );

			return questionViewModel is null
				? null
				: new GetQuestionByIdDto {Question = questionViewModel};
		}
	}
}
