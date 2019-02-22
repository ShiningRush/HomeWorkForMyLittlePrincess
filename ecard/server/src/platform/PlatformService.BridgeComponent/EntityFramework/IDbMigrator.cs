using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.EntityFramework
{
    public interface IDbMigrator: ITransientDependency
    {
        void CreateOrMigrate();

        bool IsModleChanged();
    }
}
