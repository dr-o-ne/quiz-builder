using System;
using QuizBuilder.Domain.Model.Default;
using Xunit;

namespace QuizBuilder.Test.Unit.Model {

	public sealed class QuizTests {

		[Fact]
		public void IsValid_Test() {
			var quiz = new Quiz {
				StartDate = DateTime.UtcNow.AddDays( 1 ),
				EndDate = DateTime.UtcNow.AddDays( -1 )
			};
			Assert.False( quiz.IsValid() );
		}

		[Fact]
		public void QuizDisabled_Test() {
			var quiz = new Quiz {
				IsEnabled = false
			};
			Assert.False( quiz.IsAvailable() );
		}

		[Theory]
		[InlineData( true, -1, 1 )]
		[InlineData( true, -1, null )]
		[InlineData( true, 1, null )]
		[InlineData( true, null, -1 )]
		[InlineData( true, null, 1 )]
		[InlineData( true, null, null )]
		public void IsAvailable_QuizEnabled_ScheduleDisabled_Test( bool expected, int? startDateDaysDiff, int? endDateDaysDiff ) {
			var quiz = new Quiz {
				IsEnabled = true,
				IsScheduleEnabled = false
			};

			if( startDateDaysDiff != null )
				quiz.StartDate = DateTime.UtcNow.AddDays( startDateDaysDiff.Value );
			if( endDateDaysDiff != null )
				quiz.EndDate = DateTime.UtcNow.AddDays( endDateDaysDiff.Value );

			Assert.Equal( expected, quiz.IsAvailable() );
		}

		[Theory]
		[InlineData( true, -1, 1 )]
		[InlineData( true, -1, null )]
		[InlineData( false, 1, null )]
		[InlineData( false, null, -1 )]
		[InlineData( true, null, 1 )]
		[InlineData( true, null, null )]
		public void IsAvailable_QuizEnabled_ScheduleEnabled_Test( bool expected, int? startDateDaysDiff, int? endDateDaysDiff ) {
			var quiz = new Quiz {
				IsEnabled = true,
				IsScheduleEnabled = true
			};

			if( startDateDaysDiff != null )
				quiz.StartDate = DateTime.UtcNow.AddDays( startDateDaysDiff.Value );
			if( endDateDaysDiff != null )
				quiz.EndDate = DateTime.UtcNow.AddDays( endDateDaysDiff.Value );

			Assert.Equal( expected, quiz.IsAvailable() );
		}

	}

}
