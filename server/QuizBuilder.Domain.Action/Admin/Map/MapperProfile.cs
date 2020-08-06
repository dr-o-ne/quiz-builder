using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Action.Admin.Map.Default.ActionToModel;
using QuizBuilder.Domain.Action.Admin.Map.Default.DtoToModel;
using QuizBuilder.Domain.Action.Admin.Map.Default.ModelToDto;
using QuizBuilder.Domain.Action.Admin.Map.Default.ModelToModel;
using QuizBuilder.Domain.Action.Admin.Map.Default.ModelToViewModel;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;
using QuizBuilder.Utils;

namespace QuizBuilder.Domain.Action.Admin.Map {

	public sealed class MapperProfile : Profile {

		public MapperProfile() {
			AddQuizMapping();
			AddGroupMapping();
			AddQuestionMapping();
			AddAttemptMapping();
		}

		private void AddQuizMapping() {
			CreateMap<Quiz, QuizDto>().ConvertUsing<QuizToQuizDtoConverter>();
			CreateMap<QuizDto, Quiz>().ConvertUsing<QuizDtoToQuizConverter>();
			CreateMap<UpdateQuizCommand, Quiz>().ConvertUsing<UpdateQuizCommandToQuizConverter>();

			CreateMap<Quiz, QuizViewModel>()
				.ForMember( x => x.Id, opt => opt.MapFrom( source => source.UId ) )
				.ForMember( x => x.Groups, opt => opt.Ignore() );
		}

		private void AddQuestionMapping() {
			CreateMap<Question, Question>().ConvertUsing<QuestionToQuestionConverter>();
			CreateMap<Question, QuestionDto>().ConvertUsing<QuestionToQuestionDtoConverter>();
			CreateMap<QuestionDto, Question>().ConvertUsing<QuestionDtoToQuestionConverter>();
			CreateMap<Question, QuestionViewModel>().ConvertUsing<QuestionToQuestionViewModelConverter>();
			CreateMap<CreateQuestionCommand, Question>().ConvertUsing<CreateQuestionCommandToQuestionConverter>();
			CreateMap<UpdateQuestionCommand, Question>().ConvertUsing<UpdateQuestionCommandToQuestionConverter>();
		}

		private void AddGroupMapping() {

			CreateMap<UpdateGroupCommand, Group>( MemberList.Source )
				.ForMember( x => x.Questions, opt => opt.Ignore() )
				.ForMember( x => x.IsEnabled, opt => opt.Ignore() );

			CreateMap<Group, GroupDto>()
				.ForMember( x => x.Settings, opt => opt.MapFrom( source => JsonSerializer.Serialize( source, Consts.JsonSerializerOptions ) ) );

			CreateMap<GroupDto, Group>( MemberList.Source )
				.ConstructUsing( source => JsonSerializer.Deserialize<Group>( source.Settings, Consts.JsonSerializerOptions ) )
				.ForSourceMember( x => x.Settings, opt => opt.DoNotValidate() );

			CreateMap<Group, GroupViewModel>()
				.ForMember( x => x.Id, opt => opt.MapFrom( source => source.UId ) )
				.ForMember( x => x.Questions, opt => opt.Ignore() );
		}

		private void AddAttemptMapping() {
			CreateMap<QuizAttempt, AttemptDto>().ConvertUsing<QuizAttemptToAttemptDtoConverter>();
		}
	}
}
