using MyCollege.Data.Models;
using MyCollege.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyCollege.WebApp.Controllers
{
    public class CourseController : ApiController
    {
        // GET api/values
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public string[] Get()
        {
            var courseOverview = _courseService.GetCoursesOverview();
               
            return courseOverview.Select(c=>c.Name).ToArray();
        }
    }
}
