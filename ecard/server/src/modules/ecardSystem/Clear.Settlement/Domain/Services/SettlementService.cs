using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Json;
using Clear.Settlement.Domain.OperatorAggregate;
using PlatformService.BridgeComponent.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Domain.Services
{
    public class SettlementService : DomainService
    {
        private readonly IRepository<BillingRecord, long> _billingRecordRepo;
        private readonly IRepository<SettlementRecord, Guid> _settlementRecordRepo;
        private readonly string[] _settleBillTypeArrange = { "Recharge", "Refund" };

        public SettlementService(
            IRepository<BillingRecord, long> billingRecordRepo,
            IRepository<SettlementRecord, Guid> settlementRecordRepo)
        {
            _billingRecordRepo = billingRecordRepo;
            _settlementRecordRepo = settlementRecordRepo;
        }

        public void SettleBill(Operator ownedOperator, DateTime settleTime)
        {
            var lastSettlementRecord = GetLastSettlementRecord(ownedOperator);
            if (lastSettlementRecord != null && lastSettlementRecord.SettleEndTime >= settleTime)
            {
                throw new CustomHttpException("你已经结算过该时间段, 不需要重复结算");
            }

            var notSettledBills = GetNotSettledBillsUntillTime(ownedOperator, settleTime);
            var newSettlementRecordId = GenerateSettlementRecord(ownedOperator, settleTime,notSettledBills);

            foreach (var aBillRecord in notSettledBills)
            {
                aBillRecord.SettlementRecordId = newSettlementRecordId;
                _billingRecordRepo.Update(aBillRecord);
            }
        }

        /// <summary>
        /// 预览结算信息, 返回结果为键值对方便以后定制化
        /// </summary>
        /// <param name="ownedOperator"></param>
        /// <param name="settleTime"></param>
        /// <returns></returns>
        public Dictionary<string, string> PrivewSettlement(Operator ownedOperator, DateTime settleTime)
        {
            var lastSettlementRecord = GetLastSettlementRecord(ownedOperator);
            if (lastSettlementRecord != null && lastSettlementRecord.SettleEndTime > settleTime)
            {
                throw new CustomHttpException("你已经结算过该时间段, 不需要重复结算");
            }

            var notSettledBills = GetNotSettledBillsUntillTime(ownedOperator, settleTime);
            var settlementItems = GetSettlementItems(notSettledBills);
            settlementItems.Add("SettleStartTime", lastSettlementRecord?.SettleEndTime.ToString());
            settlementItems.Add("SettleEndTime", settleTime.ToString());
            return settlementItems;
        }

        private Guid GenerateSettlementRecord(Operator currentOperator, DateTime settleTime, List<BillingRecord> matchedBills)
        {
            var newSettlementRecord = new SettlementRecord(currentOperator, settleTime);
            var settlementItems = GetSettlementItems(matchedBills);

            newSettlementRecord.AmountContent = settlementItems.ToJsonString();
            return _settlementRecordRepo.InsertAndGetId(newSettlementRecord);
        }

        private Dictionary<string, string> GetSettlementItems(List<BillingRecord> matchedBills)
        {
            var billGroup = matchedBills.GroupBy(p => p.BillingType).Select(p =>
                new KeyValuePair<string, string>(p.Key, p.Sum(amount => amount.Amount).ToString())).ToList();
            var payType = matchedBills.GroupBy(p => p.PayType).Select(p =>
                new KeyValuePair<string, string>(p.Key, p.Sum(amount => amount.Amount).ToString())).ToList();
            var total = matchedBills.Sum(p => p.Amount).ToString();

            var resultList = new Dictionary<string, string>();
            billGroup.ForEach(p => resultList.Add(p.Key, p.Value));
            payType.ForEach(p => resultList.Add(p.Key, p.Value));
            resultList.Add("Total", total);
            resultList.Add("ChineseTotal", MoneyToChinese(total));

            return resultList;
        }

        private List<BillingRecord> GetNotSettledBillsUntillTime(Operator currentOperator, DateTime settleTime)
        {
            return _billingRecordRepo.GetAll()
                .Where(p=> _settleBillTypeArrange.Contains(p.BillingType))
                .Where(p => !p.SettlementRecordId.HasValue && p.CreateTime <= settleTime).ToList();
        }

        private SettlementRecord GetLastSettlementRecord(Operator ownedOperator)
        {
            return _settlementRecordRepo.GetAll().OrderByDescending(p => p.CreateTime).FirstOrDefault(p=>p.Creator == ownedOperator.Id);
        }

        #region 大写转换

        /// <summary>
        /// 金额转为大写金额
        /// </summary>
        /// <param name="LowerMoney"></param>
        /// <returns></returns>
        public string MoneyToChinese(string LowerMoney)
        {
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                {
                    LowerMoney = LowerMoney + "0";
                }
            }
            else
            {
                LowerMoney = LowerMoney + ".00";
            }
            strLower = LowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹圆以下的金额的处理
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }
        }

        #endregion
    }
}
