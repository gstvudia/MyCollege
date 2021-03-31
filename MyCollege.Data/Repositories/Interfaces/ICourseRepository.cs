using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;

namespace MyCollege.Data.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        int UpdateCourse(Course newCourse);
    }
}