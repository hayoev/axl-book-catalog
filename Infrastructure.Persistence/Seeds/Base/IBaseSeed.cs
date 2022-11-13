using System.Threading.Tasks;
 using Application.Admin.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Persistence.Seeds.Base
{
    public interface IBaseSeed
    {
        Task<int> Start(bool isForceUpdate = false);
        void SetDbContext(IApplicationDbContext applicationDbContext);
        void SetWebHostEnvironment(IWebHostEnvironment env);
    }
}