using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Presentation.Dtos;
using JobBoardPlatform.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensibility;
using System.Security.Claims;

namespace JobBoardPlatform.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {

        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [HttpGet("GetByJobAdId/{JobAdId:Guid}")]
        [Authorize(Policy = "EmployerOrAdmin")]
        public async Task<ActionResult<BaseResponseDto>> GetApplicationByJobAdId([FromRoute] Guid JobAdId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result =await _applicationService.GetAppsByJObAdId(new ApplicationJobAdCommand { JobAdId = JobAdId,RequesterId = id});

            return Ok(new BaseResponseDto(result));
        }
        [HttpGet("{AppId:guid}")]
        [Authorize(Policy = "EmployerOrAdmin")]

        public async Task<ActionResult<BaseResponseDto>> GetApplicationDetail([FromRoute] Guid AppId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var result =await _applicationService.GetDetailApp(new AppDetailCommand { AppId = AppId,RequesterId = id});
            return Ok(new BaseResponseDto(result));

        }
        [HttpPut("ChangeApplicationState/{appId:guid}")]
        [Authorize(Policy = "EmployerOrAdmin")]

        public async Task<ActionResult<BaseResponseDto>> ChangeAppState([FromRoute] Guid appId, [FromBody] AppStatusCommand command)
        {

            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result =await _applicationService.ChangeApplicationStatusAsync(new ChangeAppStatusCommand(id,appId,command.Status));
            return Ok(new BaseResponseDto(result));
        }
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<BaseResponseDto>> MakeApplication([FromBody] CreateAppCmd cmd)
        {

            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var command = new CreateAppCommand(cmd.UserId,id,cmd.jobAdID,cmd.Note);
            var result = await _applicationService.CreateApplicationAsync(command);
            return Ok(new BaseResponseDto(result));
        }
        [HttpGet("GetByUserId/{userId:guid}")]
        [Authorize]

        public async Task<ActionResult<BaseResponseDto>> GetMyApps([FromRoute] Guid userId )
        {

            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result = await _applicationService.GetAppsForJobSeekerAsync(userId,id);
            return Ok(new BaseResponseDto(result));
        }
        [HttpGet("GetMyAppDetail/{appId:guid}")]
        [Authorize]

        public async Task<ActionResult<BaseResponseDto>> GetAppDetailForJobSeekerAsync([FromRoute] Guid appId )
        {

            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result = await _applicationService.GetAppDetailForJobSeekerAsync(id,appId);
            return Ok(new BaseResponseDto(result));
        }

        [HttpPatch("{appId:guid}")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> CancellMyApp([FromRoute] Guid appId)
        {

            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result = await _applicationService.CancellMyApp(id,appId);
            return Ok(new BaseResponseDto(result));

        }



    }
}
