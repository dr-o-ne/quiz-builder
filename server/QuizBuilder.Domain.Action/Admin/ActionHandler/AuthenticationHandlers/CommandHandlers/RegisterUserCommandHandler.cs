using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Common.Services;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Organization;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.AuthenticationHandlers.CommandHandlers {

	public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly UserManager<UserDto> _userManager;
		private readonly IOrganizationDataProvider _organizationDataProvider;

		public RegisterUserCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			UserManager<UserDto> userManager,
			IOrganizationDataProvider organizationDataProvider ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_userManager = userManager;
			_organizationDataProvider = organizationDataProvider;
		}

		public async Task<CommandResult> HandleAsync( RegisterUserCommand userCommand ) {

			UserDto userDto = _mapper.Map<UserDto>( userCommand );
			if( string.IsNullOrEmpty( userDto.UserName ) )
				userDto.UserName = userDto.Email;

			if( await CheckUserExists( userDto ) ) {
				return CommandResult.Fail(); // TODO: add client error message
			}

			var organization = await CreateOrganization();
			userDto.OrganizationId = organization.Id;

			IdentityResult result = await _userManager.CreateAsync( userDto, userCommand.Password );
			if( !result.Succeeded ) {
				await _organizationDataProvider.Delete( organization.Id );
				return CommandResult.Fail(); // TODO: filter and add client error message
			}

			return CommandResult.Success();
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

	}

}
