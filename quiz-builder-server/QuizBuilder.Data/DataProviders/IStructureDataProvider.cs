﻿using System.Threading.Tasks;

namespace QuizBuilder.Data.DataProviders {

	public interface IStructureDataProvider {

		Task AddQuizQuestionRelationship( long quizId, long quizItemId );

		Task AddQuizGroupRelationship( long quizId, long groupId );

		Task AddGroupQuestionRelationship( long groupId, long questionId );

	}
}
