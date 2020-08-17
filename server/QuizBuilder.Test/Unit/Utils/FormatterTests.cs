using QuizBuilder.Utils.Utils;
using Xunit;

namespace QuizBuilder.Test.Unit.Utils {

	public sealed class FormatterTests {

		[Theory]
		[InlineData( null, null )]
		[InlineData( "", "" )]
		[InlineData( "", "    " )]
		[InlineData( "test@ukr.net", "test@ukr.net" )]
		[InlineData( "test@ukr.net", " test@ukr.net   " )]
		[InlineData( "test@ukr.net", " TesT@ukr.neT" )]
		public void NormalizeEmail_Test( string expected, string input ) =>
			Assert.Equal( expected, Formatter.NormalizeEmail( input ) );

	}
}
