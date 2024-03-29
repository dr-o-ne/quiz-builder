﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class MoveQuestionCommandHandler : ICommandHandler<MoveQuestionCommand, CommandResult> {

		private readonly IGroupDataProvider _groupDataProvider;

		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public MoveQuestionCommandHandler(
			IQuestionDataProvider questionDataProvider,
			IStructureDataProvider structureDataProvider,
			IGroupDataProvider groupDataProvider ) {
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<CommandResult> HandleAsync( MoveQuestionCommand command ) {
			GroupDto groupDto = await _groupDataProvider.Get( command.GroupUId );
			if( groupDto == null )
				return new QuestionCommandResult {IsSuccess = false};

			QuestionDto questionDto = await _questionDataProvider.Get( command.QuestionUId );
			if( questionDto == null )
				return new QuestionCommandResult { IsSuccess = false };

			await _structureDataProvider.UpdateGroupQuizItemRelationship( groupDto.Id, command.QuestionUId );

			List<QuestionDto> dtos = ( await _questionDataProvider.GetByGroup( command.GroupUId ) )
				.OrderBy( x => x.SortOrder )
				.ToList();

			for( int i = 0; i < command.QuestionUIds.Length; i++ ) {
				QuestionDto dto = dtos.Single( x => x.UId == command.QuestionUIds[i] );
				dto.SortOrder = i;
				await _questionDataProvider.Update( dto );
			}

			return new CommandResult( true, string.Empty );
		}
	}
}
