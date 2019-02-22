using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clear.Settlement.Domain.Services;
using Clear.Settlement.Domain.OperatorAggregate;
using FakeItEasy;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlatformService.BridgeComponent.CustomException;

namespace Clear.Settlement.Test.Domain.Services
{
    [TestClass]
    public class SettlementService_Test
    {
        [TestMethod]
        public void ShouldCorrect_Settle()
        {
            var newOperator = new Operator();
            newOperator.Id = Guid.NewGuid();
            var billRecordRepo = A.Fake<IRepository<BillingRecord, long>>();
            var settlementRecordRepo = A.Fake<IRepository<SettlementRecord, Guid>>();
            var expectedResult = new
            {
                Refund = "-300.00",
                Recharge = "1501.50",
                UnionPay = "200.40",
                WeChat = "0.10",
                Alipay = "700.70",
                Cash = "300.30",
                Total = "1201.50",
                ChineseTotal = "1201.50"
            };

            // 模拟数据仓库
            A.CallTo(() => billRecordRepo.GetAll()).Returns(new List<BillingRecord>() {
                new BillingRecord() {  BillingType = "Refund", PayType = "UnionPay", Amount = -200.00M, CreateTime = new DateTime(2000, 4, 1) },
                new BillingRecord() {  BillingType = "Refund", PayType = "WeChat", Amount = -100.00M, CreateTime = new DateTime(2000, 4, 1) },
                new BillingRecord() {  BillingType = "Refund", PayType = "Alipay", Amount = -100.00M, CreateTime = new DateTime(2000, 4, 1), SettlementRecordId = default(Guid)},
                new BillingRecord() {  BillingType = "Recharge", PayType = "WeChat", Amount = 100.10M, CreateTime = new DateTime(2000, 4, 1) },
                new BillingRecord() {  BillingType = "Recharge", PayType = "Alipay", Amount = 200.20M, CreateTime = new DateTime(2000, 4, 1) },
                new BillingRecord() {  BillingType = "Recharge", PayType = "Cash", Amount = 300.30M, CreateTime = new DateTime(2000, 4, 1) },
                new BillingRecord() {  BillingType = "Recharge", PayType = "UnionPay", Amount = 400.40M, CreateTime = new DateTime(2000, 4, 1) },
                new BillingRecord() {  BillingType = "Recharge", PayType = "Alipay", Amount = 500.50M, CreateTime = new DateTime(2000, 4, 1) },
                new BillingRecord() {  BillingType = "Recharge", PayType = "UnionPay", Amount = 600.60M, CreateTime = new DateTime(2000, 5, 1, 20,0,1) }
            }.AsQueryable());

            var newGuid = Guid.NewGuid();
            A.CallTo(() => settlementRecordRepo.GetAll()).Returns(new List<SettlementRecord>() {
                new SettlementRecord(){ CreateTime = new DateTime(2000, 2, 1) , Creator = newOperator.Id }
            }.AsQueryable());
            A.CallTo(() => settlementRecordRepo.InsertAndGetId(A<SettlementRecord>.Ignored)).Invokes((SettlementRecord insertedSettlementReocord) =>
              {
                  Assert.AreEqual(JsonConvert.SerializeObject(expectedResult), insertedSettlementReocord.AmountContent);
                  Assert.AreEqual(newOperator.Id, insertedSettlementReocord.Creator);
              }).Returns(newGuid);

            A.CallTo(billRecordRepo).Where(call => call.Method.Name == "Update").Invokes((BillingRecord updateEntity) =>
              {
                Assert.AreEqual(newGuid, updateEntity.SettlementRecordId);
              });

            var service = new SettlementService(billRecordRepo, settlementRecordRepo);
            service.SettleBill(newOperator, new DateTime(2000, 5, 1, 20, 0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(CustomHttpException))]
        public void ShouldRaiseException_WhenAlreadySettled()
        {
            var newOperator = new Operator();
            newOperator.Id = Guid.NewGuid();
            var billRecordRepo = A.Fake<IRepository<BillingRecord, long>>();
            var settlementRecordRepo = A.Fake<IRepository<SettlementRecord, Guid>>();

            var newGuid = Guid.NewGuid();
            A.CallTo(() => settlementRecordRepo.GetAll()).Returns(new List<SettlementRecord>() {
                new SettlementRecord(){ SettleEndTime = new DateTime(2000, 5, 1, 20, 0, 0), Creator = newOperator.Id }
            }.AsQueryable());

            A.CallTo(billRecordRepo).Where(call => call.Method.Name == "Update").Invokes((BillingRecord updateEntity) =>
            {
                Assert.AreEqual(newGuid, updateEntity.SettlementRecordId);
            });


            var service = new SettlementService(billRecordRepo, settlementRecordRepo);
            service.SettleBill(newOperator, new DateTime(2000, 5, 1, 20, 0, 0));
        }
    }
}
