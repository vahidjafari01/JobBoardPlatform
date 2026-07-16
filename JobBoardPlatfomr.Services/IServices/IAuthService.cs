using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.OutPutDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterJobSeekerAsync(RegisterDto registerDto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RegisterEmployerAsync(AddCompanyCommand command);
    }
}
