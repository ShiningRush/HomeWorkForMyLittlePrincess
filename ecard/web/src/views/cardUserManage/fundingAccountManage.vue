<template>
    <div style="margin-left:10px;margin-right:20px;margin-top:30px">
    <!-- 查询，充值 -->
        <el-form :inline="true" class="demo-form-inline">
            <el-form-item label="就诊卡号">
                <div style="width: 500px;">
                <el-input v-model="cardNum" placeholder="卡号" v-on:change="inputChange"></el-input>
                </div>
            </el-form-item>
            <el-button type="primary" icon="el-icon-search" @click="searchClick" v-has="'Search'"  >查询</el-button>
            <el-button type="primary" icon="el-icon-goods" @click="openRechargeDialogClick" :disabled="RechargeButonDisabled" v-has="'Recharge'" >充值</el-button>        
        </el-form>
    <!-- 账户信息卡片展示 -->
        <el-card class="box-card">
            <div slot="header" class="clearfix">
                <span>{{accountInformation.name}}</span>
            </div>            
            <el-form :inline="true" :model="accountInformation" class="demo-form-inline">
                <el-row :gutter="20">
                    <el-col :span="6">
                        <el-form-item label="账户类型：" :label-width="formLabelWidth">
                            <span>{{accountInformation.accountTypeName}}</span>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="手机号码：" :label-width="formLabelWidth">
                            <span>{{accountInformation.mobile}}</span>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="性别：" :label-width="formLabelWidth">
                            <span>{{accountInformation.sexName}}</span>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="年龄：" :label-width="formLabelWidth">
                            <span>{{accountInformation.age}}</span>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row :gutter="20">
                    <el-col :span="6">
                        <el-form-item label="证件类型：" :label-width="formLabelWidth">
                            <span>{{accountInformation.idCardTypeName}}</span>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="证件号码：" :label-width="formLabelWidth">
                            <span>{{accountInformation.idCardNo}}</span>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="总余额：" :label-width="formLabelWidth">
                            <span>{{accountInformation.balance}}</span>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                    </el-col>
                </el-row>
            </el-form>
        </el-card>
    <!-- 账户管理Talbe -->
        <el-main>
            <el-table :data="accountManageTable"  element-loading-text="拼命加载中" fit highlight-current-row style="width: 100%">   
                <el-table-column type="index" width="50" label="序号"></el-table-column>          
                <el-table-column prop="capitalTypeName" label="资金类型"></el-table-column>
                <el-table-column prop="balance" label="金额"></el-table-column>
                <el-table-column label="操作" width="200">
                    <template slot-scope="scope">
                        <el-button type="text" size="mini" @click="openHandleFinancialDetailsDialogClick(scope.row)" v-has="'BillingList'" >资金明细</el-button>
                        <el-button type="text" size="mini" @click="openHandleRechargeDialogClick(scope.row)" v-has="'Recharge'" >充值</el-button>
                        <el-button type="text" size="mini" @click="openHandleReturnPremiumDialogClick(scope.row)" v-has="'Refund'" >退费</el-button>                  
                    </template>
                </el-table-column>
            </el-table>
        </el-main>
    <!-- 资金明细 -->
        <el-dialog :visible.sync="financialDetailsDialogFormVisible" title="资金明细">        
            <el-form :inline="true" ref="orgform">
                <el-table :data="financialDetails" element-loading-text="拼命加载中" fit highlight-current-row style="width: 100%">  
                    <el-table-column type="index" width="50" label="序号"></el-table-column>             
                    <el-table-column prop="name" label="姓名"></el-table-column>
                    <el-table-column prop="amount" label="金额"></el-table-column>
                    <el-table-column prop="billingType" label="类型"></el-table-column>
                    <el-table-column prop="createTime" label="操作时间"></el-table-column>
                    <el-table-column prop="userName" label="操作人"></el-table-column>
                </el-table>
                <div align="center">
                <el-pagination background layout="prev, pager, next" :total="financialDetailsQueryConditions.total" @current-change="handleCurrentChange" :current-page="financialDetailsQueryConditions.pageIndex" :page-size="8"> </el-pagination>
                </div>
            </el-form>
        </el-dialog>  
    <!-- 充值退费弹窗 -->
        <el-dialog :visible.sync="accountInformationDialogFormVisible" :title="rechargeRefund.dialogTitle" width=550px>        
            <el-form :inline="true" :model="rechargeRefund"  ref="orgform">
                <el-row>
                    <el-form-item label="资金类型" required :label-width="formLabelWidth">
                        <el-select v-model="rechargeRefund.capitalType" placeholder="资金类型" :disabled="rechargeRefund.moneyTypeDisabled" class="input_width">
                            <el-option v-for="item in moneyTypeLevelOptions" :key="item.code" :label="item.value" :value="item.code"></el-option>
                        </el-select>
                    </el-form-item>
                </el-row>
                <el-row>
                    <el-form-item label="支付方式" required :label-width="formLabelWidth">
                        <el-select v-model="rechargeRefund.payType" placeholder="支付方式" class="input_width">
                            <el-option v-for="item in paymentModeLevelOptions" :key="item.code" :label="item.value" :value="item.code"></el-option>
                        </el-select>
                    </el-form-item>
                </el-row>
                <el-row>
                    <el-form-item label="金额" required :label-width="formLabelWidth">
                        <el-input v-model="rechargeRefund.amount" placeholder="金额" :maxlength="maxlength" class="input_width"></el-input>
                    </el-form-item>
                </el-row>
            </el-form>            
            <div slot="footer" class="dialog-footer">
                <el-button @click="accountInformationDialogFormVisible = false">取 消</el-button>
                <el-button type="primary" @click="rechargeRefundClick" v-dbClick>确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>
<script>
import * as dataItemApi from "@/api/dataItem";
import * as accountApi from "@/api/account";
import * as billingRecordApi from "@/api/billingRecord.js";
import * as ageApi from "@/utils/age";
export default {
    created(){
        this.ini();//初始化
    },
    methods: {
        //初始化控件
            ini(){
                //资金类型下拉框
                dataItemApi.getDetailByCode("CapitalType").then(resp=>{
                this.moneyTypeLevelOptions = resp;
                }).catch(error=>{
                });
                //支付方式下拉框
                dataItemApi.getDetailByCode("PayType").then(resp=>{
                this.paymentModeLevelOptions = resp;
                }).catch(error=>{
                });
            },
        //查询事件
            searchClick(){
                this.search();
            },
        //充值
            openRechargeDialogClick(){
                this.rechargeRefund.payType='';
                this.rechargeRefund.capitalType='';
                this.rechargeRefund.amount='1';
                this.rechargeRefund.dialogTitle="充值";
                this.rechargeRefund.moneyTypeDisabled=false;
                this.accountInformationDialogFormVisible=true;
            },
        //Table中的资金明细操作
            openHandleFinancialDetailsDialogClick(row){
                this.financialDetailsQueryConditions.pageIndex=1;//页码
                this.financialDetailsQueryConditions.pageSize=8;//页面大小
                this.financialDetailsQueryConditions.CapitalType=row.capitalType;
                this.financialDetailsSearch();
                this.financialDetailsDialogFormVisible=true;
            },
        //Table中的充值操作
            openHandleRechargeDialogClick(row){
                this.rechargeRefund.dialogTitle="充值";
                this.rechargeRefund.capitalType=row.capitalType;
                this.rechargeRefund.payType='';
                this.rechargeRefund.amount='1';
                this.rechargeRefund.moneyTypeDisabled=true;
                this.accountInformationDialogFormVisible=true;
            },
        //Table中的退费操作
            openHandleReturnPremiumDialogClick(row){
                this.rechargeRefund.dialogTitle="退费";
                this.rechargeRefund.capitalType=row.capitalType;
                this.rechargeRefund.payType='';
                this.rechargeRefund.amount='1';
                this.rechargeRefund.moneyTypeDisabled=true;
                this.rechargeRefund.tempAmount=row.balance;
                this.accountInformationDialogFormVisible=true;
            },      
        //充值退费窗体确定按钮事件
            rechargeRefundClick(){
                if(this.rechargeRefund.capitalType==""){
                    this.$message.error('请选择资金类型');        
                }
                else if(/^\d+(?=\.{0,1}\d+$|$)/.test(this.rechargeRefund.amount) == false||this.rechargeRefund.amount<=0){
                    this.$message.error('金额必须为数字且大于0');        
                }
                else if(this.rechargeRefund.payType==""){
                    this.$message.error('请选择支付方式');        
                }
                else if(this.rechargeRefund.dialogTitle=="充值"){
                //充值
                    accountApi.AccountRecharge(this.rechargeRefund).then(resp=>{
                        this.accountInformationDialogFormVisible=false;
                        this.$message({type: 'success',message: '充值成功!'});
                        this.search();
                    }).catch(error=>{
                        this.$message.error('充值失败!'); 
                    });
                }
                else if(this.rechargeRefund.amount>this.rechargeRefund.tempAmount){
                    this.$message.error('当前余额为：'+this.rechargeRefund.tempAmount+'，不够退款'); 
                }
                else{
                //退费
                    accountApi.AccountDeductionFee(this.rechargeRefund).then(resp=>{
                        this.accountInformationDialogFormVisible=false;
                        this.$message({type: 'success',message: '退费成功!'});
                        this.search();
                    }).catch(error=>{
                        reject(error);
                        this.$message.error('退费失败!'); 
                    });
                }
            },
        //账户管理页码跳转方法
            handleCurrentChange(val) {                
                this.financialDetailsQueryConditions.pageIndex = val;
                this.financialDetailsSearch();
            },
        //查询事件
            search(){
                accountApi.GetAccountByCard(this.cardNum).then(resp=>{
                    this.accountInformation = resp;
                    this.accountInformation.age = ageApi.displayAge(resp.birthDay);
                    this.accountManageTable = resp.capitals;
                    this.rechargeRefund.accountId = resp.id;
                    this.financialDetailsQueryConditions.AccountId = resp.id;
                    this.accountInformation.balance = 0;
                    resp.capitals.forEach(element => {
                        this.accountInformation.balance = this.accountInformation.balance+element.balance;
                    });
                    this.RechargeButonDisabled=false;
                }).catch(error=>{
                });
            },
        //资金明细查询事件
            financialDetailsSearch(){
                billingRecordApi.getBillingRecordList(this.financialDetailsQueryConditions).then(resp=>{
                    resp.result.forEach(element => {
                        if(element.createTime && element.createTime != ""){
                            element.createTime = new Date(element.createTime).toLocaleDateString();                
                        }
                    });
                    this.financialDetails=resp.result;
                    this.financialDetailsQueryConditions.total = resp.totalCount;
                }).catch(error=>{
                });
            },
        //查询输入框输入内容改变事件
            inputChange(){
                if(!this.RechargeButonDisabled){
                    this.accountInformation={};
                    this.accountManageTable=[];
                    this.RechargeButonDisabled=true;
                }
            },
    }, 
    data() {
        return {
        //查询条件：卡号
            cardNum:'',//卡号
        //资金明细查询条件实体
            financialDetailsQueryConditions:{
                AccountId:1,//账户id
                CapitalType:'',//资金账户类型
                pageIndex: 1,//页码
                pageSize: 5,//页面大小
                total: 0,//总页数
            },
        //账户信息实体
            accountInformation:{
                accountTypeName:'',//账户类型
                mobile:'',//电话号码
                sex:'',//性别
                idCardTypeName:'',//证件类型
                idCardNo:'',//证件号码
                age:'',//年龄??
                balance:'',//总余额??
                noSettlement:'',//未结算??
                name:'',//姓名
            },
        //账户管理表格实体
            accountManageTable:[],
        //弹窗变量
            accountInformationDialogFormVisible:false, //打开充值退费弹窗        
            financialDetailsDialogFormVisible:false,//打开资金明细弹窗
        //label宽度
            formLabelWidth:'90px',
        //弹窗资金输入框字符长度限制
            maxlength:15,
        //充值按钮是否可以
            RechargeButonDisabled:true,
        //下拉框变量
            moneyTypeLevelOptions:[],//资金类型下拉框    
            paymentModeLevelOptions:[],//支付方式下拉框
        //充值退费弹窗对象
            rechargeRefund:{
                dialogTitle:'222',//窗体名称，用于判断是充值还是退款
                amount:0,//充值退款金额
                payType:'',//支付方式
                capitalType:'',//资金类型
                accountId:"1",//账户ID
                moneyTypeDisabled:false,//资金类型空间是否置灰
                tempAmount:0,//充值退款金额临时变量（非接口变量）
            },
        //资金明细弹窗对象
            financialDetails:[],
        };
    }
};
</script>
<style rel="stylesheet/scss" lang="scss">
    .input_width{
    width:400px;
    }
</style>