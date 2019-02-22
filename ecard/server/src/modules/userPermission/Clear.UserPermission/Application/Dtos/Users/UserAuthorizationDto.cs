using Abp.AutoMapper;
using Clear.UserPermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    [AutoMapFrom(typeof(User))]
    public class UserAuthorizationDto : Abp.Application.Services.Dto.EntityDto<Guid>
    {
        
        /// <summary>
        /// 登录名
        /// </summary> 
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        public virtual string UserName { get; set; }
       
        /// <summary>
        /// 是否重置密码
        /// </summary> 
        public virtual bool IsReset { get; set; }

        public List<ModuleDto> Modules { get; set; }

        public List<ModuleAuthDto> ModuleAuths { get; set; }

    }
}
