using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBuilder.Model.Model.Default
{
    [Table("Quiz")]
    public class Quiz : AuditableEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
