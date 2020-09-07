using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;
using static QuizBuilder.Domain.Model.Default.Enums;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.QueryHandlers {

	public sealed class GetQuestionTemplateQueryHandler : IQueryHandler<GetQuestionTemplateQuery, CommandResult<QuestionViewModel>> {

		private readonly IMapper _mapper;

		public GetQuestionTemplateQueryHandler( IMapper mapper ) {
			_mapper = mapper;
		}

		public async Task<CommandResult<QuestionViewModel>> HandleAsync( GetQuestionTemplateQuery query ) {
			var quizEntity = CreateQuizEntity( query.Type );

			var payload = _mapper.Map<QuestionViewModel>( quizEntity );

			return new CommandResult<QuestionViewModel> {
				IsSuccess = true,
				Message = "",
				Payload = payload
			};

		}

		private static QuizEntity CreateQuizEntity( QuizItemType type ) {
			QuizEntity quizEntity;
			switch( type ) {
				case LongAnswer:
					quizEntity = new LongAnswerQuestion();
					break;
				case TrueFalse:
					var trueFalseQuestion = new TrueFalseQuestion();
					trueFalseQuestion.TrueChoice = new BinaryChoice {Id = 0, Order = 0, Text = "True", IsCorrect = true};
					trueFalseQuestion.FalseChoice = new BinaryChoice {Id = 1, Order = 1, Text = "False", IsCorrect = false};
					return trueFalseQuestion;
				case MultiChoice:
					var multipleChoiceQuestion = new MultipleChoiceQuestion();
					multipleChoiceQuestion.Choices.Add( new BinaryChoice {Id = 0, Order = 0, Text = "True", IsCorrect = true} );
					multipleChoiceQuestion.Choices.Add( new BinaryChoice {Id = 1, Order = 1, Text = "False", IsCorrect = false} );
					return multipleChoiceQuestion;
				case MultiSelect:
					var multipleSelectQuestion = new MultipleSelectQuestion();
					multipleSelectQuestion.Choices.Add( new BinaryChoice {Id = 0, Order = 0, Text = "True", IsCorrect = true} );
					multipleSelectQuestion.Choices.Add( new BinaryChoice {Id = 1, Order = 1, Text = "False", IsCorrect = false} );
					return multipleSelectQuestion;
				case Empty:
					quizEntity = new EmptyQuestion();
					break;
				default:
					quizEntity = null;
					break;
			}

			return quizEntity;
		}

	}

}
