using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Extensions;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections;


namespace Clear.CommonContext.Domain.DataItemAggregate
{
    public class DataItemTransformInterceptor: IInterceptor
    {

        private readonly IStaticDataItemManager _staticDataItemManager;
        public ILogger Logger { get; set; }
             

        public DataItemTransformInterceptor(IStaticDataItemManager staticDataItemManager)
        {
            _staticDataItemManager = staticDataItemManager;
            Logger = NullLogger.Instance;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            PreIntercept(invocation);

            invocation.Proceed();

            PostIntercept(invocation);
        }


        /// <summary>
        /// 接口方法执行之前 检测数据字典存储key是否合法
        /// </summary>
        /// <param name="invocation"></param>
        private void PreIntercept(IInvocation invocation)
        {
            if (invocation.Arguments.Length == 0) return;

            foreach (var argument in invocation.Arguments)
            {
                argument.LoopDataItem(CheckDataItem);
            }
        }

        /// <summary>
        /// 接口方法执行之后  将字典存储key转换成显示value
        /// </summary>
        /// <param name="invocation"></param>
        private void PostIntercept(IInvocation invocation)
        {
            var returnValue = invocation.ReturnValue;
            if (returnValue == null) return;
            try
            {
                if (returnValue.GetType().IsAssignableToGenericType(typeof(YiBan.Common.Pages.PagerResult<>)))
                {
                    var result = returnValue.GetType().GetProperty("Result")?.GetValue(returnValue, null);
                    result.DataItemTransform(GetDataItemValue);
                }
                else
                {
                    returnValue.DataItemTransform(GetDataItemValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("转换数据字典异常", ex);
            }
        }

        private string GetDataItemValue(string category, string key)
        {
            return _staticDataItemManager.Get(category, key)?.Value;
        }

        private void CheckDataItem(string category, string key)
        {
            if (key.IsNullOrEmpty()) return;

            if (_staticDataItemManager.Get(category, key) == null)
            {
                throw new CustomHttpException($"字典【{category}】不存在值【{key}】");
            }
        }

    }
}
