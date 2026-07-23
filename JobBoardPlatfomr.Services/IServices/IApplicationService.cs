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
        Task<string> ChangeApplicationStatusAsync(ChangeAppStatusCommand command);
        Task<string> CreateApplicationAsync(CreateAppCommand cmd);
        Task<string> CancellMyApp(Guid requesterId, Guid appId);

        Task<AppDetailForJobSeeker> GetAppDetailForJobSeekerAsync(Guid RequesterId, Guid appid);
        Task<List<AppDtoForCustomer>> GetAppsForJobSeekerAsync(Guid UserId, Guid RequesterId);
        

    }
}
