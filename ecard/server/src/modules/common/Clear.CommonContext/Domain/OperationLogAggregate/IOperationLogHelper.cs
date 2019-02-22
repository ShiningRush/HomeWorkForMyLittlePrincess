using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Domain.OperationLogAggregate
{
    public interface IOperationLogHelper
    {
        /// <summary>
        /// 是否需要记录操作日志
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        bool ShouldRecordOperationLog(IInvocation invocation);
        /// <summary>
        /// 创建操作日志对象
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        Operationlog CreateOperationlog(IInvocation invocation);
        /// <summary>
        /// 创建操作日志对象
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        Operationlog CreateOperationlog(IInvocation invocation, IDictionary<string, object> arguments);
        /// <summary>
        /// 保存操作日志对象
        /// </summary>
        /// <param name="operationlog"></param>
        Task SaveAsync(Operationlog operationlog);
        /// <summary>
        /// 保存操作日志对象
        /// </summary>
        /// <param name="invocation"></param>
        Task SaveAsync(IInvocation invocation);
    }
}
