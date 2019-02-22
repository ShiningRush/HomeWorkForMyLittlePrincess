<template>
<div style="margin-left:10px;">
      <el-form>
        <el-form-item>
          <el-form :inline="true">
            <el-row :gutter="50" style="text-align: right">
              <el-col :span="50" style="text-align: left">
                <el-form-item prop="orderBy" label="操作员">
                  <el-input auto-complete="off" v-model="queryCriteria.operatorName" placeholder="请输入操作员"></el-input>
                </el-form-item>
                <el-form-item prop="keyword" label="操作时间">                     
                <el-date-picker
                  v-model="valueTime"
                  type="datetimerange"
                  :picker-options="pickerOptions"
                  range-separator="至"
                  start-placeholder="开始时间"
                  end-placeholder="结束时间"
                  align="right">
                </el-date-picker>
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" icon="el-icon-search" @click="onSubmit" :loading="pageLoading" v-has="'Search'" >查询</el-button>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-form-item>
      </el-form>
      <el-table :data="settlementList.result" v-loading="pageLoading" style="width:100%"  element-loading-text="拼命加载中" fit highlight-current-row>
        <el-table-column prop="operatorName" label="操作员"></el-table-column>
        <el-table-column prop="settlementTime" width="180px"  label="结算时间 "></el-table-column>
        <el-table-column prop="Recharge" label="预交金总额"></el-table-column>
        <el-table-column prop="Refund" label="退款总额"></el-table-column>
        <el-table-column prop="Total" label="合计总额"></el-table-column>
        <el-table-column prop="Cash" label="现金"></el-table-column>
        <el-table-column prop="WeChat" label="微信"></el-table-column>
        <el-table-column prop="Alipay" label="支付宝"></el-table-column>
        <el-table-column prop="UnionPay" label="银联"></el-table-column>
        <el-table-column label="操作" align="center">
            <div slot-scope="scope">
              <el-button type="primary" size="mini"  @click="preview(scope.row)" v-has="'Detail'" >详情明细</el-button>
            </div>
          </el-table-column>            
      </el-table>
      <div class="block pagination" style="text-align:center ">
        <el-pagination
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page="this.queryCriteria.pageIndex"
            :page-sizes="[10,20,30,50]"
            :page-size="this.queryCriteria.pageSize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="settlementList.totalCount">
        </el-pagination>
      </div>
      <el-dialog :visible.sync="detailFormConfig.visible" title="日结明细">
          <el-table class="only-for-print" :data="detailList" v-loading='detailFormConfig.formLoading' style="width:100%" ref="detaiFromTable"  element-loading-text="拼命加载中" fit highlight-current-row>
            <el-table-column prop="billingNo" label="流水号"></el-table-column>
            <el-table-column prop="operatorName" label="操作员 "></el-table-column>
            <el-table-column prop="customerName" label="姓名"></el-table-column>
            <el-table-column prop="billingType" label="交易类型"></el-table-column>
            <el-table-column prop="payType" label="支付方式"></el-table-column>
            <el-table-column prop="amount" label="交易金额"></el-table-column>
            <el-table-column prop="createTime" label="交易时间"></el-table-column>
          </el-table>
          <div slot="footer" class="dialog-footer">
              <el-button type="primary" :loading="detailFormConfig.formLoading" @click="exportBillDetails" v-has="'Export'">导 出</el-button>
              <el-button type="primary" :loading="detailFormConfig.formLoading" @click="printBillDetails" v-has="'Print'" >打 印</el-button>
              <el-button @click="detailFormConfig.visible = false">关 闭</el-button>
          </div>
      </el-dialog>
    </div>  
</template>

<script>
  import * as settlementApi from "@/api/settlement"
  import { oauthTokenManager } from "@/utils/auth"

  export default {
    data() {
      return {
        pageLoading :false,
        detailFormConfig:{
          title: '',
          visible: false,
          settlementId: '',
          formLoading: false
        },
        settlementList:{
          result:[],
          totalCount: 0
        },
        detailList:[],
        queryCriteria:{
          operatorName: "",
          startTime: "",
          endTime: "",
          orderBy: "",
          isAsc: true,
          pageIndex:1,
          pageSize: 10
        },
        pickerOptions: {
          shortcuts: [{
            text: '今天',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 1);
              picker.$emit('pick', [start, end]);
            }
          },{
            text: '最近一周',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit('pick', [start, end]);
            }
          }, {
            text: '最近一个月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit('pick', [start, end]);
              }
            }, {
            text: '最近三个月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit('pick', [start, end]);
            }
          }]
        },
        valueTime: ''
      };
    },
    methods: {
      onSubmit(){
        this.pageLoading = true;
        this.queryCriteria.startTime = "";
        this.queryCriteria.endTime ="";

        if(this.valueTime && this.valueTime.length>0){
          this.queryCriteria.startTime = this.valueTime[0];
          this.queryCriteria.endTime = this.valueTime[1];
        }
        this.settlementData();
      },
      handleSizeChange(val) {
        this.queryCriteria.pageSize = val;
        this.onSubmit();
      },
      handleCurrentChange(val) {
        this.queryCriteria.pageIndex = val;
        this.onSubmit();
      },
      settlementData(){
        this.pageLoading = true;
        settlementApi.GetSettlementList(this.queryCriteria).then((response)=>{
          this.settlementList = response;
          this.settlementList.result.forEach(settlement =>{
            settlement.settlementTime = new Date(settlement.settlementTime).toLocaleString();
            var obj = JSON.parse(settlement.settlementContent); //由JSON字符串转换为JSON对象
            settlement.Recharge = obj.Recharge;
            settlement.Refund = obj.Refund;
            settlement.Total = obj.Total;
            settlement.Cash = obj.Cash;
            settlement.WeChat = obj.WeChat;
            settlement.Alipay = obj.Alipay;
            settlement.UnionPay = obj.UnionPay;
          });

          this.pageLoading = false;
        }).catch(error =>{
          this.pageLoading = false;
        })  
      },      
      preview(data){
        this.detailFormConfig.formLoading = true;
        this.detailFormConfig.visible = true;
        settlementApi.GetSingleSettlementDetail(data.settlementRecordId).then((response)=>{
          this.detailList = response;
          this.detailFormConfig.visible = true;
          this.detailFormConfig.settlementId = data.settlementRecordId;
          this.detailFormConfig.formLoading = false;
        }).catch(error =>{
          this.detailFormConfig.formLoading = false;
        })
      },
      exportBillDetails(){
        location = process.env.BASE_API + '/Settlement/ExportSingleSettlementDetail?inputDto.SettlementId=' + this.detailFormConfig.settlementId
      },
      printBillDetails(){
        var oldHTML = window.document.body.innerHTML;
        window.document.body.innerHTML = this.$refs.detaiFromTable.$el.outerHTML;
        window.print(); //开始打印  
        this.$router.go('/dailySettleManage/dailySettleDetails');
      },
    },  
  };
</script>

<style rel="stylesheet/scss" lang="scss" scoped> 
  .el-header, .el-footer {
    color: #97a8be;
    line-height: 30px;
  }
  
  body > .el-container {
    margin-bottom: 40px;
  }

  .el-width {
  width: 250px;
}
.el-row {
  margin-bottom: 20px;
  &:last-child {
    margin-bottom: 0;
  }
}
.el-col {
  border-radius: 4px;
}
.bg-purple-dark {
  background: #99a9bf;
}
.bg-purple {
  background: #d3dce6;
}
.bg-purple-light {
  background: #e5e9f2;
}
.grid-content {
  border-radius: 4px;
  min-height: 36px;
}
.row-bg {
  padding: 10px 0;
  background-color: #f9fafc;
}
</style>