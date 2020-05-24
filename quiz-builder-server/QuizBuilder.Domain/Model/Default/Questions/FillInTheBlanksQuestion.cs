using System.Collections.Generic;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Choices;
using static QuizBuilder.Domain.Model.Enums;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class FillInTheBlanksText : IOrdered {

		public string Text { get; set; }

		public int Order { get; set; }

	}

	public sealed class FillInTheBlanksQuestion : Question {

		public override QuestionType Type { get => FillInTheBlanks; }

		public override string Text {
			get { return string.Empty; }
			set { }
		}

		public override Question NullifyChoices() {
			return this;
		}

		public List<FillInTheBlanksText> Texts { get; set; } = new List<FillInTheBlanksText>();

		public List<TextChoiceGroup> ChoiceGroups { get; set; } = new List<TextChoiceGroup>();

		public override bool IsValid() {
			return true; //TODO:
		}
	}

}
