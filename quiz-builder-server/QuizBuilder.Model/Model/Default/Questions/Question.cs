using QuizBuilder.Model.Model.Default.Structure;

namespace QuizBuilder.Model.Model.Default.Questions {

	public abstract class Question : QuizEntity {

		public override bool IsValid() {
			return !string.IsNullOrWhiteSpace( Text );
		}

	}

}
