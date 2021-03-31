using MyCollege.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MyCollege.Data.Migrations
{

    public class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context context)
        {
            base.Seed(context);

            Random random = new Random();

            var students = new List<Student>
            {
                new Student { Id =1, Name = "Homer Simpson",   Birthdate = DateTime.Now.AddYears(-18),
                    RegistrationNumber = random.Next(100) },
                new Student {Id = 2, Name = "Gustavo",   Birthdate = DateTime.Now.AddYears(-28),
                    RegistrationNumber = random.Next(100) },
                new Student {Id = 3, Name = "Thomas Shelby",   Birthdate = DateTime.Now.AddYears(-38),
                    RegistrationNumber = random.Next(100) },
                new Student {Id = 4, Name = "Walter White",   Birthdate = DateTime.Now.AddYears(-48),
                    RegistrationNumber = random.Next(100) },
                new Student {Id = 5, Name = "Mr Nobody",   Birthdate = DateTime.Now.AddYears(-20),
                    RegistrationNumber = random.Next(100) },
            };
            
            
            var courses = new List<Course>
            {
                new Course {Id = 1, Name = "Engineering"},
                new Course {Id = 2, Name = "Economics"},
                new Course {Id = 3, Name = "Design"}
            };

            var teachers = new List<Teacher>
            {
                new Teacher {Id = 1, Name = "Skinner", Birthdate = DateTime.Now.AddYears(-78),
                    Salary = 10000.0m },
                new Teacher {Id = 2, Name = "Edna Krabappe", Birthdate = DateTime.Now.AddYears(-28),
                    Salary = 1000.0m },
                new Teacher {Id = 3, Name = "Mr. Johnson", Birthdate = DateTime.Now.AddYears(-48),
                    Salary = 4500.0m },
            };

            var subjects = new List<Subject>
            {
                new Subject {Id = 1, Name = "Math", CourseId = 1, TeacherId = 1},
                new Subject {Id = 2, Name = "Calculus", CourseId = 1, TeacherId = 2},
                new Subject {Id = 3, Name = "Physics", CourseId = 1, TeacherId = 2},
                new Subject {Id = 4, Name = "How to Engineering", CourseId = 1, TeacherId = 3},

                new Subject {Id = 5, Name = "Economic history", CourseId = 2, TeacherId = 3},
                new Subject {Id = 6, Name = "International trade", CourseId = 2, TeacherId = 1},
                new Subject {Id = 7, Name = "Money and banking", CourseId = 2, TeacherId = 1},

                new Subject {Id = 8, Name = "Art", CourseId = 3, TeacherId = 2},
                new Subject {Id = 9, Name = "Bootstrap 4", CourseId = 3, TeacherId = 2},
                new Subject {Id = 10, Name = "Fashion Studies", CourseId = 3, TeacherId = 3},
            };

            var grades = new List<Grade>
            {
                new Grade {Id = 1, StudentId = 1, SubjectId = 1, Value = 6},
                new Grade {Id = 2, StudentId = 1, SubjectId = 2, Value = 5},
                new Grade {Id = 3, StudentId = 1, SubjectId = 3, Value = 8},
                new Grade {Id = 4, StudentId = 1, SubjectId = 4, Value = 10},


                new Grade {Id = 5, StudentId = 2, SubjectId = 5, Value = 10},
                new Grade {Id = 6, StudentId = 2, SubjectId = 6, Value = 10},
                new Grade {Id = 7, StudentId = 2, SubjectId = 7, Value = 10},

                new Grade {Id = 8,  StudentId = 3, SubjectId = 8, Value = 10},
                new Grade {Id = 9,  StudentId = 3, SubjectId = 9, Value = 10},
                new Grade {Id = 10, StudentId = 3, SubjectId = 10, Value = 8},

                new Grade {Id = 11, StudentId = 4, SubjectId = 1, Value = 10},
                new Grade {Id = 12, StudentId = 4, SubjectId = 2, Value = 4},
                new Grade {Id = 13, StudentId = 4, SubjectId = 3, Value = 6},
                new Grade {Id = 14, StudentId = 4, SubjectId = 4, Value = 7},

                new Grade {Id = 15, StudentId = 5, SubjectId = 5, Value = 10},
                new Grade {Id = 16, StudentId = 5, SubjectId = 6, Value = 1.0m},
                new Grade {Id = 17, StudentId = 5, SubjectId = 7, Value = 5.6m},
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.Id, s));
            teachers.ForEach(s => context.Teachers.AddOrUpdate(p => p.Id, s));
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Id, s));
            subjects.ForEach(s => context.Subjects.AddOrUpdate(p => p.Id, s));
            grades.ForEach(s => context.Grades.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

        }
    }
}