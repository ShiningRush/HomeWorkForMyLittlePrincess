<template>
<div >
 <el-form :model="listQuery" :inline="true">
 <el-row>
   
     <el-form-item label="卡号" >
     <el-input v-model="listQuery.cardNo" placeholder="卡号"></el-input>
     </el-form-item>
 
        <el-form-item label="姓名" >
        <el-input v-model="listQuery.name" placeholder="姓名"></el-input>
        </el-form-item>

  
        <el-form-item label="操作人员"  >        
         <el-select v-model="listQuery.creator"  filterable placeholder="操作人员" clearable>
         <el-option
            v-for="item in users"
            :key="item.userName"
            :label="item.userName"
            :value="item.id">
         </el-option>
       </el-select>
       </el-form-item>
  
  
        <el-form-item label="">
           <el-button type="primary" @click="search" icon="el-icon-search" v-has="'Search'">查询</el-button>
        <el-button  @click="exportDetails" icon="el-icon-message" v-has="'Export'">导出</el-button>
        </el-form-item>     
 </el-row>
 <el-row>

        <el-form-item label="操作时间" >
        <el-date-picker
                        v-model="valueTime"
                        type="datetimerange"
                        :picker-options="pickerOptions"                       
                        range-separator="至"
                        start-placeholder="开始时间"
                        end-placeholder="结束时间"
                        format = "yyyy-MM-dd HH:mm:ss"
                        value-format = "yyyy-MM-dd HH:mm:ss"
                        align="center">
                        </el-date-picker>
    </el-form-item>
 
  
        <el-form-item label="资金类型"  >        
         <el-select v-model="listQuery.capitalType"  filterable placeholder="资金类型" clearable>
         <el-option
            v-for="item in capitalTypes"
            :key="item.value"
            :label="item.value"
            :value="item.code">
         </el-option>
       </el-select>
       </el-form-item>

 </el-row>
</el-form> 
  <el-table :data="list"  v-loading.body="listLoading" element-loading-text="拼命加载中" fit highlight-current-row  style="width: 100%">
      <el-table-column align="center" label='姓名'>
        <template slot-scope="scope">
        {{scope.row.name}}
        </template>
      </el-table-column>    
      <el-table-column align="center" label='金额'>
        <template slot-scope="scope">
        {{scope.row.amount}}
        </template>
      </el-table-column>
     <el-table-column align="center" label='账户余额'>
        <template slot-scope="scope">
        {{scope.row.balance}}
        </template>
      </el-table-column>
     <el-table-column align="center" label='操作时间'>
        <template slot-scope="scope">
        {{scope.row.createTime}}
        </template>
      </el-table-column>
     <el-table-column align="center" label='操作员'>
        <template slot-scope="scope">
        {{scope.row.userName}}
        </template>
      </el-table-column>      
      <el-table-column align="center" label='流水类型'>
        <template slot-scope="scope">
        {{scope.row.billingType}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='资金类型'>
        <template slot-scope="scope">
        {{scope.row.capitalType}}
        </template>
      </el-table-column>
      <el-table-column align="center" label='应用场景'>
        <template slot-scope="scope">
        {{scope.row.applicationScene}}
        </template>
      </el-table-column>
  </el-table>
    <div v-show="!listLoading" class="block pagination" style="text-align: center;padding-bottom:20px;">
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page.sync="listQuery.pageIndex" :page-sizes="[10,20,30,50]" :page-size="listQuery.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </div>
</div>
</template>

<script>
import * as dataItemApi from  '@/api/dataItem'
import * as billingRecordApi from  '@/api/billingRecord'
import * as userApi from  '@/api/user'
export default {
    created(){
         this.getList()
         this.getAllUsers()
         this.getCapitalType()
        },
    data() {
      return {
        listQuery:{
            pageIndex:1,
            pageSize:10,
            cardNo:"",
            name:"",
            beginTime:"",
            endTime:"",
            creator:"",
            applicationScene:"",
            capitalType:"",                 
        },
        list:[],
        listLoading:false,
        total:null,
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
        valueTime: [],
        formLabelWidth: '100px',
        title:"",
        users:[],
        capitalTypes:[]
        
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
    getList(){
         this.listLoading = true;
         let request = this.listQuery;     
         this.listQuery.beginTime = "";
         this.listQuery.endTime ="";     
         if(this.valueTime && this.valueTime.length>0){
          this.listQuery.beginTime = this.valueTime[0];
          this.listQuery.endTime = this.valueTime[1];
         }    
         billingRecordApi.getBillingRecordList(request).then((response)=>{    
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
    //导出明细
    exportDetails(){
         let request = this.listQuery;
         window.location.href = process.env.BASE_API + '/billingRecord/DeriveBillingRecordList?cardNo=' + request.cardNo 
                                                                                            + '&name='+ request.name 
                                                                                            + '&creator='+ request.creator
                                                                                            + '&beginTime='+ request.beginTime
                                                                                            + '&endTime='+ request.endTime
                                                                                            + '&applicationScene='+ request.applicationScene
                                                                                            + '&applicationScene='+ request.applicationScene;
                                                                                            
    },
       getAllUsers(request) {
                var request = {};
                userApi.getSimpleUsers(request).then((response) => {
                    this.users = response;
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取操作员失败!'
                    });
                });
            },
         //获取资金类型下拉列表
       getCapitalType(){
           dataItemApi.getDetailByCode("CapitalType").then(resp=>{
            this.capitalTypes = resp;
          }).catch(error=>{
          })
      },
            

  }
}
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
 .el-dialog__wrapper /deep/ .el-dialog__header  {
    padding:0px;
    background-color: #394eff;
    height:40px;
    text-align: center;
   
    
 }
 .el-dialog__wrapper /deep/ .el-dialog__title{      
   color:white;
   line-height: 40px;
 }
  .input_width{
    width:200px;
  }
   body {
    margin: 0;
  }

</style>

