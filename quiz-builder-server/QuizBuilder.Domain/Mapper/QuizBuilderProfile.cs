using AutoMapper;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Mapper.Default;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper {
	public sealed class QuizBuilderProfile : Profile {
		public QuizBuilderProfile() {
			AddQuizMapping();
			AddQuestionMapping();
		}

		private void AddQuizMapping() {
			CreateMap<Quiz, QuizDto>().ConvertUsing<QuizToQuizDtoConverter>();
			CreateMap<Quiz, Quiz>().ConvertUsing<QuizToQuizConverter>();
			CreateMap<QuizDto, Quiz>().ConvertUsing<QuizDtoToQuizConverter>();
			CreateMap<Quiz, QuizViewModel>().ConvertUsing<QuizToQuizViewModelConverter>();
			CreateMap<CreateQuizCommand, Quiz>().ConvertUsing<CreateQuizCommandToQuizConverter>();
			CreateMap<UpdateQuizCommand, Quiz>().ConvertUsing<UpdateQuizCommandToQuizConverter>();
		}

		private void AddQuestionMapping() {
			CreateMap<Question, QuestionDto>().ConvertUsing<QuestionToQuestionDtoConverter>();
			CreateMap<QuestionDto, Question>().ConvertUsing<QuestionDtoToQuestionConverter>();
			CreateMap<CreateQuestionCommand, Question>().ConvertUsing<CreateQuestionCommandToQuestionConverter>();
			CreateMap<UpdateQuestionCommand, Question>().ConvertUsing<UpdateQuestionCommandToQuestionConverter>();
		}
	}
}
