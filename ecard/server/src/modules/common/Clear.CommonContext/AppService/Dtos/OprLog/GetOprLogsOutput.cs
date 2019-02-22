using Abp.AutoMapper;
using Clear.CommonContext.Domain.OperationLogAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService.Dtos.OprLog
{
    [AutoMapFrom(typeof(Operationlog))]
    public class GetOprLogsOutput
    {
        /// <summary>
        /// 模块
        /// </summary> 
        public virtual string Module { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary> 
        public virtual string Context { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary> 
        public virtual string OperationType { get; set; }
        /// <summary>
        /// 访问客户端名
        /// </summary> 
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 访问来源Ip
        /// </summary> 
        public virtual string ClientIpAddress { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        public virtual Guid? Creator { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary> 
        public virtual string CreatorName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
