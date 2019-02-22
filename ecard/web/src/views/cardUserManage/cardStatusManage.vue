<template>
<div style="margin-left:10px">
 <el-form :inline="true" :model="listQuery">
  <el-form-item label="卡类型">
             <el-select v-model="listQuery.cardType" placeholder="卡类型" clearable>
             <el-option v-for="v in cardTypes" :key="v.code" :label="v.value" :value="v.code">               
             </el-option>
            </el-select>                          
  </el-form-item>
  <el-form-item label="卡号">
    <el-input v-model="listQuery.cardNo" placeholder="卡号"></el-input>
  </el-form-item>
  <el-form-item label="姓名">
    <el-input v-model="listQuery.name" placeholder="姓名"></el-input>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="search" icon="el-icon-search" v-has="'Search'"  >查询</el-button>
  </el-form-item> 
</el-form> 
  <el-table :data="list"  v-loading.body="listLoading" element-loading-text="拼命加载中" fit highlight-current-row  style="width: 100%" @current-change="handleCardCurrentChange">
      <el-table-column align="center" label='卡号'>
        <template slot-scope="scope">
        {{scope.row.cardNo}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='姓名'>
        <template slot-scope="scope">
        {{scope.row.accountName}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='身份证号'>
        <template slot-scope="scope">
        {{scope.row.accountIDCardNo}}
        </template>
      </el-table-column>  
        <el-table-column align="center" label='卡类型'>
        <template slot-scope="scope">
        {{scope.row.cardTypeName}}
        </template>
      </el-table-column>        
       <el-table-column align="center" label='卡状态'>
        <template slot-scope="scope">
        {{scope.row.statusName}}
        </template>
      </el-table-column>
       <el-table-column align="center" label='密码验证'>
        <template slot-scope="scope">
           <el-switch v-model="scope.row.isPasswordAuth" active-color="#13ce66" inactive-color="#ff4949"  disabled></el-switch>
        </template>
      </el-table-column>      
       <el-table-column label="操作" align="center" width="300px">
        <div slot-scope="scope">
        <el-button  size="mini"  @click="lost(scope.row)" v-has="'Loss'"  v-show="!scope.row.isOnlyCancellation">{{scope.row.statusName == "挂失"?"解挂":"挂失"}}</el-button>
        <el-button  size="mini"  @click="replace(scope.row)" v-has="'ReplaceCard'" v-show="!scope.row.isOnlyCancellation" >补换卡</el-button>
        <el-button  size="mini" @click="recede(scope.row)" v-has="'CancellationCard'"  >退卡</el-button> 
        </div>
      </el-table-column>   
  </el-table>
    <div v-show="!listLoading" class="block pagination" style="text-align: center;padding-bottom:20px;">
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page.sync="listQuery.pageIndex" :page-sizes="[10,20,30,50]" :page-size="listQuery.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </div>
    <el-dialog title="提示" :visible.sync="dialogVisible" width="30%">
     <el-row>
      <el-col :span="4">
          <span style="line-height:30px">{{operateName}}原因</span>
      </el-col>
      <el-col :span="16">
        <el-input v-model="reason" placeholder="原因" ></el-input>
      </el-col>
    </el-row> 
    <el-row style="margin-top:10px;" v-show="isNewCard">
      <el-col :span="4">
          <span style="line-height:30px">新卡卡号</span>
      </el-col>
      <el-col :span="12">
        <el-input v-model="newCardNo" placeholder="卡号"></el-input>
      </el-col>
      <el-col :span="4" style="margin-left:20px;">
         <el-button type="primary" size="small" >读卡</el-button> 
      </el-col>
    </el-row>     
    <p style="color:red">系统提示：</p>
    <span style="color:red">{{tips}}</span>
    <span slot="footer" class="dialog-footer">    
    <el-button @click="dialogVisible = false">取 消</el-button>
    <el-button type="primary" @click="submitReason" v-dbClick>确 定</el-button>
  </span>
</el-dialog>
</div>
</template>

<script>
import * as cardApi from  '@/api/card'
import * as dataItemApi from  '@/api/dataItem'
export default {
    created(){
        this.getList()
        this.getCardType()
        },
    data() {
      return {
        listQuery:{
            pageIndex:1,
            pageSize:10,
            cardType:"",
            cardNo:"",          
            name:"",           
        },
         pickerOptions: {
          shortcuts: [{
            text: '今天',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
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
        valueTime: '',
        list:[],
        listLoading:false,
        total:null,
        dialogVisible:false,
        tips:"",
        reason:"",
        isNewCard:false,
        isRecede:false,
        cardCurrentRow:undefined,
        operateName:"",
        newCardNo:"",
        cardTypes:[]
    }
  },
  methods: {
      handleSizeChange(val) {
      this.listQuery.pageSize = val;
      this.getList();
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.getList();
    },
    handleCardCurrentChange(val){
       this.cardCurrentRow = val;
    },
    getList(){
        this.listLoading = true;  
        let request = this.listQuery;
        cardApi.getCards(request).then((response)=>{                                   
                  this.list = response.result;
                  this.total = response.totalCount;  
                  this.listLoading = false;                               
             }).catch(error =>{
                 this.listLoading = false;    
             })     
      
    },
    search(){
      this.getList();
    },
    lost(data){
       if(data.status == 0){
           this.operateName = "挂失";
           this.tips = "挂失后，此卡卡号将被自动作为黑名单下发到所有系统下辖设备上，以禁止此卡片被使用。";
       }else{
            this.operateName = "解挂";
            this.tips = "解挂后，将恢复卡为正常状态。"
       }    
        
       this.isNewCard = false;
       this.isRecede = false;
       this.dialogVisible = true;   
       this.reason = "";   
    },
    submitReason(){
     if(!this.reason ||this.reason == ""){
            this.$message({
                    type: 'error',
                    message: '请输入原因!'
                });   
           return false;         
        }
      let request = {
          accountId: this.cardCurrentRow.accountId,
          cardType: this.cardCurrentRow.cardType,          
          reason: this.reason
        };
      let replaceRequest = {
          accountId: this.cardCurrentRow.accountId,
          cardType: this.cardCurrentRow.cardType,
          oldCardNo: this.cardCurrentRow.cardNo,
          newCardNo:this.newCardNo ,
          reason: this.reason
        };
     switch(this.operateName)
        {
        case "挂失":
       
        cardApi.loss(request).then((response)=>{   
                  this.getList();    
                  this.dialogVisible = false;           
                  this.$message({
                    type: 'success',
                    message: '挂失成功!'
                });                                          
             }).catch(error =>{
             })     
        break;
        case "解挂" :        
        cardApi.releaseLoss(request).then((response)=>{                                  
                  this.getList();    
                  this.dialogVisible = false;           
                  this.$message({
                    type: 'success',
                    message: '解挂成功!'
                });                                          
             }).catch(error =>{
             })     
        break;
        case "补换卡" :
        if(!this.newCardNo ||this.newCardNo == ""){
            this.$message({
                    type: 'error',
                    message: '请输入新卡卡号!'
                });   
           return false;         
        }
       
        cardApi.replaceCard(replaceRequest).then((response)=>{                                  
                  this.getList();    
                  this.dialogVisible = false;           
                  this.$message({
                    type: 'success',
                    message: '补换卡成功!'
                });                                          
             }).catch(error =>{
             })     
        break;
        case "退卡" :       
        cardApi.cancellationCard(request).then((response)=>{                                  
                  this.getList();    
                  this.dialogVisible = false;           
                  this.$message({
                    type: 'success',
                    message: '退卡成功!'
                });                                          
             }).catch(error =>{
             })     
        break;
        
        }
    },
    
    replace(data){
          this.operateName = "补换卡";
          this.tips = " 补换新卡后，原卡卡号将被自动作为黑名单下发到所有系统下辖设备上，以禁止原卡片被使用。";
          this.isNewCard = true;
          this.isRecede = false;
          this.dialogVisible = true;
          this.reason = "";
          this.newCardNo = "";
    },
    recede(data){
         this.operateName = "退卡";
         this.tips = "  退卡后，该卡将被禁用，请确认当前持卡人已应完成支付、退费、清退余额等操作。";
         this.isNewCard = false;
         this.isRecede = true;
         this.dialogVisible = true;
         this.reason = ""
    },
    //获取卡类型下拉列表
       getCardType(){
           dataItemApi.getDetailByCode("CardType").then(resp=>{
            this.cardTypes = resp;
          }).catch(error=>{
          })
      },


  }
}
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
 body {
    margin: 0;
  }
</style>

