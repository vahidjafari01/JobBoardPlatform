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

    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet("{companyId:guid}")]
        [Authorize(policy:"EmployerOrAdmin")]
        [Authorize(policy: "IsActive")]
        public async Task<ActionResult<BaseResponseDto>> GetMyCompanyDetail([FromRoute] Guid companyId)
        {
            var user = User;

            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var command = new GetCompanyDetailCommand { CompanyId = companyId,RequesterId = id };
            var result = await _companyService.GetMyCompanyDetailAsync(command);
            return Ok(new BaseResponseDto(result));
        }
        [HttpPut("{companyId:guid}")]
        [Authorize(policy: "EmployerOrAdmin")]
        [Authorize(policy: "IsActive")]

        public async Task<ActionResult<BaseResponseDto>> EditMyProfileCompany([FromRoute] Guid companyId, [FromBody] UpdateCompanyDto cmd)
        {
            var user = User;
            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);
            var command = new UpdateCompanyComand { RequesterId = id,CompanyId = companyId,Name = cmd.Name,Description = cmd.Description,Website = cmd.Website,Location = cmd.Location,CityId = cmd.CityId};

            await _companyService.UpdateCompanyAsync(command);
            return Ok(new BaseResponseDto("succesfully updated"));
        }
        
       


        [HttpPost("{companyId:guid}/logo")]
        [Authorize(policy:"EmployerOrAdmin")]
        [Authorize(policy: "IsActive")]

        public async Task<ActionResult<BaseResponseDto>> UploadCompanyLogo([FromRoute] Guid companyId,IFormFile file)
        {
            var user = User;
            var requesterId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(requesterId, out var id);

            var result =await _companyService.UploadCompanyLogo(companyId,id,file);
            return Ok(new BaseResponseDto(result));
        }





    }
}
