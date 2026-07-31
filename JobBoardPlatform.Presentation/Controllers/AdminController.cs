using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobBoardPlatform.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController:ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Company/Activate/{companyId:guid}")]
        public async Task<ActionResult<BaseResponseDto>> ApproveCompany([FromRoute] Guid companyId)
        {
            var result = await _adminService.SetApprovedCompanyAsync(companyId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("Company/Deactivate/{companyId:guid}")]
        public async Task<ActionResult<BaseResponseDto>> DeApproveCompany([FromRoute] Guid companyId)
        {
            var result = await _adminService.SetNotApprovedCompanyAsync(companyId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Users/Employers")]
        public async Task<ActionResult<BaseResponseDto>> GetEmployersAsync([FromQuery] Paging? paging)
        {
            var result = await _adminService.GetEmployerAsync(paging);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Company/{companyId:guid}")]
        public async Task<ActionResult<BaseResponseDto>> GetEmployerDetailAsync([FromRoute] Guid companyId)
        {
            var user = User;
            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var command = new GetCompanyDetailCommand() { CompanyId = companyId,RequesterId = id };
            var result = await _adminService.GetMyCompanyDetailAsync(command);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Users/JobSeekers")]
        public async Task<ActionResult<BaseResponseDto>> GetJobSeekersAsync([FromQuery] Paging? paging)
        {
            var result = await _adminService.GetJobSeekers(paging);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Users/JobSeekers/{userId:guid}")]
        public async Task<ActionResult<BaseResponseDto>> GetJobSeekerDetailAsync([FromRoute] Guid userId)
        {
            var user = User;
            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var result = await _adminService.GetJobseekerDetailAsync(userId,id);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("Users/JobSeekers/Activate/{userId:guid}")]
        public async Task<ActionResult<BaseResponseDto>> ActivateJobSeekerAsync([FromRoute] Guid userId)
        {
            var result = await _adminService.ActivateUser(userId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("Users/JobSeekers/DeActivate/{userId:guid}")]
        public async Task<ActionResult<BaseResponseDto>> DeActivateJobSeekerAsync([FromRoute] Guid userId)
        {
            var result = await _adminService.DeactivateUser(userId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("JObAds")]
        public async Task<ActionResult<BaseResponseDto>> GetJobAds([FromQuery] Paging? paging)
        {
            var result = await _adminService.GetAllJobAds(paging);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("JObAds/{jobAdId:guid}/Activate")]
        public async Task<ActionResult<BaseResponseDto>> ActivateJobAd([FromRoute] Guid jobAdId)
        {
            var result = await _adminService.ActivateJobAd(jobAdId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("JObAds/{jobAdId:guid}/Archivate")]
        public async Task<ActionResult<BaseResponseDto>> ArchivejobAd([FromRoute] Guid jobAdId)
        {
            var result = await _adminService.ArchiveJobAd(jobAdId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("JObAds/{jobAdId:guid}/Close")]
        public async Task<ActionResult<BaseResponseDto>> CloseJobAd([FromRoute] Guid jobAdId)
        {
            var result = await _adminService.CloseJobAd(jobAdId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("JObAds/{jobAdId:guid}/MakePlus")]
        public async Task<ActionResult<BaseResponseDto>> MakeplusJobAd([FromRoute] Guid jobAdId)
        {
            var result = await _adminService.MakePlusJobAd(jobAdId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("JObAds/{jobAdId:guid}/MakePro")]
        public async Task<ActionResult<BaseResponseDto>> MakeProJobAd([FromRoute] Guid jobAdId)
        {
            var result = await _adminService.MakeProJobAd(jobAdId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("JObAds/{jobAdId:guid}/MakeNormal")]
        public async Task<ActionResult<BaseResponseDto>> MakeNormalJobAd([FromRoute] Guid jobAdId)
        {
            var result = await _adminService.MakeNormalJobAd(jobAdId);
            return Ok(new BaseResponseDto(result));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("DashBoard")]
        public async Task<ActionResult<BaseResponseDto>> GetDashBoard()
        {
            var result = await _adminService.GetAdminDashboardAsync();
            return Ok(new BaseResponseDto(result));
        }
    }
}
