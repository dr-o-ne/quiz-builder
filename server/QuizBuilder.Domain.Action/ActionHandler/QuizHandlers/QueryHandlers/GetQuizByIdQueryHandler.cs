using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Action.ActionResult;
using QuizBuilder.Domain.Action.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.ActionHandler.QuizHandlers.QueryHandlers {
	public sealed class GetQuizByIdQueryHandler : IQueryHandler<GetQuizByIdQuery, QuizQueryResult> {
		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;

		public GetQuizByIdQueryHandler( IMapper mapper, IQuizDataProvider quizDataProvider, IGroupDataProvider groupDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<QuizQueryResult> HandleAsync( GetQuizByIdQuery query ) {
			QuizDto quizDto = await _quizDataProvider.Get( query.UId );
			if( quizDto == null )
				return null;

			Quiz quiz = _mapper.Map<QuizDto, Quiz>( quizDto );
			IEnumerable<GroupDto> groupDtos = await _groupDataProvider.GetByQuiz( quiz.UId );
			IEnumerable<Group> groups = _mapper.Map<IEnumerable<GroupDto>, IEnumerable<Group>>( groupDtos );
			IEnumerable<GroupViewModel> groupViewModels = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>( groups );
			QuizViewModel quizViewModel = _mapper.Map<QuizViewModel>( quiz );

			if( quizViewModel is null ) {
				return null;
			}

			quizViewModel.Groups = groupViewModels;

			return new QuizQueryResult { Quiz = quizViewModel };
		}
	}
}
