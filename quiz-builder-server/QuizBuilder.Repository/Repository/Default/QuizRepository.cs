using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using QuizBuilder.Model.Model.Default;
using System.Collections.Generic;

namespace QuizBuilder.Repository.Repository.Default {
	public class QuizRepository : IQuizRepository {
		private readonly string connectionString;
		public QuizRepository( string conn ) {
			connectionString = conn;
		}

		public Quiz Add( Quiz entity ) {
			using( IDbConnection db = new SqlConnection( connectionString ) ) {
				string sqlQuery = "INSERT INTO Quiz (Name) VALUES(@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
				int? userId = db.Query<int>( sqlQuery, entity ).FirstOrDefault();
				entity.Id = userId.Value;
			}
			return entity;
		}

		public void Delete( long id ) {
			using( IDbConnection db = new SqlConnection( connectionString ) ) {
				string sqlQuery = "DELETE FROM Quiz WHERE Id = @id";
				db.Execute( sqlQuery, new { id } );
			}
		}

		public void Edit( Quiz entity ) {
			using( IDbConnection db = new SqlConnection( connectionString ) ) {
				string sqlQuery = "UPDATE Quiz SET Name = @Name WHERE Id = @Id";
				db.Execute( sqlQuery, entity );
			}
		}

		public IEnumerable<Quiz> GetAll() {
			using IDbConnection db = new SqlConnection( connectionString );
			return db.Query<Quiz>( "SELECT * FROM Quiz" );
		}

		public Quiz GetById( long id ) {
			using IDbConnection db = new SqlConnection( connectionString );
			return db.Query<Quiz>( "SELECT * FROM Quiz WHERE Id = @id", new { id } ).FirstOrDefault();
		}
	}
}
