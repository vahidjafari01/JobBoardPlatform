using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.OutPutDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IApplicationService
    {
        Task<List<ApplicationDto>> GetAppsByJObAdId(ApplicationJobAdCommand command);
        Task<DetailAppDto> GetDetailApp(AppDetailCommand command);
        Task ChangeApplicationStatusAsync(ChangeAppStatusCommand command);

    }
}
