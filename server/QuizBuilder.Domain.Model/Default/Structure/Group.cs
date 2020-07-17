using System.Collections.Generic;
using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Structure {

	public sealed class Group : QuizEntity {

		[JsonIgnore]
		public List<Question> Questions { get; set; }

		public override bool IsValid() => true;

	}

}
