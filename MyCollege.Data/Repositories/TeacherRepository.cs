using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using MyCollege.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Data.Repositories
{
    public class TeacherRepository
        : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(Context dbContext)
            : base(dbContext)
        {

        }

        public List<Teacher> GetTeachersBy(int courseId, int teacherid)
        {
            var query = (from teacher in _dbContext.Teachers.AsNoTracking()
                         select teacher);

            if (courseId > 0)
            {
                query = (from teacher in _dbContext.Teachers.AsNoTracking()
                         join subject in _dbContext.Subjects.AsNoTracking()
                            on teacher.Id equals subject.TeacherId
                          where subject.CourseId == courseId
                         select teacher);
            }
            if (teacherid > 0)
            {
                query = (from teacher in _dbContext.Teachers.AsNoTracking()
                         where teacher.Id == teacherid
                         select teacher);
            }

            return query.ToList();
        }

        public Teacher GetByName(string name)
        {
            return _dbContext.Teachers.FirstOrDefault(e => e.Name == name);
        }

        public bool Remove(int teacherId)
        {
            var target = _dbContext.Teachers
                .FirstOrDefault(c => c.Id == teacherId);

            var result = _dbContext.Set<Teacher>().Remove(target);
            _dbContext.SaveChanges();
            return true;
        }
        public int Update(Teacher newTeacher)
        {
            if (newTeacher == null)
            {
                return 0;
            }
            var tracked = _dbContext.Teachers.FirstOrDefault(c => c.Id == newTeacher.Id);
            tracked.Name = newTeacher.Name;
            tracked.Birthdate = newTeacher.Birthdate;
            tracked.Salary = newTeacher.Salary;
            return _dbContext.SaveChanges();
        }
    }
}
