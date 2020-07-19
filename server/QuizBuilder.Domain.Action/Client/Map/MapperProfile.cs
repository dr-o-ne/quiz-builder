using System.Collections.Generic;
using AutoMapper;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Action.Client.Map.Default;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Appearance;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.Client.Map {

	public sealed class MapperProfile : Profile {

		public MapperProfile() {
			CreateMap<Appearance, AppearanceInfo>();
			CreateMap<Quiz, SettingsInfo>();
			CreateMap<BinaryChoice, ChoiceAttemptInfo>();

			CreateMap<Question, QuestionAttemptInfo>().ConvertUsing<QuestionToQuestionAttemptInfoConverter>();
			CreateMap<Question, List<ChoiceAttemptInfo>>().ConvertUsing<QuestionToChoiceAttemptInfoConverter>();
		}

	}

}
