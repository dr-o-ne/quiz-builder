using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;
using QuizBuilder.Utils.Extensions;
using static QuizBuilder.Domain.Model.Default.Enums.PageSettings;


namespace QuizBuilder.Domain.Action.Client.Map.Extensions {

	internal static class MapperExtensions {

		public static List<PageInfo> MapGroupAttemptInfos( this IMapper mapper, Quiz quiz, List<Group> groups, List<Question> questions ) {

			groups = Sort( groups );
			questions = Sort( groups, questions );

			var result = new List<PageInfo>();

			switch( quiz.PageSettings ) {
				case PagePerQuiz: {
					var page = new PageInfo();

					page.Questions = questions.Select( mapper.Map<QuestionAttemptInfo> ).ToList();
					if( quiz.RandomizeQuestions )
						page.Questions.Shuffle();
					 
					result.Add( page );
					break;
				}
				case PagePerGroup: {
					foreach( var group in groups ) {
						var page = new PageInfo();
						page.Questions = questions
							.Where( x => x.ParentUId == group.UId )
							.Select( mapper.Map<QuestionAttemptInfo> )
							.ToList();
						if( page.Questions.Any() )
							result.Add( page );
					}
					break;
				}
				case PagePerQuestion: {
					foreach( var question in questions ) {
						var page = new PageInfo();
						page.Questions.Add( mapper.Map<QuestionAttemptInfo>( question ) );

						result.Add( page );
					}

					if( quiz.RandomizeQuestions )
						result.Shuffle();
					break;
				}
				case Custom: {

					var questionItems = questions
						.Select( mapper.Map<QuestionAttemptInfo> )
						.ToList();

					if(quiz.RandomizeQuestions)
						questionItems.Shuffle();

					var questionGroups = questionItems
						.ChunkBy( quiz.QuestionsPerPage );

					foreach( var questionGroup in questionGroups ) {
						var page = new PageInfo();
						page.Questions.AddRange( questionGroup );
						result.Add( page );
					}
					break;
				}

			}

			return result;
		}

		private static List<Group> Sort( IEnumerable<Group> groups ) =>
			groups
				.OrderBy( x => x.SortOrder )
				.ToList();

		private static List<Question> Sort( IEnumerable<Group> groups, IEnumerable<Question> questions ) {
			var groupUIds = groups.Select( x => x.UId ).ToList();
			return questions
				.OrderBy( x => groupUIds.IndexOf( x.ParentUId ) )
				.ThenBy( x => x.SortOrder )
				.ToList();
		}
	}
}
