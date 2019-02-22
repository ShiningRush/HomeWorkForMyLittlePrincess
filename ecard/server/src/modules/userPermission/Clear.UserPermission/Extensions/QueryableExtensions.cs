using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;
using YiBan.Common.Extensions;
using Abp.AutoMapper;

namespace Clear.UserPermission.Extensions
{
    public static class QueryableExtensions
    {
        public static PagerResult<TDto> GetPagerResult<TDto, TEntity>(this IQueryable<TEntity> source, PagerParameter parameter)
        {
            if (string.IsNullOrEmpty(parameter.OrderBy))
            {
                parameter.OrderBy = "Id";
            }
            int total = source.Count();
            var list = source.OrderBy(parameter.OrderBy, parameter.IsAsc).Skip((parameter.PageIndex - 1) * parameter.PageSize).Take(parameter.PageSize).ToList();
            var dtos = list.MapTo<List<TDto>>();
            return new PagerResult<TDto>(parameter.PageSize, parameter.PageIndex, total, dtos.ToList());
        }
    }
}
