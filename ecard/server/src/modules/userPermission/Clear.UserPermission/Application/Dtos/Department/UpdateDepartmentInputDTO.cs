using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 更新部门信息DTO
    /// </summary>
    [AutoMap(typeof(Entities.Department))]
    public class UpdateDepartmentInputDTO : DepartmentDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
