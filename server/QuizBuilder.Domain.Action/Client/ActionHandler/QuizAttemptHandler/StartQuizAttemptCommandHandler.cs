using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Appearance;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;
using QuizBuilder.Utils.Services;
using QuizBuilder.Utils.Utils;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class StartQuizAttemptCommandHandler : ICommandHandler<StartQuizAttemptCommand, StartQuizAttemptCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;


		public StartQuizAttemptCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			IQuizDataProvider quizDataProvider,
			IQuestionDataProvider questionDataProvider,
			IQuizAttemptDataProvider attemptDataProvider,
			IGroupDataProvider groupDataProvider ) {

			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_questionDataProvider = questionDataProvider;
			_attemptDataProvider = attemptDataProvider;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<StartQuizAttemptCommandResult> HandleAsync( StartQuizAttemptCommand command ) {

			string quizUId = command.QuizUId;

			if( string.IsNullOrWhiteSpace( quizUId ) )
				return new StartQuizAttemptCommandResult { IsSuccess = false };

			QuizDto quizDto = await _quizDataProvider.Get( quizUId );

			if( quizDto == null )
				return new StartQuizAttemptCommandResult { IsSuccess = false };

			//TODO: load from DB
			var appearance = new Appearance {
				ShowQuizName = true,
				HeaderColor = "#1a202e",
				MainColor = "#f5f5f8",
				CardColor = "#fff",
				FooterColor = "#fff"
			};

			List<QuestionDto> questionDtos = (await _questionDataProvider.GetByQuiz( command.QuizUId ))
				.OrderBy( x => x.SortOrder )
				.ToList();

			ImmutableArray<GroupDto> groupDtos = await _groupDataProvider.GetByQuiz( command.QuizUId );
			

			Quiz quiz = _mapper.Map<QuizDto, Quiz>( quizDto );

			List<Question> questions = _mapper.Map<List<Question>>( questionDtos );
			List<Group> groups = _mapper.Map<List<Group>>( groupDtos.OrderBy( x => x.SortOrder ) );

			QuizAttempt quizAttempt = await CreateQuizAttempt( quizUId );

			return new StartQuizAttemptCommandResult {
				IsSuccess = true,
				Message = string.Empty,
				Payload = MapPayload( quizAttempt.UId, quiz, groups, questions, appearance )
			};

		}

		private async Task<QuizAttempt> CreateQuizAttempt( string quizUId ) {

			var quizAttempt = new QuizAttempt {
				UId = _uIdService.GetUId(),
				QuizUId = quizUId,
				StartDate = DateTime.UtcNow,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow
			};

			var quizAttemptDto = _mapper.Map<AttemptDto>( quizAttempt );
			await _attemptDataProvider.Add( quizAttemptDto );

			return quizAttempt;
		}

		private static QuizAttemptInfo MapPayload( string uid, Quiz quiz, List<Group> groups, List<Question> questions, Appearance appearance ) {

			var groupAttemptInfos = groups.Select(
				g => new GroupAttemptInfo {
					UId = string.Empty,
					Name = string.Empty,
					Questions = questions.Where( x => x.ParentUId == g.UId ).Select( MapQuestion ).ToImmutableArray()
				} ).ToList();

			var result = new QuizAttemptInfo {
				UId = uid,
				Name = appearance.ShowQuizName ? quiz.Name : string.Empty,
				SettingsInfo = MapSettings( quiz ),
				AppearanceInfo = MapAppearance( appearance ),
				Groups = groupAttemptInfos.Where( g => g.Questions.Any() ).ToImmutableArray()
			};

			return result;
		}

		private static SettingsInfo MapSettings( Quiz quiz ) =>
			new SettingsInfo {
				IsPrevButtonEnabled = quiz.IsPrevButtonEnabled
			};

		private static AppearanceInfo MapAppearance( Appearance appearance ) =>
			new AppearanceInfo {
				HeaderColor = appearance.HeaderColor,
				MainColor = appearance.MainColor,
				CardColor = appearance.CardColor,
				FooterColor = appearance.FooterColor
			};

		private static QuestionAttemptInfo MapQuestion( Question question ) {

			long GetChoicesDisplayType() {
				if( question is TrueFalseQuestion trueFalseQuestion ) return (long)trueFalseQuestion.ChoicesDisplayType;
				if( question is MultipleChoiceQuestion multipleChoiceQuestion ) return (long)multipleChoiceQuestion.ChoicesDisplayType;
				if( question is MultipleSelectQuestion multipleSelectQuestion ) return (long)multipleSelectQuestion.ChoicesDisplayType;

				return 0;
			}

			//TODO: save in db
			(string content, bool isHtml) = QuillEditorHelper.NormalizeText( question.Text );

			return new QuestionAttemptInfo {
				UId = question.UId,
				Type = (long)question.Type,
				Text = content,
				IsHtmlText = isHtml,
				ChoicesDisplayType = GetChoicesDisplayType(),
				Choices = MapChoices( question )
			};
		}

		private static ImmutableArray<ChoiceAttemptInfo> MapChoices( Question question ) {

			static ChoiceAttemptInfo MapBinaryChoice( BinaryChoice choice ) => new ChoiceAttemptInfo { Id = choice.Id, Text = choice.Text };

			switch( question.Type ) {
				case Enums.QuestionType.TrueFalse: {
					var x = (TrueFalseQuestion)question;
					return ImmutableArray.Create( MapBinaryChoice( x.TrueChoice ), MapBinaryChoice( x.FalseChoice ) );
				}
				case Enums.QuestionType.MultiChoice: {
					var x = (MultipleChoiceQuestion)question;
					return x.GetChoicesRandomized().Select( MapBinaryChoice ).ToImmutableArray();
				}
				case Enums.QuestionType.MultiSelect: {
					var x = (MultipleSelectQuestion)question;
					return x.GetChoicesRandomized().Select( MapBinaryChoice ).ToImmutableArray();
				}
				case Enums.QuestionType.LongAnswer: 
					return ImmutableArray<ChoiceAttemptInfo>.Empty;
			}

			throw null;
		}

	}

}
