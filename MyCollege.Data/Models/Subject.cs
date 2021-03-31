using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollege.Data.Models
{
    [Table("Subjects")]
    public class Subject
        : BaseEntity
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }


        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
