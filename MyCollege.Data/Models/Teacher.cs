using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollege.Data.Models
{
    [Table("Teachers")]
    public class Teacher
        : BaseEntity
    {
        public DateTime Birthdate { get; set; }
        public decimal Salary { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
