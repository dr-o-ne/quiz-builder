using System;
using System.Security.Claims;

namespace QuizBuilder.Common.Extensions {

	public static class ClaimsPrincipalExtensions {

		public static string GetUserId( this ClaimsPrincipal user ) =>
			user.FindFirst( ClaimTypes.NameIdentifier ).Value;

		public static long GetOrgId( this ClaimsPrincipal user ) =>
			Convert.ToInt64( user.FindFirst( Consts.Claim.OrgId ).Value );

	}

}
