using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatform.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardPlatform.Presentation.Controllers
{
    public class AdminController:ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponseDto>> ApproveCompany([FromRoute] Guid companyId)
        {
            var result = await _adminService.SetApprovedCompanyAsync(companyId);
            return Ok(new BaseResponseDto(result));
        }
    }
}
