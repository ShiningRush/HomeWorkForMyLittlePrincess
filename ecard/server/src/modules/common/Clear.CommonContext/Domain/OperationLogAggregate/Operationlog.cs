using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clear.CommonContext.Domain.OperationLogAggregate
{
    /// <summary>
    /// 操作日志表
    /// </summary> 
    [Table("sys_operationlog")]
    public partial class Operationlog : Entity<Guid>, IHasCreatorTime
    {
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
        public string OperationType { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary> 
        [MaxLength(200, ErrorMessage = "日志内容最大长度200")]
        public string LogContext { get; set; }
        /// <summary>
        /// 访问客户端名
        /// </summary> 
        [MaxLength(64, ErrorMessage = "访问客户端名最大长度64")]
        public  string ClientName { get; set; }
        /// <summary>
        /// 访问来源Ip
        /// </summary> 
        [MaxLength(64, ErrorMessage = "访问来源Ip最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "访问来源Ip不能为空")]
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 访问浏览器信息
        /// </summary>
        [MaxLength(64, ErrorMessage = "访问浏览器信息最大长度64")]
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 参数内容
        /// </summary> 
        public  string Parameters { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid? OperationUserId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
