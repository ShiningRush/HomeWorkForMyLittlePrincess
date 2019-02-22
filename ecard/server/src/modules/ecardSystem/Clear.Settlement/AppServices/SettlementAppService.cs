using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clear.Settlement.AppServices.Dtos;
using YiBan.Common.Pages;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Clear.Settlement.Domain.OperatorAggregate;
using Clear.Settlement.Domain.Services;
using PlatformService.BridgeComponent.Service;
using YiBan.Common.Extensions;
using PlatformService.BridgeComponent.WebApiConfig;
using Clear.Component.Interfaces.Componets.DataFormat;
using System.IO;
using PlatformService.Core.Common.Const;
using PlatformService.BridgeComponent.Service.Session;
using Abp.Extensions;
using PlatformService.BridgeComponent.Service.Data;
using Clear.Settlement.EventHandler;
using ServiceAnt;

namespace Clear.Settlement.AppServices
{
    /// <summary>
    /// 结算相关服务
    /// </summary>
    public class SettlementAppService : IApplicationService, ISettlementAppService
    {
        private readonly IRepository<Operator, Guid> _operatorRepository;
        private readonly IRepository<BillingRecord, long> _billingRecordRepository;
        private readonly IRepository<SettlementRecord, Guid> _settlementRecordRepository;

        private readonly SettlementService _settlementService;
        private readonly IServiceContext _serviceContext;
        private readonly IFormatterFactory _formatterFactory;
        private readonly IServiceBus _serviceBus;

        public SettlementAppService(
            IRepository<Operator, Guid>  operatorRepository,
            SettlementService settlementService,
            IServiceContext serviceContext,
            IRepository<BillingRecord, long> billingRecordRepository,
            IRepository<SettlementRecord, Guid> settlementRecordRepository,
            IFormatterFactory formatterFactory,
            IServiceBus serviceBus)
        {
            _operatorRepository = operatorRepository;
            _settlementService = settlementService;
            _serviceContext = serviceContext;
            _billingRecordRepository = billingRecordRepository;
            _formatterFactory = formatterFactory;
            _settlementRecordRepository = settlementRecordRepository;
            _serviceBus = serviceBus;
        }

        /// <summary>
        /// 按时间段结算交易记录
        /// </summary>
        /// <param name="inputDto"></param>
        public void SettleBillingRecord(SettleBillingRecordInput inputDto)
        {
            var currentOperator = _operatorRepository.FirstOrDefault(p => p.LoginName == _serviceContext.ClientID);
            _settlementService.SettleBill(currentOperator, inputDto.SettleTime);
        }

        /// <summary>
        /// 获取结算明细
        /// </summary>
        public PagerResult<GetSettlementListOutput> GetSettlementList(GetSettlementListInput inputDto)
        {
            var result = new PagerResult<GetSettlementListOutput>();
            if (!inputDto.StartTime.HasValue)
                inputDto.StartTime = DateTime.Now.AddDays(-1);

            if (!inputDto.EndTime.HasValue)
                inputDto.EndTime = DateTime.Now;

            var matchedRecords = _settlementRecordRepository.GetAllIncluding(p=>p.OwedOperator)
                .WhereIf(p=>p.OwedOperator.UserName == inputDto.OperatorName, !inputDto.OperatorName.IsNullOrEmpty())
                .WhereIf(p => p.SettleEndTime > inputDto.StartTime, inputDto.StartTime != default(DateTime))
                .WhereIf(p => p.SettleEndTime <= inputDto.EndTime, inputDto.EndTime != default(DateTime))
                .Page(inputDto, result)
                .Select(p => new GetSettlementListOutput()
                 {
                    SettlementRecordId = p.Id.ToString(),
                    OperatorName = p.OwedOperator.UserName,
                    SettlementTime = p.SettleEndTime,
                    SettlementContent = p.AmountContent
                }).ToList();

            result.Result = matchedRecords;
            return result;
        }

        /// <summary>
        /// 获取单次结算的交易记录
        /// </summary>
        public List<GetSingleSettlementDetailOutput> GetSingleSettlementDetail(GetSingleSettlementDetailInput inputDto)
        {
            return _billingRecordRepository.GetAll()
                .Where(p => p.SettlementRecordId == inputDto.SettlementId)
                .Select( p=> new GetSingleSettlementDetailOutput()
                {
                    Amount = p.Amount,
                    BillingNo = p.BillingNo,
                    BillingType = p.BillingType,
                    CustomerName = p.OwedAccount.Name,
                    PayType = p.PayType,
                    OperatorName = p.OwedOperator.UserName,
                    CreateTime = p.CreateTime
                }).ToList();
        }

        /// <summary>
        /// 导出单次结算的交易记录
        /// </summary>
        public HttpFileOutput ExportSingleSettlementDetail(ExportSingleSettlementDetailInput inputDto)
        {
            var details = this.GetSingleSettlementDetail(new GetSingleSettlementDetailInput() { SettlementId = inputDto.SettlementId });
            // 创建对应格式的Formatter
            var excelFormatter = _formatterFactory.Create(FomatterType.Excel);

            // 准备输出文件的路径（绝对路径）
            var filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                PlatformServiceConst.TEMP_FILE_NAME,
                $"{Guid.NewGuid().ToString()}.excel");

            var tempDictionary = _serviceBus.Send<List<DataItemDto>>(new GetDataItemRequest() { Categorys = new string[] { "PayType", "BillingType" } });

            // 数据字典转换
            details.DataItemTransform((category, key) =>
            {
                return tempDictionary.Where(p => p.Category == category && p.Key == key).FirstOrDefault()?.Value;
            });

            // 把Studen实体列表输出到指定路径
            excelFormatter.FromListToFile(details, filePath);
            return new HttpFileOutput()
            {
                FilePath = filePath,
                FileName = $"日结明细_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.xlsx"
            };
        }

        /// <summary>
        /// 获取当前未结算的交易记录图表集
        /// </summary>
        /// <param name="inputDto"></param>
        public List<GetNotSettledBillingGraphDataOutput> GetNotSettledBillingGraphData(GetNotSettledBillingGraphDataInput inputDto)
        {
            var currentOperator = _operatorRepository.FirstOrDefault(p => p.LoginName == _serviceContext.ClientID);
            return _settlementService.PrivewSettlement(currentOperator, inputDto.SettleTime)
                    .Select(p=> new GetNotSettledBillingGraphDataOutput()
                    {
                        ItemKey = p.Key,
                        ItemValue = p.Value
                    }).ToList();
        }
    }
}
