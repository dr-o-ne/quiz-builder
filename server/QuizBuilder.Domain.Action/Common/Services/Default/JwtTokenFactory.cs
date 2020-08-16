using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QuizBuilder.Data.Dto;
using QuizBuilder.Utils;

namespace QuizBuilder.Domain.Action.Common.Services.Default {

	internal sealed class JwtTokenFactory : IJwtTokenFactory {

		public string Create( UserDto user ) {

			var tokenHandler = new JwtSecurityTokenHandler();
			byte[] key = Encoding.ASCII.GetBytes( Consts.JwtSecret );

			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity( new[] {new Claim( "id", user.Id )} ),
				Expires = DateTime.UtcNow.AddDays( 7 ),
				SigningCredentials = new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256Signature )
			};

			SecurityToken token = tokenHandler.CreateToken( tokenDescriptor );
			return tokenHandler.WriteToken( token );
		}

	}
}
