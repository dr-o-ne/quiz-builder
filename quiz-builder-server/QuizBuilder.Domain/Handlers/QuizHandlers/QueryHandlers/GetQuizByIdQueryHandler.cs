using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.QueryHandlers {

	public sealed class GetQuizByIdQueryHandler : IQueryHandler<GetQuizByIdQuery, GetQuizByIdDto> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;

		public GetQuizByIdQueryHandler( IMapper mapper, IQuizDataProvider quizDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<GetQuizByIdDto> HandleAsync( GetQuizByIdQuery query ) {
			QuizDto quizDto = await _quizDataProvider.Get( query.UId );
			Quiz quiz = _mapper.Map<QuizDto, Quiz>( quizDto );
			QuizViewModel quizViewModel = _mapper.Map<Quiz, QuizViewModel>( quiz );

			return quizViewModel is null
				? null
				: new GetQuizByIdDto{ Quiz = quizViewModel };
		}
	}
}
