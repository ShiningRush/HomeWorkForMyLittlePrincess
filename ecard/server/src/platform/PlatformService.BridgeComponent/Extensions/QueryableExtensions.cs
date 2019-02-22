using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace PlatformService.BridgeComponent.Extensions
{
    public static class QueryableExtensions
    {
        public static PagerResult<TEntity> Page<TEntity>(this IQueryable<TEntity> source, PagerParameter parameter)
        {
            int total = source.Count();
            var list = source.Skip((parameter.PageIndex - 1) * parameter.PageSize).Take(parameter.PageSize).ToList();
            return new PagerResult<TEntity>(parameter.PageSize, parameter.PageIndex, total, list);
        }

        public static PagerResult<TResult> Page<TSource, TResult>(this IQueryable<TSource> source, PagerParameter parameter, Func<TSource, TResult> selector)
        {
            int total = source.Count();
            var list = source.Skip((parameter.PageIndex - 1) * parameter.PageSize).Take(parameter.PageSize).ToList().Select(selector);
            return new PagerResult<TResult>(parameter.PageSize, parameter.PageIndex, total, list.ToList());
        }
    }
}
