using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using MyCollege.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Data.Repositories
{
    public class GradeRepository
        : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(Context dbContext)
            : base(dbContext)
        {

        }

        public List<Grade> SearchGradesBy(int courseId, int studentId , int subjectId)
        {
            var query = (from grade in _dbContext.Grades.AsNoTracking()
                         select grade);

            if (subjectId > 0)
            {
                query = (from grade in _dbContext.Grades.AsNoTracking()
                         join subject in _dbContext.Subjects.AsNoTracking()
                            on grade.SubjectId equals subject.Id
                         where subject.Id == subjectId
                         select grade);
            }
            if (studentId > 0)
            {
                query = (from grade in _dbContext.Grades.AsNoTracking()
                         join student in _dbContext.Students.AsNoTracking()
                            on grade.StudentId equals student.Id
                         where student.Id == studentId
                         select grade);
            }
            if (courseId > 0)
            {
                query = (from grade in _dbContext.Grades.AsNoTracking()
                         join subject in _dbContext.Subjects.AsNoTracking()
                            on grade.SubjectId equals subject.Id
                         where subject.CourseId == courseId
                         select grade);
            }

            return query.ToList();
        }

        public int Update(Grade newGrade)
        {
            if (newGrade == null)
            {
                return 0;
            }
            var tracked = _dbContext.Grades.FirstOrDefault(c => c.Id == newGrade.Id);
            tracked.Value = newGrade.Value;
            return _dbContext.SaveChanges();
        }
    }
}
