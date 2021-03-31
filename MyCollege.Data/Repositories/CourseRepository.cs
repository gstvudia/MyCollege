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

        public int UpdateCourse(Course newCourse)
        {
            if (newCourse == null)
            {
                //do smth
            }
            var tracked = _dbContext.Courses.FirstOrDefault(c => c.Id == newCourse.Id);
            tracked = newCourse;
            return _dbContext.SaveChanges();
        }
    }
}
