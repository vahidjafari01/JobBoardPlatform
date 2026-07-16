using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatform.Presentation.Dtos;
using JobBoardPlatform.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Employer")]

        public async Task<ActionResult<BaseResponseDto>> CreateJobAd([FromBody] CreateJobAdCommand cmd)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var command = new JobAdCreateCommand(cmd.Title,cmd.Description,cmd.Location,cmd.StartWorkTime,cmd.EndWorkTIme,cmd.SalaryMin,cmd.SalaryMax,cmd.EmployementType,cmd.CategoryId,cmd.CityId,id,cmd.Skils);
            return Ok(new BaseResponseDto(await _jobAdService.AddJobAd(command)));
        }
        [HttpPut("{jobAdId:guid}")]
        [Authorize(policy: "EmployerOrAdmin")]


        public async Task<ActionResult<BaseResponseDto>> EditJObAd([FromRoute] Guid jobAdId, [FromBody] JobAdEditCommand cmd)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var command = new JobEditCommand(cmd.Title,cmd.Description,cmd.Location,cmd.StartWorkTime,cmd.EndWorkTIme,cmd.SalaryMin,cmd.SalaryMax,cmd.EmployementType,cmd.Jobadstatus,cmd.CategoryId,cmd.CityId,id, jobAdId,cmd.Skils);
            await _jobAdService.UpdateJobAd(command);
            return Ok(new BaseResponseDto("updated succesfully"));
        }
        [HttpDelete("{JobAdId:guid}")]
        [Authorize(policy: "EmployerOrAdmin")]


        public async Task<ActionResult<BaseResponseDto>> DeleteJobAd([FromRoute] Guid JobAdId)
        {
            var user = User;
            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            await _jobAdService.DeleteJobAd(new JObAdDeleteCommand { JObAdID = JobAdId ,RequesterId = id});
            return Ok(new BaseResponseDto("Deleted succesfully"));
        }
        [HttpGet("CompanyJobAds/{companyId:guid}")]
        [Authorize(policy: "EmployerOrAdmin")]


        public async Task<ActionResult<BaseResponseDto>> GetMyCompanyJobAds([FromRoute]Guid companyId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var result = await _jobAdService.GetMyJobAds(new GetMyJobAdsCommand(id,companyId));
            return Ok(new BaseResponseDto(result));
        }
        [HttpGet("{jobadId:guid}")]
        [Authorize(policy: "EmployerOrAdmin")]


        public async Task<ActionResult<BaseResponseDto>> GetDetailJobAds([FromRoute] Guid jobAdId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var result = await _jobAdService.GetDetailJobAd(new GetJObAdDetailCommand(id,jobAdId));
            return Ok(new BaseResponseDto(result));
        }
        [HttpPut("Activate/{jobAdId:guid}")]
        [Authorize(policy: "EmployerOrAdmin")]

        public async Task<ActionResult<BaseResponseDto>> ActiveJobAd([FromRoute] Guid jobAdId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var cmd = new ActiveJobAdCommand { RequesterId = id,JobId = jobAdId};
            await _jobAdService.ActiveMyJObAd(cmd);
            return Ok(new BaseResponseDto("Activated succesfully"));
        }
        [HttpPut("Archived/{jobAdId:guid}")]
        [Authorize(policy: "EmployerOrAdmin")]

        public async Task<ActionResult<BaseResponseDto>> ArchiveJobAdId([FromRoute] Guid jobAdId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var cmd = new ArchiveMyJobAdCommand { RequesterId = id,JobId = jobAdId};
            await _jobAdService.ArchiveMyJobAd(cmd);
            return Ok(new BaseResponseDto("Archived succesfully"));
        }
       




    }
}
