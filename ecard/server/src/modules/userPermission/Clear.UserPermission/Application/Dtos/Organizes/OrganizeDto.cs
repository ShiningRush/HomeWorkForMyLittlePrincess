using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    [AutoMap(typeof(Entities.Organize))]
    public class OrganizeDTO
    {
        
        /// <summary>
        /// 父级主键
        /// </summary> 
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary> 
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "代码必须是数字或字母！")]
        public virtual string Code { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary> 
        public virtual string Name { get; set; }
        /// <summary>
        /// 机构性质
        /// </summary> 
        public virtual string Nature { get; set; }
        /// <summary>
        /// 电话
        /// </summary> 
        public virtual string Phone { get; set; }
        /// <summary>
        /// 传真
        /// </summary> 
        public virtual string Fax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary> 
        public virtual string Postalcode { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "邮箱格式不正确！")]
        public virtual string Email { get; set; }
        /// <summary>
        /// 负责人主键
        /// </summary> 
        public virtual string ManagerId { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary> 
        public virtual string Address { get; set; }
        /// <summary>
        /// 公司官网
        /// </summary> 
        public virtual string WebAddress { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary> 
        public virtual string FoundedTime { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public virtual string Description { get; set; }


    }
}
