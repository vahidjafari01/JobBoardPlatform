using JobBoardPlatform.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class NotificationRepo : GenericRepository<Notification>, INotificationRepo
    {
        public NotificationRepo(AppDbContext context) : base(context)
        {
        }
    }
}
