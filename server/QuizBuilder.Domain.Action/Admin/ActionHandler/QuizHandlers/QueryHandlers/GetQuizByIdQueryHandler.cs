using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuizHandlers.QueryHandlers {

	public sealed class GetQuizByIdQueryHandler : IQueryHandler<GetQuizByIdQuery, QuizQueryResult> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;

		public GetQuizByIdQueryHandler(
			IMapper mapper,
			IQuizDataProvider quizDataProvider,
			IGroupDataProvider groupDataProvider,
			IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_groupDataProvider = groupDataProvider;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<QuizQueryResult> HandleAsync( GetQuizByIdQuery query ) {
			List<QuestionViewModel> questionViewModels = await GetQuestionViewModels( query.UId );
			List<GroupViewModel> groupViewModels = await GetGroupViewModels( query.UId, questionViewModels );
			QuizViewModel quizViewModel = await GetQuizViewModel( query.UId, groupViewModels );

			return new QuizQueryResult {Quiz = quizViewModel};
		}

		private async Task<List<QuestionViewModel>> GetQuestionViewModels( string quizUid ) {
			ImmutableArray<QuestionDto> questionsDtos = await _questionDataProvider.GetByQuiz( quizUid );
			List<QuestionDto> orderedQuestionDtos = questionsDtos
				.OrderBy( x => x.ParentUId )
				.ThenBy( x => x.SortOrder )
				.ToList();

			List<Question> questions = _mapper.Map<List<Question>>( orderedQuestionDtos );
			List<QuestionViewModel> questionViewModels = _mapper.Map<List<QuestionViewModel>>( questions );

			return questionViewModels;
		}

		private async Task<List<GroupViewModel>> GetGroupViewModels( string quizUid, List<QuestionViewModel> questionViewModels ) {
			ImmutableArray<GroupDto> groupDtos = await _groupDataProvider.GetByQuiz( quizUid );
			List<GroupDto> orderedGroupDtos = groupDtos
				.OrderBy( x => x.SortOrder )
				.ToList();

			List<Group> groups = _mapper.Map<List<Group>>( orderedGroupDtos );
			List<GroupViewModel> groupViewModels = _mapper.Map<List<GroupViewModel>>( groups );

			foreach( GroupViewModel groupViewModel in groupViewModels ) {
				groupViewModel.Questions = questionViewModels
					.Where( x => x.GroupId == groupViewModel.Id )
					.ToImmutableArray();
			}

			return groupViewModels;
		}

		private async Task<QuizViewModel> GetQuizViewModel( string quizUid, List<GroupViewModel> groupViewModels ) {
			QuizDto quizDto = await _quizDataProvider.Get( quizUid );

			if( quizDto == null )
				return null;

			Quiz quiz = _mapper.Map<Quiz>( quizDto );
			QuizViewModel quizViewModel = _mapper.Map<QuizViewModel>( quiz );
			quizViewModel.Groups = groupViewModels.ToImmutableArray();

			return quizViewModel;
		}
	}
}
