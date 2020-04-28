using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public abstract class Question : QuizEntity {

		public override bool IsValid() => !string.IsNullOrWhiteSpace( Text );
	}

}
