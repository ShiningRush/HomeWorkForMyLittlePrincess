using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAnt.Request.Handler;
using ServiceAnt.Handler;
using ServiceAnt.Request;

namespace Clear.CommonContext.Domain.DataItemAggregate.DomainEvents
{
    public class GetDataItemRequest : IRequestTrigger
    {
        public string[] Categorys { get; set; }
    }

    public class GetDataItemHandler : IRequestHandler<GetDataItemRequest>,Abp.Dependency.ITransientDependency
    {
        private readonly IStaticDataItemManager _staticDataItemManager;
        public GetDataItemHandler(IStaticDataItemManager staticDataItemManager)
        {
            _staticDataItemManager = staticDataItemManager;
        }
        public Task HandleAsync(GetDataItemRequest param, IRequestHandlerContext handlerContext)
        {
            List<DataItemDto> list = new List<DataItemDto>();
            if(param != null && param.Categorys != null)
            {
                foreach (var category in param.Categorys)
                {
                    list.AddRange(_staticDataItemManager.GetList(category));
                }
            }
            
            handlerContext.Response = list;

            return Task.FromResult(0);
        }
    }
}
