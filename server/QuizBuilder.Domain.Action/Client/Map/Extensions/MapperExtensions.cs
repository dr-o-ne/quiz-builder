using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;
using static QuizBuilder.Domain.Model.Default.Enums.PageSettings;


namespace QuizBuilder.Domain.Action.Client.Map.Extensions {

	internal static class MapperExtensions {

		public static List<PageInfo> MapGroupAttemptInfos( this IMapper mapper, Quiz quiz, List<Group> groups, List<Question> questions ) {
			var result = new List<PageInfo>();

			switch( quiz.PageSettings ) {
				case PagePerQuiz: {
					var page = new PageInfo();
					page.Questions = questions.Select( mapper.Map<QuestionAttemptInfo> ).ToList();
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

					break;
				}
				default:
					throw null;
			}

			return result;
		}

	}
}
