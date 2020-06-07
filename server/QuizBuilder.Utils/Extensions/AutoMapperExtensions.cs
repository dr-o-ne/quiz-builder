using System.Linq;
using AutoMapper;

namespace QuizBuilder.Utils.Extensions {

	public static class AutoMapperExtensions {

		public static TResult Map<TResult>( this IMapper mapper, params object[] objects ) {
			TResult result = mapper.Map<TResult>( objects.First() );
			return objects.Skip( 1 ).Aggregate( result, ( res, obj ) => mapper.Map( obj, res ) );
		}

		public static TOriginal Merge<TModified, TOriginal>( this IMapper mapper, TModified modified, TOriginal original ) where TModified : TOriginal {
			return mapper.Map( modified, original );
		}

	}
}
