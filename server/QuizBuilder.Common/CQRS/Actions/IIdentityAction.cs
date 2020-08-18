namespace QuizBuilder.Common.CQRS.Actions {

	public interface IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

	}

}
