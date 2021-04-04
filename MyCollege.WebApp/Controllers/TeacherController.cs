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
    [RoutePrefix("Home/api/Teacher")]
    public class TeacherController : ApiController
    {        
        private readonly ISubjectService _subjectService;
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        public TeacherController(ISubjectService subjectService,
            ITeacherService teacherService,
            IMapper mapper)
        {
            _subjectService = subjectService;
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Overview")]
        public async Task<IHttpActionResult> GetOverview()
        {
            var response = new OverviewResponse<TeacherOverview>();
            var overviews = GetTeacherOverview();
            response.IsSuccess = !response.Errors.Any();
            response.Overview = overviews;

            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add([FromBody] TeacherDTO teacher)
        {
           
            var response = new OverviewResponse<TeacherOverview>();
            try
            {
                Teacher newTeacher = _mapper.Map<TeacherDTO, Teacher>(teacher);
                var result = _teacherService.Add(newTeacher);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Already exists!");
                    return Ok(response);
                }

                var overviews = GetTeacherOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetTeacherOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadTeacher(response);
            return Ok(response);
        }
        
        [HttpPut]
        [Route("Update")]
        public async Task<IHttpActionResult> Update([FromBody] TeacherDTO subject)
        {
            
            var response = new OverviewResponse<TeacherOverview>();
            try
            {
                Teacher newTeacher = _mapper.Map<TeacherDTO, Teacher>(subject);
                var result = _teacherService.Update(newTeacher);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Teacher was not saved");
                    return Ok(response);
                }
        
                var overviews = GetTeacherOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetTeacherOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadTeacher(response);
            return Ok(response);
        }
        
        [HttpDelete]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete([FromUri] int teacherId)
        {
            var response = new OverviewResponse<TeacherOverview>();
            try
            {
                var hasAssociation = _subjectService.GetSubjectsList(0, teacherId, 0).Any();

                if (hasAssociation)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Cannot remove teacher associated with subjects.");
                    return Ok(response);
                }

                var result = _teacherService.Delete(teacherId);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Teacher was not removed");
                    return Ok(response);
                }
        
                var overviews = GetTeacherOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetTeacherOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadTeacher(response);
            return Ok(response);
        }
        
        public List<TeacherOverview> GetTeacherOverview()
        {
            var teacherEntries = _teacherService.GetTeachersBy();
            List<TeacherOverview> overviews = new List<TeacherOverview>();
        
            foreach (var teacher in teacherEntries)
            {
                overviews.Add(new TeacherOverview
                {
                    Teacher = _mapper.Map<Teacher, TeacherDTO>(teacher)              
                });
            }
        
            return overviews;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IHttpActionResult> GetList()
        {
            var teachersList = _teacherService.GetTeachersBy();
            var teachersDTO = new List<TeacherDTO>();

            foreach(var teacher in teachersList)
            {
                teachersDTO.Add(_mapper.Map<Teacher, TeacherDTO>(teacher));
            }
            return Ok(teachersDTO);
        }

    }
}
