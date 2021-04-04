using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using MyCollege.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Data.Repositories
{
    public class StudentRepository
        : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(Context dbContext)
            : base(dbContext)
        {

        }

        public List<Student> SearchStudentsBy(int courseId, int studentid)
        {
            var query = (from student in _dbContext.Students.AsNoTracking()
                         select student);

            if (courseId > 0)
            {
                query = (from student in _dbContext.Students.AsNoTracking()
                         join grade in _dbContext.Grades.AsNoTracking()
                            on student.Id equals grade.StudentId
                         join subject in _dbContext.Subjects.AsNoTracking()
                            on grade.SubjectId equals subject.Id
                         where subject.CourseId == courseId
                         select student);
            }
            if (studentid > 0)
            {
                query = (from student in _dbContext.Students.AsNoTracking()
                         where student.Id == studentid
                         select student);
            }

            return query.ToList();
        }
        public Student GetByName(string name)
        {
            return _dbContext.Students.FirstOrDefault(e => e.Name == name);
        }

        public bool Remove(int studentId)
        {
            var target = _dbContext.Students
                .FirstOrDefault(c => c.Id == studentId);

            var result = _dbContext.Set<Student>().Remove(target);
            _dbContext.SaveChanges();
            return true;
        }

        public int Update(Student newStudent)
        {
            if (newStudent == null)
            {
                return 0;
            }
            var tracked = _dbContext.Students.FirstOrDefault(c => c.Id == newStudent.Id);
            tracked.Name = newStudent.Name;
            tracked.Birthdate = newStudent.Birthdate;
            tracked.RegistrationNumber = newStudent.RegistrationNumber;
            return _dbContext.SaveChanges();
        }
    }
}
