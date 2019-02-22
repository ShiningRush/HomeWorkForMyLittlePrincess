using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Domain.OperatorAggregate
{
    /// <summary>
    /// 账户信息
    /// </summary> 
    [Table("ec_account")]
    public partial class Account : Entity<string>
    {
        protected Account()
        {
        }

        /// <summary>
        /// 账号代码 自动按照1XXXXXXXXXX格式生成11位数字
        /// </summary> 
        [Column("Id")]
        [Key]
        public override string Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        [Column("Name")]
        public virtual string Name { get; set; }
    }
}
