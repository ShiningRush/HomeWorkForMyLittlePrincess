using Abp.AutoMapper;
using Clear.AccountManage.Domain.Blacklist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    /// <summary>
    /// 黑白名单修改入参
    /// </summary>
    [AutoMap(typeof(Blacklist))]
    public class InputUpdateBlacklistDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "id不能为空")]
        public Guid Id { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡号不能为空")]
        public string CardNo { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary> 
        public string AppId { get; set; }
        /// <summary>
        /// 类型
        /// </summary> 
        public virtual string Type { get; set; }
        /// <summary>
        /// 原因
        /// </summary> 
        public virtual string Remark { get; set; }
        /// <summary>
        /// 有效期开始时间
        /// </summary> 
        public virtual DateTime BeginValidDate { get; set; }
        /// <summary>
        /// 有效期结束时间
        /// </summary> 
        public virtual DateTime EndValidDate { get; set; }
    }
}
