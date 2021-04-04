using AutoMapper;
using Microsoft.AspNet.SignalR;
using MyCollege.Data.Models;
using MyCollege.Services.Interfaces;
using MyCollege.WebApp.Hubs;
using MyCollege.WebApp.Models;
using MyCollege.WebApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyCollege.WebApp.Controllers
{
    [RoutePrefix("Home/api/Course")]
    public class CourseController : ApiController
    {
        // GET api/values
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService,
            ITeacherService teacherService,
            IStudentService studentService,
            IGradeService gradeService,
            IMapper mapper)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _studentService = studentService;
            _gradeService = gradeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Overview")]
        public async Task<IHttpActionResult> GetOverview()
        {
            var response = new OverviewResponse<CourseOverview>();
            var overviews = GetCourseOverview();
            response.IsSuccess = !response.Errors.Any();
            response.Overview = overviews;

            return Ok(response);
        }

        [HttpPost]        
        [Route("Add")]
        public async Task<IHttpActionResult> Add([FromBody] CourseDTO course)
        {
            
            var response = new OverviewResponse<CourseOverview>();
            try
            {
                Course newCourse = _mapper.Map<CourseDTO, Course>(course);
                var result = _courseService.Add(newCourse);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Already exists!");
                    response.Overview = GetCourseOverview();
                    return Ok(response);
                }
                var associated = _courseService.AssociateSubjects(result.Id, course.SelectedSubjects);
                if (!associated)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Associating course to subjects did not went well");
                    
                }

                var overviews = GetCourseOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetCourseOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadCourse(response);
            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IHttpActionResult> Update([FromBody] CourseDTO course)
        {
            
            var response = new OverviewResponse<CourseOverview>();
            try
            {
                Course newCourse = _mapper.Map<CourseDTO, Course>(course);
                var result = _courseService.Update(newCourse);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Course was not saved");
                    response.Overview = GetCourseOverview();
                    return Ok(response);
                }
                var associated = _courseService.AssociateSubjects(newCourse.Id, course.SelectedSubjects);
                if (!associated)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Associating course to subjects did not went well");
                }

                var overviews = GetCourseOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetCourseOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadCourse(response);
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete([FromUri] int courseId)
        {           
            var response = new OverviewResponse<CourseOverview>();
            try
            {
                var result = _courseService.Delete(courseId);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Course was not removed");
                }

                var overviews = GetCourseOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetCourseOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadCourse(response);
            return Ok(response);
        }

        public List<CourseOverview> GetCourseOverview()
        {
            var courseEntries = _courseService.GetCourses();
            List<CourseOverview> overviews = new List<CourseOverview>();

            foreach (var course in courseEntries)
            {
                overviews.Add(new CourseOverview
                {
                    Course = _mapper.Map<Course, CourseDTO>(course),
                    TeachersCount = _teacherService.GetTeachersBy(course.Id).Count(),
                    StudentsCount = _studentService.SearchStudentsBy(course.Id).Count(),
                    AverageGrade = String.Format("{0:0.0#}", _gradeService.GetAverageByCourse(course.Id))
                });
            }

            return overviews;
        }
    }
}
