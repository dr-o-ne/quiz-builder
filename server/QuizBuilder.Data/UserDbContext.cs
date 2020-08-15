using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data {

	public class UserDbContext : IdentityDbContext<UserDto>  {

		protected UserDbContext() {
		}

		public UserDbContext( DbContextOptions options) : base(options)
		{
		}

	}

}
