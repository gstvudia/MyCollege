using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using MyCollege.Data.Repositories.Interfaces;
using System.Linq;

namespace MyCollege.Data.Repositories
{
    public class CourseRepository
        : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(Context dbContext)
            : base(dbContext)
        {

        }

        public Course  GetByName(string name)
        {
            return _dbContext.Courses.FirstOrDefault(e => e.Name == name);
        }

        public bool Remove(int courseId)
        {
            var target = _dbContext.Courses
                .FirstOrDefault(c => c.Id == courseId);

            var result = _dbContext.Set<Course>().Remove(target);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateCourse(Course newCourse)
        {
            if (newCourse == null)
            {
                //do smth
            }
            var tracked = _dbContext.Courses.FirstOrDefault(c => c.Id == newCourse.Id);
            tracked.Name = newCourse.Name;
            var result = _dbContext.SaveChanges();
            return result > 0;
        }
    }
}
