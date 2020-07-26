using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Action.Client.Map;
using QuizBuilder.Domain.Action.Client.Services;
using QuizBuilder.Domain.Action.Client.Services.Default;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;
using Xunit;
using static QuizBuilder.Domain.Model.Default.Enums.PageSettings;

namespace QuizBuilder.Test.Unit.Services {

	public sealed class PageInfoDataFactoryTests {

		private readonly IPageInfoDataFactory _sut;

		private static readonly List<Group> Groups = new List<Group> {
			new Group {UId = "a", SortOrder = 4},
			new Group {UId = "b", SortOrder = 3},
			new Group {UId = "c", SortOrder = 2},
			new Group {UId = "d", SortOrder = 1}
		};

		private static readonly List<Question> Questions = new List<Question> {
			new TrueFalseQuestion { UId = "a1", SortOrder = 1, ParentUId = "a"},
			new TrueFalseQuestion { UId = "c2", SortOrder = 2, ParentUId = "c"},
			new TrueFalseQuestion { UId = "c1", SortOrder = 1, ParentUId = "c"},
			new TrueFalseQuestion { UId = "b6", SortOrder = 6, ParentUId = "b"},
			new TrueFalseQuestion { UId = "b5", SortOrder = 5, ParentUId = "b"},
			new TrueFalseQuestion { UId = "b4", SortOrder = 4, ParentUId = "b"},
			new TrueFalseQuestion { UId = "b3", SortOrder = 3, ParentUId = "b"},
			new TrueFalseQuestion { UId = "b2", SortOrder = 2, ParentUId = "b"},
			new TrueFalseQuestion { UId = "b1", SortOrder = 1, ParentUId = "b"},
		};


		public PageInfoDataFactoryTests() {
			_sut = new PageInfoDataFactory( new Mapper( new MapperConfiguration( cfg => cfg.AddProfile<MapperProfile>() ) ) );
		}

		[Fact]
		public void PagePerQuiz_NoRandom_Test() {

			var quiz = new Quiz {
				PageSettings = PagePerQuiz,
				RandomizeQuestions = false
			};

			List<PageInfo> result1 = _sut.Create( quiz, Groups, Questions );
			List<PageInfo> result2 = _sut.Create( quiz, Groups, Questions );

			Assert.Single( result1 );
			Assert.Single( result2 );
			Assert.Equal( Questions.Count, result1[0].Questions.Count );
			Assert.Equal( Questions.Count, result2[0].Questions.Count );

			result1.Should().BeEquivalentTo( result2, options => options.WithoutStrictOrdering() );
		}

		[Fact]
		public void PagePerQuiz_Random_Test() {

			var quiz = new Quiz {
				PageSettings = PagePerQuiz,
				RandomizeQuestions = true
			};

			List<PageInfo> result1 = _sut.Create( quiz, Groups, Questions );
			List<PageInfo> result2 = _sut.Create( quiz, Groups, Questions );

			Assert.Single( result1 );
			Assert.Single( result2 );
			Assert.Equal( Questions.Count, result1[0].Questions.Count );
			Assert.Equal( Questions.Count, result2[0].Questions.Count );

			AssertHasDifferentOrder( result1[0].Questions, result2[0].Questions );
		}

		private static void AssertHasDifferentOrder( IReadOnlyList<QuestionAttemptInfo> input1, IReadOnlyList<QuestionAttemptInfo> input2 ) {

			input1.Should().BeEquivalentTo( input2, options => options.WithoutStrictOrdering() );
			for( int i = 0; i < input1.Count; i++ ) {
				if( input1[i].UId == input2[i].UId )
					Assert.True( false );
			}
			Assert.True( true );
		}
	}

}
