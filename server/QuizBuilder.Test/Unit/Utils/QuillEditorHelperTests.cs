using QuizBuilder.Common.Utils;
using Xunit;

namespace QuizBuilder.Test.Unit.Utils {

	public sealed class QuillEditorHelperTests {

		[Theory]
		[InlineData( @"<p class=""ql-align-center"">Test</p>", true, @"<p class=""ql-align-center"">Test</p>" )]
		[InlineData( "test2", false, "<p>test2</p>" )]
		public void NormalizeText_Test( string expectedString, bool expectedIsHtml, string input ) =>
			Assert.Equal( ( expectedString, expectedIsHtml ), QuillEditorHelper.NormalizeText( input ) );
	}

}
