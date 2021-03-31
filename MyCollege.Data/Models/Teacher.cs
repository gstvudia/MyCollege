using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollege.Data.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key,
           DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Salary { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
