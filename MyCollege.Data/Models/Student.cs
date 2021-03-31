using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollege.Data.Models
{
    [Table("Students")]
    public class Student 
        : BaseEntity
    {
        public DateTime Birthdate { get; set; }
        public int RegistrationNumber { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
