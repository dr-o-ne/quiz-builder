using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Repository.Repository.Default {

	internal sealed class GenericQueryBuilder<T> : IGenericQueryBuilder<T> {

		private readonly string _tableName;
		private readonly ImmutableArray<string> _columnNames;

		public GenericQueryBuilder() {
			_tableName = GetTableName;
			_columnNames = GetProperties
				.Where( p => p.GetCustomAttributes( typeof( IgnoreDataMemberAttribute ), false ).Length == 0 )
				.Select( x => x.Name )
				.ToImmutableArray();
		}

		public string GenerateInsertQuery() {
			var sb = new StringBuilder( $"INSERT INTO {_tableName} " );

			sb.Append( "(" );
			foreach( var column in _columnNames ) {
				sb.Append( $"[{column}]," );
			}
			sb.Remove( sb.Length - 1, 1 ).Append( ") VALUES (" );

			foreach( var column in _columnNames ) {
				sb.Append( $"@{column}," );
			}

			return sb
				.Remove( sb.Length - 1, 1 )
				.Append( ")" )
				.ToString();
		}

		public string GenerateUpdateQuery() {
			var updateQuery = new StringBuilder( $"UPDATE {_tableName} SET " );

			foreach( string columnName in _columnNames ) {
				updateQuery.Append( $"{columnName}=@{columnName}," );
			}

			return updateQuery
				.Remove( updateQuery.Length - 1, 1 )
				.Append( " WHERE Id=@Id" )
				.ToString();
		}

		private static IEnumerable<PropertyInfo> GetProperties => typeof( T ).GetProperties( BindingFlags.Instance | BindingFlags.Public );

		private static string GetTableName => typeof( T ).GetAttributeValue( ( TableAttribute attribute ) => attribute.Name );

	}

}
