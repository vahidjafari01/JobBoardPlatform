using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardPlatform.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("registerJobSeeker")]
        public async Task<ActionResult<BaseResponseDto>> RegisterJObSeeker([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterJobSeekerAsync(registerDto);
            return Ok(new BaseResponseDto(result));
        }
        [HttpPost("registerJobEmployer")]
        [Authorize]

        public async Task<ActionResult<BaseResponseDto>> RegisterEmployer([FromBody] AddCompanyCommand command)
        {
            var result = await _authService.RegisterEmployerAsync(command);
            return Ok(new BaseResponseDto(result));
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(new BaseResponseDto(result));
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult<BaseResponseDto>> Refresh([FromBody] RefreshDto refreshDto)
        {
            var result = await _authService.Refresh(refreshDto.token, refreshDto.UserId);
            return Ok(new BaseResponseDto(result));
        }
        [HttpPost("logOut")]
        [Authorize]
        public async Task<ActionResult<BaseResponseDto>> Logout([FromBody] RefreshDto refreshDto)
        {
            var result = await _authService.LogOutAsync(refreshDto.token, refreshDto.UserId);
            return Ok(new BaseResponseDto(result));
        }

    }
}
