using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace QuizBuilder.Repository.Dto {
	[Table( "Quiz" )]
	public sealed class QuizDto {
		[IgnoreDataMember]
		public Guid Id { get; set; }
		public bool IsVisible { get; set; }
		public string Name { get; set; }
	}
}
