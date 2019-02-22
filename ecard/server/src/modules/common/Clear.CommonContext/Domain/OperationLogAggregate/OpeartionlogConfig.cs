using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clear.CommonContext.Domain.OperationLogAggregate
{
    /// <summary>
    /// 操作日志配置表
    /// </summary> 
    [Table("sys_opeartionlogconfig")]
    public partial class OpeartionlogConfig : Entity<Guid>
    {
        /// <summary>
        /// 方法名称(包含命名空间)
        /// </summary> 
        [MaxLength(256, ErrorMessage = "方法名称最大长度256")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "方法名称不能为空")]
        public  string MethodName { get; set; }
        /// <summary>
        /// 模块
        /// </summary> 
        [MaxLength(20, ErrorMessage = "模块最大长度20")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "模块不能为空")]
        public string Module { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary> 
        [MaxLength(20, ErrorMessage = "操作类型最大长度20")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "操作类型不能为空")]
        public  string OperationType { get; set; }
        /// <summary>
        /// 日志格式化
        /// </summary> 
        [MaxLength(200, ErrorMessage = "日志格式化最大长度200")]
        public string LogFormat { get; set; }
    }
}
