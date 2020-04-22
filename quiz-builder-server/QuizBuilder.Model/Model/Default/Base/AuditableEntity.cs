using System;
using System.ComponentModel.DataAnnotations;

namespace QuizBuilder.Model.Model.Default.Base
{
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }
    }
}
