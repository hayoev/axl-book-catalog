using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Interfaces;
using Domain.Common.BaseEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds.Base
{
    public abstract class BaseSeed<TEntity> : IBaseSeed
        where TEntity : BaseEntity
    {
        protected IApplicationDbContext ApplicationDbContext;
        protected IWebHostEnvironment WebHostEnvironment;
        protected virtual bool ForceUpdate => false;

        protected virtual bool BeforeStart()
        {
            return true;
        }

        protected abstract IList<TEntity> DataSeed();

        public async Task<int> Start(bool isForceUpdate)
        {
            if (!BeforeStart()) return 0;

            foreach (var data in DataSeed())
            {
                var entity = await ApplicationDbContext
                    .Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == data.Id);

                if (entity == null)
                {
                    ApplicationDbContext.Set<TEntity>().Add(data);
                }else if (ForceUpdate && isForceUpdate)
                {
                    //что бы не изменилось дата создание надо передать то что сохранено в базе
                    data.CreatedDateTime = entity.CreatedDateTime;
                    ApplicationDbContext.Set<TEntity>().Update(data);
                }
            }
            
            return await ApplicationDbContext.SaveChangesAsync();
        }

        public void SetDbContext(IApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public void SetWebHostEnvironment(IWebHostEnvironment env)
        {
            WebHostEnvironment = env;
        }
    }
}