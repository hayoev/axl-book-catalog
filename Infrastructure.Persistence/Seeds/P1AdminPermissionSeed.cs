using System;
using Domain.Enums.AdminUsers;
using System.Collections.Generic;
using Domain.Entities.AdminUsers;
using Infrastructure.Persistence.Seeds.Base;

namespace Infrastructure.Persistence.Seeds
{
    public class P1AdminPermissionSeed : BaseSeed<AdminPermission>
    {
        protected override bool ForceUpdate => true;

        protected override IList<AdminPermission> DataSeed()
        {
            var data = new List<AdminPermission>();

            foreach (var (code, permissionEnum) in AdminPermissionEnum.Dictionary)
            {
                data.Add(new AdminPermission
                {
                    Id = permissionEnum.Value,
                    Code = code,
                    Name = permissionEnum.Name,
                    CreatedDateTime = DateTime.Now
                });
            }

            return data;
        }
    }
}