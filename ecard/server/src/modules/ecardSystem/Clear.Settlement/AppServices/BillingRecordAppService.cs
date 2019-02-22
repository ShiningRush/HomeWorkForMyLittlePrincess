using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Clear.AccountManage.Domain.Accounts;
using Clear.Component.Interfaces.Componets.DataFormat;
using Clear.Settlement.Domain.OperatorAggregate;
using MySql.Data.MySqlClient;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.Service.Session;
using PlatformService.BridgeComponent.WebApiConfig;
using PlatformService.Core.Common.Const;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Extensions;
using YiBan.Common.Pages;

namespace Clear.AccountManage.Application
{
    public class BillingRecordAppService : ApplicationService, IBillingRecordAppService
    {
        private readonly IRepository<BillingRecord, long> _billingRecordRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IDatabaseMediator<BillingRecord, long> _databaseMediator;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IFormatterFactory _formatterFactory;
        private readonly ISessionManager _sessionManager;
        public BillingRecordAppService(
            IRepository<BillingRecord, long> billingRecordRepository,
            IAccountRepository accountRepository,
            IDatabaseMediator<BillingRecord, long> databaseMediator,
            IUnitOfWorkManager unitOfWorkManager,
            IFormatterFactory formatterFactory,
            ISessionManager sessionManager)
        {
            _billingRecordRepository = billingRecordRepository;
            _accountRepository = accountRepository;
            _databaseMediator = databaseMediator;
            _unitOfWorkManager = unitOfWorkManager;
            _formatterFactory = formatterFactory;
            _sessionManager = sessionManager;

        }
        /// <summary>
        /// 获取用卡明细列表（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public YiBan.Common.Pages.PagerResult<OutputBillingRecordListDto> GetBillingRecordList(InputBillingRecordListDto input)
        {
            string sql = string.Empty;
            string sqlCount = string.Empty;
            var parameters = HandleSQL(input,"", out sql);
            var parametersCount = HandleSQL(input, "count", out sqlCount);
            var pagerWrapper = new PagerResult<OutputBillingRecordListDto>();
            pagerWrapper.Result = _databaseMediator.ExcuteSqlQueryAsync<OutputBillingRecordListDto>(sql, parameters).Result;
            int count = _databaseMediator.ExcuteSqlQueryAsync<int>(sqlCount, parametersCount).Result[0];
            pagerWrapper.TotalCount = count;
            pagerWrapper.PageIndex = input.PageIndex;
            pagerWrapper.PageSize = input.PageSize;
            return pagerWrapper;
        }

        /// <summary>
        /// 导出用卡明细列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public HttpFileOutput DeriveBillingRecordList(InputBillingRecordListDto input)
        {
            string sql = string.Empty;
            var parameters = HandleSQL(input, "export", out sql);
            var pagerWrapper = new PagerResult<OutputDeriveBillingRecordListDto>();
            pagerWrapper.Result = _databaseMediator.ExcuteSqlQueryAsync<OutputDeriveBillingRecordListDto>(sql, parameters).Result;
            //预留：ex操作组件 begin
            var excelFormatter = _formatterFactory.Create(FomatterType.Excel);
            // 准备输出文件的路径（绝对路径）
            var filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                PlatformServiceConst.TEMP_FILE_NAME,
                $"{Guid.NewGuid().ToString()}.xlsx");
            // 把Studen实体列表输出到指定路径
            excelFormatter.FromListToFile<OutputDeriveBillingRecordListDto>(pagerWrapper.Result, filePath);

            //预留：ex操作组件 end
            return new HttpFileOutput()
            {
                FilePath = filePath,
                FileName = $"用卡明细列表数据{DateTime.Now.ToString("yyyyMMdd_HH:mm")}.xlsx"
            };
        }

        /// <summary>
        /// sql处理
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private MySqlParameter[] HandleSQL(InputBillingRecordListDto input,string type,out string sql)
        {
            var organizeId = _sessionManager.CurrentOrganizeId;
            sql = string.Empty;
            DateTime? bTime = null;
            DateTime? eTime = null;
            if (!input.BeginTime.IsNullOrEmpty())
                bTime = DateTime.Parse(input.BeginTime);
            if (!input.EndTime.IsNullOrEmpty())
                eTime = DateTime.Parse(input.EndTime).AddDays(1);
            
            int pageIn = (input.PageIndex - 1) * input.PageSize;
            int pageSi = input.PageSize;
            List<MySqlParameter> mysql = new List<MySqlParameter>();
            string sqlWhere = string.Empty;
            string strlimit = $" LIMIT {pageIn}, {pageSi}";
            string strasc = "order by e.CreateTime desc ";
            if (input.IsAsc)
                strasc = "order by e.CreateTime Asc ";
            if (type== "export")
            {
                strlimit = string.Empty;
            }
            if (!string.IsNullOrEmpty(input.CardNo))
            {
                sqlWhere += string.Format(@" and  EXISTS (SELECT c.`AccountId` FROM ec_card c WHERE e.AccountId = c.AccountId   AND c.CardNo =  @CardNo ) ");
                mysql.Add(new MySqlParameter("@CardNo", input.CardNo));
            }
            if(organizeId.HasValue)
            {
                sqlWhere += string.Format(" and e.organizeId  =@OrganizeId ");
                mysql.Add(new MySqlParameter("@OrganizeId", organizeId));
            }
            if (!string.IsNullOrEmpty(input.Name))
            {
                sqlWhere += string.Format(" and a.Name  like concat(?@Name,'%')  ");
                mysql.Add(new MySqlParameter("@Name", input.Name));
            }
            if (!string.IsNullOrEmpty(input.Creator))
            {
                sqlWhere += string.Format("and e.Creator =@Creator  ");
                mysql.Add(new MySqlParameter("@Creator", input.Creator));
            }
            if (!string.IsNullOrEmpty(input.CapitalType))
            {
                sqlWhere += string.Format(" and e.CapitalType =@CapitalType  ");
                mysql.Add(new MySqlParameter("@CapitalType", input.CapitalType));
            }
            if (!string.IsNullOrEmpty(input.AccountId))
            {
                sqlWhere += string.Format(" and e.AccountId =@AccountId  ");
                mysql.Add(new MySqlParameter("@AccountId", input.AccountId));
            }
            if (bTime != null)
            {
                sqlWhere += string.Format(" and e.CreateTime>='{0}'  ", bTime);
                mysql.Add(new MySqlParameter("@bTime", bTime));
            }
            if (eTime != null)
            {
                sqlWhere += string.Format(" and e.CreateTime <='{0}' ", eTime);
                mysql.Add(new MySqlParameter("@eTime", eTime));
            }
            if (type== "count")
                sql = string.Format(@"SELECT  count(1) as count 
                            FROM  ec_billingrecord e   LEFT JOIN ec_account a  ON a.Id = e.AccountId  LEFT JOIN sys_user u ON e.Creator = u.Id 
                            WHERE  1=1 {0}", sqlWhere);
            else
                sql = string.Format(@"SELECT e.AccountId,a.Name,e.Amount,e.Balance,e.CreateTime,e.DESC AS ApplicationScene,u.UserName,
                              e.CapitalType,
                              (
                                CASE
                                  e.BillingType 
                                  WHEN 'Recharge' 
                                  THEN '充值' 
                                  WHEN 'Consumption' 
                                  THEN '消费' 
                                  WHEN 'Refund' 
                                  THEN '退费' 
                                END
                              ) AS BillingType 
                            FROM  ec_billingrecord e   LEFT JOIN ec_account a  ON a.Id = e.AccountId  LEFT JOIN sys_user u ON e.Creator = u.Id 
                            WHERE  1=1 {0} {1} {2}", sqlWhere,  strasc, strlimit);
            return mysql.ToArray();
        }

        

    }
}
