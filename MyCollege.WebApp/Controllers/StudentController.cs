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
    [RoutePrefix("Home/api/Student")]
    public class StudentController : ApiController
    {        
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService,
            ITeacherService teacherService,
            IGradeService gradeService,
            IMapper mapper)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _gradeService = gradeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Overview")]
        public async Task<IHttpActionResult> GetOverview()
        {
            var response = new OverviewResponse<StudentOverview>();
            var overviews = GetStudentOverview();
            response.IsSuccess = !response.Errors.Any();
            response.Overview = overviews;

            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add([FromBody] StudentDTO student)
        {
            
            var response = new OverviewResponse<StudentOverview>();
            try
            {
                Student newStudent = _mapper.Map<StudentDTO, Student>(student);
                var result = _studentService.Add(newStudent);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Already exists!");
                }

                var overviews = GetStudentOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetStudentOverview();
                response.Overview = overviews;
                return Ok(response);
            }
        
            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadStudent(response);
            return Ok(response);
        }
        
        [HttpPut]
        [Route("Update")]
        public async Task<IHttpActionResult> Update([FromBody] StudentDTO Student)
        {
            
            var response = new OverviewResponse<StudentOverview>();
            try
            {
                Student newStudent = _mapper.Map<StudentDTO, Student>(Student);
                var result = _studentService.Update(newStudent);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Student was not saved");
                }
                var overviews = GetStudentOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetStudentOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadStudent(response);
            return Ok(response);
        }
        
        [HttpDelete]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete([FromUri] int StudentId)
        {
            var response = new OverviewResponse<StudentOverview>();
            try
            {
                var result = _studentService.Delete(StudentId);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Student was not removed");
                }
        
                var overviews = GetStudentOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetStudentOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadStudent(response);
            return Ok(response);
        }
        
        public List<StudentOverview> GetStudentOverview()
        {
            var studentEntries = _studentService.GetStudents();
            List<StudentOverview> overviews = new List<StudentOverview>();
        
            foreach (var Student in studentEntries)
            {
                overviews.Add(new StudentOverview
                {
                    Student = _mapper.Map<Student, StudentDTO>(Student)             
                });
            }
        
            return overviews;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IHttpActionResult> GetList([FromUri] int StudentId = 0, [FromUri] int teacherid = 0)
        {
            var StudentsList = _studentService.GetStudents();
            var StudentsDTO = new List<StudentDTO>();

            foreach(var Student in StudentsList)
            {
                StudentsDTO.Add(_mapper.Map<Student, StudentDTO>(Student));
            }
            return Ok(StudentsDTO);
        }

    }
}
