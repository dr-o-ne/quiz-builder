﻿using Microsoft.AspNetCore.Identity;

namespace QuizBuilder.Data.Dto {

	public sealed class UserDto : IdentityUser {

		public long OrganizationId { get; set; }

	}

}
