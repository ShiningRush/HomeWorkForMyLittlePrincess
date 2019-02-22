using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Domain.OperatorAggregate
{
    /// <summary>
    /// 操作员
    /// </summary> 
    [Table("sys_user")]
    public class Operator : Entity<Guid>
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        [Column("LoginName")]
        public string LoginName { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("UserName")]
        public string UserName { get; set; }

        public Operator()
        {
            
        }
    }
}
