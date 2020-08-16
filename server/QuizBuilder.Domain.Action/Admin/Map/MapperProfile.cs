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
using QuizBuilder.Utils.Utils;

namespace QuizBuilder.Domain.Action.Admin.Map {

	public sealed class MapperProfile : Profile {

		public MapperProfile() {
			AddUserMapping();
			AddQuizMapping();
			AddGroupMapping();
			AddQuestionMapping();
			AddAttemptMapping();
		}

		private void AddUserMapping() {
			CreateMap<RegisterUserCommand, UserDto>( MemberList.Source )
				.ForSourceMember( x => x.Password, opt => opt.DoNotValidate() )
				.ForMember( x => x.UserName, map => map.MapFrom( x => x.Email ) );
		}

		private void AddQuizMapping() {

			CreateMap<UpdateQuizCommand, Quiz>( MemberList.Source )
				.ForMember( x => x.StartDate, opt => opt.MapFrom( x => Converter.FromUnixTimeSeconds( x.StartDate ) ) )
				.ForMember( x => x.EndDate, opt => opt.MapFrom( x => Converter.FromUnixTimeSeconds( x.EndDate ) ) );

			CreateMap<Quiz, QuizDto>()
				.ForMember( x => x.Settings, opt => opt.MapFrom( source => JsonSerializer.Serialize( source, Consts.JsonSerializerOptions ) ) );

			CreateMap<QuizDto, Quiz>( MemberList.Source )
				.ConstructUsing( source => JsonSerializer.Deserialize<Quiz>( source.Settings, Consts.JsonSerializerOptions ) )
				.ForSourceMember( x => x.Settings, opt => opt.DoNotValidate() );

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
				.ForMember( x => x.Questions, opt => opt.Ignore() );

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
