using System.Collections.Generic;
using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Structure {

	public sealed class Group : QuizEntity {

		[JsonIgnore]
		public List<Question> Questions { get; set; }

		[JsonIgnore]
		public bool IsEnabled { get; set; }

		public bool SelectAllQuestions { get; set; }

		public bool RandomizeQuestions { get; set; }

		public int? CountOfQuestionsToSelect { get; set; }

		public override bool IsValid() => true;

	}

}
