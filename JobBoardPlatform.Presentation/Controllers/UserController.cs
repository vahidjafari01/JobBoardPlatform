using Azure.Core.Pipeline;
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
    public class UserController:ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId:guid}")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> GetMyProfile([FromRoute] Guid userId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result = await _userService.GetmyProfile(userId,id);
            return Ok(new BaseResponseDto(result));
        }

        [HttpPut("{userId:guid}")]
        [Authorize]
        [Authorize(policy: "IsActive")]
        public async Task<ActionResult<BaseResponseDto>> EditMyProfile([FromRoute] Guid userId, [FromForm] UpdateProfileCmd cmd)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var command = new UpdateProfileCommand(cmd.FirsName,cmd.lastname,cmd.UserName,cmd.Email,id,userId);
            var result = await _userService.UpdateProfile(command);
            return Ok(new BaseResponseDto(result));
        }

        [HttpPut("{userId:guid}/Password")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> EditPassword([FromRoute] Guid userId, [FromBody] PasswordCommand cmd)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var passCommand = new UpdatePasswordCommand(id,userId,cmd.CurentPassword,cmd.NewPassword);
            var result = await _userService.UpdatePassword(passCommand);
            return Ok(new BaseResponseDto(result));
        }
        [HttpPut("{userId:guid}/Resume")]
        [Authorize]
        [Authorize(policy: "IsActive")]
        public async Task<ActionResult<BaseResponseDto>> UploadResume([FromRoute] Guid userId, [FromForm] IFormFile file)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result =await _userService.UploadResume(userId,id,file);
            return Ok(new BaseResponseDto(result));

        }
        [HttpDelete("{userId:guid}/Resume")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> DeleteMyResume([FromRoute] Guid userId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var result = await _userService.DeleteMyResumeAsync(userId,id);
            return Ok(new BaseResponseDto(result));

        }
        [HttpGet("{userId:guid}/Resume")]
        [Authorize]
        public async Task<ActionResult> DownloadMyResume([FromRoute] Guid userId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var result = await _userService.DownloadMyResumeAsync(id,userId);
            return File(result.Filedb64,result.contentType);
        }
        [HttpDelete("{userId:guid}")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> DeleteUser([FromRoute] Guid userid)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var result =await _userService.DeleteUser(userid,id);

            return Ok(new BaseResponseDto(result));
        }














    }
}
