using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Graders;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class EndQuizAttemptCommandHandler : ICommandHandler<EndQuizAttemptCommand, CommandResult<AttemptFeedbackInfo>> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;

		public EndQuizAttemptCommandHandler(
			IMapper mapper,
			IQuizDataProvider quizDataProvider,
			IQuizAttemptDataProvider attemptDataProvider,
			IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_attemptDataProvider = attemptDataProvider;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<CommandResult<AttemptFeedbackInfo>> HandleAsync( EndQuizAttemptCommand command ) {

			AttemptDto attemptDto = await _attemptDataProvider.Get( command.AttemptUId );

			List<Question> questions = await GetQuestions( attemptDto );

			decimal totalScore = 0;

			foreach( QuestionAttemptResult item in command.Answers ) {
				Question question = questions.SingleOrDefault( x => x.UId == item.QuestionUId );
				if( question == null ) {
					return new CommandResult<AttemptFeedbackInfo> { IsSuccess = false, Message = string.Empty };
				}
				//TODO: check required;

				switch( question.Type ) {

					case TrueFalse:

						var trueFalseGrader = new TrueFalseGrader();
						var trueFalseQuestion = (TrueFalseQuestion)question;
						var trueFalseAnswer = new TrueFalseAnswer( question.UId, item.ChoiceId );
						decimal trueFalseResult = trueFalseGrader.Grade( trueFalseQuestion, trueFalseAnswer );
						totalScore += trueFalseResult;

						break;

					case MultiChoice:

						var multipleChoiceGrader = new MultipleChoiceGrader();
						var multipleChoiceQuestion = (MultipleChoiceQuestion)question;
						var multipleChoiceAnswer = new MultipleChoiceAnswer( question.UId, item.ChoiceId );
						decimal multipleChoiceResult = multipleChoiceGrader.Grade( multipleChoiceQuestion, multipleChoiceAnswer );
						totalScore += multipleChoiceResult;

						break;

					case MultiSelect:

						var multipleSelectGrader = new MultipleSelectGrader();
						var multipleSelectQuestion = (MultipleSelectQuestion)question;
						var multipleSelectAnswer = new MultipleSelectAnswer( question.UId, item.ChoiceIds );
						decimal multipleSelectResult = multipleSelectGrader.Grade( multipleSelectQuestion, multipleSelectAnswer );
						totalScore += multipleSelectResult;

						break;

					case LongAnswer:

						var longAnswerGrader = new LongAnswerGrader();
						var longAnswerQuestion = (LongAnswerQuestion)question;
						var longAnswerAnswer = new LongAnswerAnswer( question.UId, item.Text );
						decimal longAnswerResult = longAnswerGrader.Grade( longAnswerQuestion, longAnswerAnswer );
						totalScore += longAnswerResult;

						break;

					default:
						throw null;
				}


			}

			attemptDto.Result = totalScore;
			attemptDto.EndDate = DateTime.UtcNow;

			await _attemptDataProvider.Update( 0, "169", attemptDto );

			return new CommandResult<AttemptFeedbackInfo> {
				IsSuccess = true,
				Message = string.Empty,
				Payload = new AttemptFeedbackInfo {Score = totalScore}
			};

		}

		private async Task<List<Question>> GetQuestions( AttemptDto attemptDto ) {
			QuizDto quizDto = await _quizDataProvider.Get( Consts.SupportUser.OrgId, Consts.SupportUser.UserId, attemptDto.QuizId );
			List<QuestionDto> questionDtos = ( await _questionDataProvider.GetByQuiz( quizDto.UId ) )
				.OrderBy( x => x.SortOrder )
				.ToList();
			return _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos ).ToList();
		}

	}
}
