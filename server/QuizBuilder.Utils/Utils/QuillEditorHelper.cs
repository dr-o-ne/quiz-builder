using System.Text.RegularExpressions;

namespace QuizBuilder.Utils.Utils {

	public static class QuillEditorHelper {

		private static readonly Regex TagRegex = new Regex( @"<[^>]+>" );

		public static (string, bool) NormalizeText( string input ) {

			string copy = input;
			if( copy.StartsWith( "<p>" ) && copy.EndsWith( "</p>" ) ) {
				copy = input.Substring( 3, copy.Length - 7 );
			}

			return TagRegex.IsMatch( copy ) ? ( input, true ) : ( copy, false );

		}

	}

}
