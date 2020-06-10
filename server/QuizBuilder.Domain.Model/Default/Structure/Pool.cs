using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Model.Default.Structure {

	public sealed class Pool : QuizEntity {

		[JsonIgnore]
		public List<QuizEntity> Items { get; set; }

		public override bool IsValid() => true;

	}

}
