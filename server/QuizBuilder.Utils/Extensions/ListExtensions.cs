using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizBuilder.Utils.Extensions {

	public static class ListExtensions {

		private static readonly Random Rng = new Random();

		public static void Shuffle<T>( this IList<T> list ) {
			int n = list.Count;
			while( n > 1 ) {
				n--;
				int k = Rng.Next( n + 1 );
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

		public static List<List<T>> ChunkBy<T>( this List<T> list, long chunkSize ) =>
			list.Select( ( x, i ) => new {Index = i, Value = x} )
				.GroupBy( x => x.Index / chunkSize )
				.Select( x => x.Select( v => v.Value ).ToList() )
				.ToList();

		public static bool ContainsDuplicates<T>( this IList<T> list ) {

			var set = new HashSet<T>( list );
			return set.Count != list.Count;

		}

	}

}
