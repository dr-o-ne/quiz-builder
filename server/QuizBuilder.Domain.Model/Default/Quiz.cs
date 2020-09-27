using System;
using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Base;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Model.Default {

	public sealed class Quiz : AuditableEntity<long> {

		[JsonIgnore]
		public string UId { get; set; }

		[JsonIgnore]
		public string Name { get; set; }

		[JsonIgnore]
		public bool IsEnabled { get; set; }

		public bool IsIntroductionEnabled { get; set; }

		public string Introduction { get; set; }

		public bool IsPrevButtonEnabled { get; set; }

		public PageSettings PageSettings { get; set; }

		public bool RandomizeGroups { get; set; }

		public bool RandomizeQuestions { get; set; }

		public long QuestionsPerPage { get; set; }

		public bool IsScheduleEnabled { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public bool IsValid() {

			if( StartDate != null && EndDate != null && StartDate.Value > EndDate.Value )
				return false;

			return true;
		}

		public bool IsAvailable() {

			if( !IsEnabled ) return false;

			if( IsScheduleEnabled ) {
				if( StartDate != null && StartDate > DateTime.UtcNow )
					return false;
				if( EndDate != null && EndDate < DateTime.UtcNow )
					return false;
			}

			return true;
		}

		// Appearance

		public string HeaderColor { get; set; }

		public string BackgroundColor { get; set; }

		public string SideColor { get; set; }

		public string FooterColor { get; set; }

	}
}
