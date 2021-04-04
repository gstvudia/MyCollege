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
    [RoutePrefix("Home/api/Grade")]
    public class GradeController : ApiController
    {        
        private readonly IGradeService _GradeService;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;
        public GradeController(IGradeService GradeService,
            IGradeService gradeService,
            IMapper mapper)
        {
            _GradeService = GradeService;
            _gradeService = gradeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Overview")]
        public async Task<IHttpActionResult> GetOverview()
        {
            var response = new OverviewResponse<GradeOverview>();
            var overviews = GetGradeOverview();
            response.IsSuccess = !response.Errors.Any();
            response.Overview = overviews;
            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IHttpActionResult> Add([FromBody] GradeDTO grade)
        {
            
            var response = new OverviewResponse<GradeOverview>();
            try
            {
                Grade newGrade = _mapper.Map<GradeDTO, Grade>(grade);
                var result = _gradeService.Add(newGrade);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Already exists!");
                }

                var overviews = GetGradeOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetGradeOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadGrade(response);
            return Ok(response);
        }
        
        [HttpPut]
        [Route("Update")]
        public async Task<IHttpActionResult> Update([FromBody] GradeDTO grade)
        {
            var response = new OverviewResponse<GradeOverview>();
            try
            {
                Grade newGrade = _mapper.Map<GradeDTO, Grade>(grade);
                
                var result = _gradeService.Update(newGrade);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("Grade was not saved");
                }
                var overviews = GetGradeOverview();
                response.IsSuccess = !response.Errors.Any();
                response.Overview = overviews;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Errors.Add(ex.Message);
                response.Errors.Add("-----------------------------------");
                response.Errors.Add(ex.InnerException.Message);
                var overviews = GetGradeOverview();
                response.Overview = overviews;
                return Ok(response);
            }

            //Call overview         
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            hubContext.Clients.All.reloadGrade(response);
            return Ok(response);
        }

        public List<GradeOverview> GetGradeOverview()
        {
            var gradeEntries = _gradeService.SearchGradesBy();
            List<GradeOverview> overviews = new List<GradeOverview>();
        
            foreach (var grade in gradeEntries)
            {
                overviews.Add(new GradeOverview
                {
                    Grade = _mapper.Map<Grade, GradeDTO>(grade)             
                });
            }
        
            return overviews;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IHttpActionResult> GetList([FromUri] int GradeId = 0, [FromUri] int teacherid = 0)
        {
            var gradesList = _gradeService.SearchGradesBy();
            var gradesDTO = new List<GradeDTO>();

            foreach(var grade in gradesList)
            {
                gradesDTO.Add(_mapper.Map<Grade, GradeDTO>(grade));
            }
            return Ok(gradesDTO);
        }

    }
}
