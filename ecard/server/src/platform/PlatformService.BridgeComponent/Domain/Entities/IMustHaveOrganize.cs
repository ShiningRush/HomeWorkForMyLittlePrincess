using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    /// <summary>
    /// 定义一个拥有机构编号的实体
    /// </summary>
    public interface IMustHaveOrganize: IMustHaveOrganize<Guid>
    {
    }

    /// <summary>
    /// 定义一个拥有机构编号的实体
    /// </summary>
    public interface IMustHaveOrganize<TPrimaryKey>
    {
        /// <summary>
        /// 机构id
        /// </summary>
        TPrimaryKey OrganizeId { get; set; }
    }
}
