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
            await _applicationService.ChangeApplicationStatusAsync(new ChangeAppStatusCommand(id,appId,command.Status));
            return Ok(new BaseResponseDto("Changed succesfully"));

        }



    }
}
