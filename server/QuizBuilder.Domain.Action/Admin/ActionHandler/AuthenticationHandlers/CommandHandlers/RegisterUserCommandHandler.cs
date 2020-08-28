using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Common.Services;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Action.Common.Services;
using QuizBuilder.Domain.Model.Default.Organization;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, CommandResult<LoginViewModel>> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly UserManager<UserDto> _userManager;
		private readonly IJwtTokenFactory _jwtTokenFactory;
		private readonly SignInManager<UserDto> _signInManager;
		private readonly IOrganizationDataProvider _organizationDataProvider;

		public RegisterUserCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			UserManager<UserDto> userManager,
			IJwtTokenFactory jwtTokenFactory,
			IOrganizationDataProvider organizationDataProvider,
			SignInManager<UserDto> signInManager ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_userManager = userManager;
			_jwtTokenFactory = jwtTokenFactory;
			_organizationDataProvider = organizationDataProvider;
			_signInManager = signInManager;
		}

		public async Task<CommandResult<LoginViewModel>> HandleAsync( RegisterUserCommand command ) {

			UserDto userDto = _mapper.Map<UserDto>( command );
			if( string.IsNullOrEmpty( userDto.UserName ) )
				userDto.UserName = userDto.Email;

			if( await CheckUserExists( userDto ) ) {
				return new CommandResult<LoginViewModel> { IsSuccess = false }; // TODO: add client error message
			}

			OrganizationDto organization = await CreateOrganization();
			userDto.OrganizationId = organization.Id;

			IdentityResult result = await _userManager.CreateAsync( userDto, command.Password );
			if( !result.Succeeded ) {
				await _organizationDataProvider.Delete( organization.Id );
				return new CommandResult<LoginViewModel> { IsSuccess = false }; // TODO: filter and add client error message
			}

			UserDto user = await _userManager.FindByEmailAsync( command.Email );

			return await Login( user, command.Password );
		}

		private async Task<bool> CheckUserExists( UserDto userDto ) {
			var existingUser = await _userManager.FindByEmailAsync( userDto.Email );
			return existingUser != null;
		}

		private async Task<OrganizationDto> CreateOrganization() {
			var organization = new Organization {
				UId = _uIdService.GetUId()
			};
			OrganizationDto organizationDto = _mapper.Map<OrganizationDto>( organization );
			return await _organizationDataProvider.Add( organizationDto );
		}

		private async Task<CommandResult<LoginViewModel>> Login( UserDto user, string password ) {

			SignInResult signInResult = await _signInManager.PasswordSignInAsync( user, password, false, false );
			if( !signInResult.Succeeded )
				return new CommandResult<LoginViewModel> { IsSuccess = false };

			string token = _jwtTokenFactory.Create( user );

			var payload = new LoginViewModel { Username = user.UserName, Token = token };
			return new CommandResult<LoginViewModel> {
				IsSuccess = true,
				Payload = payload
			};

		}

	}

}
