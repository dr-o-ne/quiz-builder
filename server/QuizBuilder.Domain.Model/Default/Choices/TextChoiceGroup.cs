using System.Collections.Generic;
using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Choices {

	public sealed class TextChoiceGroup : IOrdered {

		public List<TextChoice> Choices { get; set; } = new List<TextChoice>();

		public int Order { get; set; }
	}
}
