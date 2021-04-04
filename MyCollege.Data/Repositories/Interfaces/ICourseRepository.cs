using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;

namespace MyCollege.Data.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Course GetByName(string name);
        bool Remove(int courseId);
        bool UpdateCourse(Course newCourse);
    }
}