using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Uow
{
    public static class PlatformDataFilters
    {
        public const string MustHaveOrganize = "MustHaveOrganize";

        public static class Parameters
        {
            /// <summary>
            /// "OrganizeIds".
            /// </summary>
            public const string OrganizeIds = "organizeIds";
        }
    }
}
