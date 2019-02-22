using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.UserPermission.Application.Dtos
{
    public class GetOrganizeInputDTO : PagerParameter
    {
        /// <summary>
        /// 机构名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 机构负责人
        /// </summary>
        public string Manager { get; set; }
    }
}
