using System;

namespace QuizBuilder.Common.Utils {

	public static class Converter {

		public static DateTime? FromUnixTimeSeconds( long? value ) {

			if( value == null )
				return null;

			return DateTimeOffset.FromUnixTimeSeconds( value.Value ).UtcDateTime;
		}

	}
}
