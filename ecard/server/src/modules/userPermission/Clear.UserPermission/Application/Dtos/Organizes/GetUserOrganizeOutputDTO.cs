using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    [AutoMap(typeof(Entities.Organize))]
    public class GetUserOrganizeOutputDTO : OrganizeDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
