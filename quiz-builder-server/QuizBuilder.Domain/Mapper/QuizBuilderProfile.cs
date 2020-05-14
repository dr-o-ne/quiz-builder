using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Mapper.Default;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Mapper {
	public sealed class QuizBuilderProfile : Profile {
		public QuizBuilderProfile() {
			AddQuizMapping();
			AddQuestionMapping();
		}

		private void AddQuizMapping() {
			CreateMap<Quiz, Quiz>().ConvertUsing<QuizToQuizConverter>();
			CreateMap<Quiz, QuizDto>().ConvertUsing<QuizToQuizDtoConverter>();
			CreateMap<QuizDto, Quiz>().ConvertUsing<QuizDtoToQuizConverter>();
			CreateMap<Quiz, QuizViewModel>().ConvertUsing<QuizToQuizViewModelConverter>();
			CreateMap<CreateQuizCommand, Quiz>().ConvertUsing<CreateQuizCommandToQuizConverter>();
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
	}
}
