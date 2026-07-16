using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatform.Presentation.Dtos;
using JobBoardPlatform.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobBoardPlatform.Presentation.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]

    public class JobAdController:ControllerBase
    {
        private readonly IJobAdServices _jobAdService;

        public JobAdController(IJobAdServices jobAdService)
        {
            _jobAdService = jobAdService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponseDto>> CreateJobAd([FromBody] CreateJobAdCommand cmd)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var command = new JobAdCreateCommand(cmd.Title,cmd.Description,cmd.Location,cmd.StartWorkTime,cmd.EndWorkTIme,cmd.SalaryMin,cmd.SalaryMax,cmd.EmployementType,cmd.CategoryId,cmd.CityId,id,cmd.Skils);
            return Ok(new BaseResponseDto(await _jobAdService.AddJobAd(command)));
        }
        [HttpPut("{jobAdId:guid}")]
        public async Task<ActionResult<BaseResponseDto>> EditJObAd([FromRoute] Guid jobAdId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);



        }





    }
}
