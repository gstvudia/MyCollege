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
    [RoutePrefix("Home/api/Subject")]
    public class SubjectController : ApiController
    {        
        private readonly ISubjectService _subjectService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;
        public SubjectController(ISubjectService subjectService,
            ITeacherService teacherService,
            IStudentService studentService,
            IGradeService gradeService,
            IMapper mapper)
        {
            _subjectService = subjectService;
            _teacherService = teacherService;
            _studentService = studentService;
            _gradeService = gradeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Overview")]
        public async Task<IHttpActionResult> GetOverview()
        {
            var response = new OverviewResponse<SubjectOverview>();
            var overviews = GetSubjectOverview();
            response.IsSuccess = !response.Errors.Any();
            response.Overview = overviews;
            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add([FromBody] SubjectDTO subject)
        {
            
            var response = new OverviewResponse<SubjectOverview>();
            try
            {
                Subject newsubject = _mapper.Map<SubjectDTO, Subject>(subject);
                newsubject.TeacherId = subject.SelectedTeacher;

                var result = _subjectService.Add(newsubject);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Already exists!");
                    return Ok(response);
                }

                var overviews = GetSubjectOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetSubjectOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadSubject(response);
            return Ok(response);
        }
        
        [HttpPut]
        [Route("Update")]
        public async Task<IHttpActionResult> Update([FromBody] SubjectDTO subject)
        {
            
            var response = new OverviewResponse<SubjectOverview>();
            try
            {
                Subject newsubject = _mapper.Map<SubjectDTO, Subject>(subject);
                newsubject.TeacherId = subject.SelectedTeacher;
                var result = _subjectService.Update(newsubject);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Subject was not saved");
                    return Ok(response);
                }
                var overviews = GetSubjectOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetSubjectOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadSubject(response);
            return Ok(response);
        }
        
        [HttpDelete]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete([FromUri] int subjectId)
        {
            var response = new OverviewResponse<SubjectOverview>();
            try
            {
                var result = _subjectService.Delete(subjectId);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Subject was not removed");
                    return Ok(response);
                }
        
                var overviews = GetSubjectOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetSubjectOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadSubject(response);
            return Ok(response);
        }
        
        public List<SubjectOverview> GetSubjectOverview()
        {
            var subjectEntries = _subjectService.GetSubjects();
            List<SubjectOverview> overviews = new List<SubjectOverview>();
        
            foreach (var subject in subjectEntries)
            {
                overviews.Add(new SubjectOverview
                {
                    Subject = _mapper.Map<Subject, SubjectDTO>(subject),                    
                    StudentsCount = _studentService.SearchStudentsBy(subject.Id).Count(),
                    AverageGrade = String.Format("{0:0.0#}", _gradeService.GetAverageBySubject(subject.Id))
                });
            }
        
            return overviews;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IHttpActionResult> GetList(
            [FromUri] int subjectId = 0,
            [FromUri] int teacherId = 0,
            [FromUri] int studentId = 0)
        {
            var subjectsList = _subjectService.GetSubjectsList(subjectId, teacherId, studentId);
            var subjectsDTO = new List<SubjectDTO>();

            foreach(var subject in subjectsList)
            {
                subjectsDTO.Add(_mapper.Map<Subject, SubjectDTO>(subject));
            }
            return Ok(subjectsDTO);
        }

    }
}
