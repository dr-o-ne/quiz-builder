namespace QuizBuilder.Model.Model.Default.Structure {

	public sealed class FreeText : QuizEntity {

		public override bool IsValid() {
			return !string.IsNullOrWhiteSpace( Text );
		}

	}

}
