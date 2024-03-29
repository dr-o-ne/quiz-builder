﻿using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.Services;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using Group = QuizBuilder.Domain.Model.Default.Structure.Group;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, GroupCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public CreateGroupCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			IQuizDataProvider quizDataProvider,
			IGroupDataProvider groupDataProvider,
			IStructureDataProvider structureDataProvider ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_groupDataProvider = groupDataProvider;
			_structureDataProvider = structureDataProvider;
		}

		public async Task<GroupCommandResult> HandleAsync( CreateGroupCommand command ) {

			var quizDto = await _quizDataProvider.Get( command.OrgId, command.UserId, command.QuizUId );
			if( quizDto == null )
				return new GroupCommandResult { IsSuccess = false };

			ImmutableArray<GroupDto> groupDtos = await _groupDataProvider.GetByQuiz( command.QuizUId );

			var modelBefore = new Group {
				UId = _uIdService.GetUId(),
				Name = GetNextGroupName( groupDtos ),
				IsEnabled = true,
				SelectAllQuestions = true
			};

			if( !modelBefore.IsValid() )
				return new GroupCommandResult { IsSuccess = false };

			var dto = _mapper.Map<GroupDto>( modelBefore );
			dto = await Save( quizDto.Id, dto );

			var modelAfter = _mapper.Map<Group>( dto );
			var groupViewModel = _mapper.Map<GroupViewModel>( modelAfter );

			return new GroupCommandResult {
				IsSuccess = true,
				Group = groupViewModel
			};
		}

		private static string GetNextGroupName( ImmutableArray<GroupDto> dtos ) {
			Regex regExp = new Regex( @"^Group \d+$" );

			int defaultNumber = dtos.Where( x => regExp.IsMatch( x.Name ) )
				.Select( x => x.Name.Substring( 6 ) )
				.Select( x => Convert.ToInt32( x ) )
				.DefaultIfEmpty( 0 )
				.Max();

			return $"Group {Math.Max( defaultNumber, dtos.Length ) + 1}";
		}

		private async Task<GroupDto> Save( long quizId, GroupDto dto ) {
			var result = await _groupDataProvider.Add( quizId, dto );
			await _structureDataProvider.AddQuizQuestionRelationship( quizId, result.Id );

			return result;
		}

	}
}
