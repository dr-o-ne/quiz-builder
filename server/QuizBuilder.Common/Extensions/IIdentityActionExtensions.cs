using System.Security.Claims;
using QuizBuilder.Common.CQRS.Actions;

namespace QuizBuilder.Common.Extensions {

	public static class IdentityActionExtensions {

		public static void SetIdentity( this IIdentityAction action, ClaimsPrincipal user ) {
			action.OrgId = user.GetOrgId();
			action.UserId = user.GetUserId();
		}

	}
}
