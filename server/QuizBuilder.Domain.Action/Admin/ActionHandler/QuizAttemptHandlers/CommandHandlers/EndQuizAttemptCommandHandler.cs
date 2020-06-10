using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Graders;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuizAttemptHandlers.CommandHandlers {

	public sealed class EndQuizAttemptCommandHandler : ICommandHandler<EndQuizAttemptCommand, EndQuizAttemptCommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;
		private readonly IQuizDataProvider _quizDataProvider;

		public EndQuizAttemptCommandHandler( IMapper mapper, IQuestionDataProvider questionDataProvider, IQuizAttemptDataProvider attemptDataProvider, IQuizDataProvider quizDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
			_attemptDataProvider = attemptDataProvider;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<EndQuizAttemptCommandResult> HandleAsync( EndQuizAttemptCommand command ) {

			List<QuestionAnswerDU> questionAttemptDUs = _mapper.Map<EndQuizAttemptCommand, List<QuestionAnswerDU>>( command );

			AttemptDto attemptDto = await _attemptDataProvider.Get( command.UId );
			QuizDto quizDto = await _quizDataProvider.Get( attemptDto.QuizId );
			IEnumerable<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( quizDto.UId );
			List<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos ).ToList();

			double totalScore = 0;

			foreach( QuestionAnswerDU item in questionAttemptDUs ) {

				Question question = questions.SingleOrDefault( x => x.UId == item.QuestionUId );
				if( question == null ) {

					//
					throw null;
				}

				switch( question.Type ) {

					case Enums.QuestionType.MultiChoice:

						MultipleChoiceAnswer answer = _mapper.Map<QuestionAnswerDU, MultipleChoiceAnswer>( item );
						var grader = new MultipleChoiceGrader();
						double result = grader.Grade( (MultipleChoiceQuestion)question, answer );
						totalScore += result;

						break;

					default:
						// throw new ArgumentOutOfRangeException();
						continue;
				}

			}

			return new EndQuizAttemptCommandResult() {
				Success = true,
				Message = string.Empty,
				Score = totalScore
			};
		}

	}

}
