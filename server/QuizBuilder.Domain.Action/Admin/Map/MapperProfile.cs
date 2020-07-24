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

namespace QuizBuilder.Domain.Action.Admin.Map {
	public sealed class MapperProfile : Profile {
		public MapperProfile() {
			AddQuizMapping();
			AddGroupMapping();
			AddQuestionMapping();
			AddAttemptMapping();
		}

		private void AddQuizMapping() {
			CreateMap<Quiz, Quiz>().ConvertUsing<QuizToQuizConverter>();
			CreateMap<Quiz, QuizDto>().ConvertUsing<QuizToQuizDtoConverter>();
			CreateMap<QuizDto, Quiz>().ConvertUsing<QuizDtoToQuizConverter>();
			CreateMap<Quiz, QuizViewModel>().ConvertUsing<QuizToQuizViewModelConverter>();
			CreateMap<UpdateQuizCommand, Quiz>().ConvertUsing<UpdateQuizCommandToQuizConverter>();
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
			CreateMap<Group, GroupDto>().ConvertUsing<GroupToGroupDtoConverter>();
			CreateMap<GroupDto, Group>(MemberList.Source);
			CreateMap<Group, GroupViewModel>().ConvertUsing<GroupToGroupViewModelConverter>();
		}

		private void AddAttemptMapping() {
			CreateMap<QuizAttempt, AttemptDto>().ConvertUsing<QuizAttemptToAttemptDtoConverter>();
		}
	}
}
