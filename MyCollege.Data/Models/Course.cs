using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollege.Data.Models
{
    [Table("Courses")]
    public class Course
        : BaseEntity
    {
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
