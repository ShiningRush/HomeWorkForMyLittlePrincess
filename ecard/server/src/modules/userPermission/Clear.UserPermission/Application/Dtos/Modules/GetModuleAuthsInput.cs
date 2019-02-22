using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos.Modules
{
    public class GetModuleAuthsInput
    {
        public List<Guid> ModuleIds { get; set; }

        /// <summary>
        /// 是否包含模块
        /// </summary>
        public bool IsIncludeModule { get; set; } = true;
    }
}
