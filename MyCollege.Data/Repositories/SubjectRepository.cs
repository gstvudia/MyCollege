using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using MyCollege.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Data.Repositories
{
    public class SubjectRepository
        : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(Context dbContext)
            : base(dbContext)
        {

        }

        public List<Subject> SearchBy(int courseId, int teacherid, int studentId)
        {
            var query = (from subject in _dbContext.Subjects.AsNoTracking()
                        select subject);

            if(courseId > 0)
            {
                query = (from subject in _dbContext.Subjects.AsNoTracking()
                         where subject.CourseId == courseId ||
                         subject.CourseId == null
                         select subject);
            }
            if (teacherid > 0)
            {
                query = (from subject in _dbContext.Subjects.AsNoTracking()
                         where subject.TeacherId == teacherid
                         select subject);
            }
            if (studentId > 0)
            {
                query = (from subject in _dbContext.Subjects.AsNoTracking()
                         join grade in _dbContext.Grades.AsNoTracking()
                            on subject.Id equals grade.SubjectId
                         where grade.StudentId == studentId
                         select subject);
            }

            return query.ToList();
        }
        public Subject GetByName(string name)
        {
            return _dbContext.Subjects.FirstOrDefault(e => e.Name == name);
        }
        public List<Subject> GetByCourseId (int courseId)
        {
            return (from subject in _dbContext.Subjects
                   where subject.CourseId == courseId
                   select subject).ToList();

        }

        public List<Subject> GetListById(int[] subjectsId)
        {
            var a = (from subject in _dbContext.Subjects.AsNoTracking()
                    where subjectsId.Any(id => subject.Id == id)
                    select subject).ToList(); 
            return (from subject in _dbContext.Subjects.AsNoTracking()
                    where subjectsId.Any(id => subject.Id == id)
                    select subject).ToList();
        }

        public List<Subject> GetAvailable(int? courseId)
        {
            return (from subject in _dbContext.Subjects.AsNoTracking()
                    where subject.CourseId == courseId
                    select subject).ToList();

        }

        public bool Remove(int subjectId)
        {
            var target = _dbContext.Subjects
                .FirstOrDefault(c => c.Id == subjectId);

            var result = _dbContext.Set<Subject>().Remove(target);
            _dbContext.SaveChanges();
            return true;
        }

        public int Update(Subject newSubject)
        {
            if (newSubject == null)
            {
                return 0;
            }
            var tracked = _dbContext.Subjects.FirstOrDefault(c => c.Id == newSubject.Id);
            tracked.Name = newSubject.Name;
            tracked.CourseId = newSubject.CourseId;
            tracked.TeacherId = newSubject.TeacherId;
            return _dbContext.SaveChanges();
        }
    }
}
