using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Model.Default.Structure {

	public sealed class FreeText : QuizEntity {

		[JsonIgnore]
		public string Text { get; set; }

		public override bool IsValid() => !string.IsNullOrWhiteSpace( Text );

	}

}
