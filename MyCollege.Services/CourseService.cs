using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        public List<Course> GetCoursesOverview()
        {
            var courses = _courseRepository.GetAll();
                

            return courses;
        }
    }
}
