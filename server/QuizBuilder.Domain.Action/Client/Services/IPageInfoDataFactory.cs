using System.Collections.Generic;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Client.Services {

	public interface IPageInfoDataFactory {

		List<PageInfo> Create( Quiz quiz, List<Group> groups, List<Question> questions );

	}
}
