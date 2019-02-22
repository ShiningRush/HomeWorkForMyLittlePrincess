<template>
<div style="margin-left:10px">
 <el-form :inline="true" :model="listQuery">
  <el-form-item label="卡务类型">
     <el-select v-model="listQuery.operationType" placeholder="卡务类型" clearable>
             <el-option v-for="item in opreateTypes" :key="item.code" :label="item.value" :value="item.code">               
             </el-option>
            </el-select>
  </el-form-item>
  <el-form-item label="卡号">
    <el-input v-model="listQuery.cardNo" placeholder="卡号"></el-input>
  </el-form-item>
  <el-form-item label="姓名">
    <el-input v-model="listQuery.name" placeholder="姓名"></el-input>
  </el-form-item>
   <el-form-item label="身份证号">
    <el-input v-model="listQuery.idno" placeholder="身份证号"></el-input>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="search" icon="el-icon-search" v-has="'Search'" >查询</el-button>
  </el-form-item> 
</el-form> 
  <el-table :data="list"  v-loading.body="listLoading" element-loading-text="拼命加载中" fit highlight-current-row  style="width: 100%">
      <el-table-column align="center" label='卡号'>
        <template slot-scope="scope">
        {{scope.row.cardNo}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='姓名'>
        <template slot-scope="scope">
        {{scope.row.name}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='身份证号'>
        <template slot-scope="scope">
        {{scope.row.idCardNo}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='卡务类型'>
        <template slot-scope="scope">
        {{scope.row.operationTypeName}}
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
        {{scope.row.creatorName}}
        </template>
      </el-table-column>
       <el-table-column align="center" label='原因'>
        <template slot-scope="scope">
        {{scope.row.remark}}
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
import * as cardLogApi from  '@/api/cardLog'

export default {
    created(){
        this.getList()
        this.getOperationType()
        },
    data(){
      return {
        listQuery:{
            pageIndex:1,
            pageSize:10,
            operationType:"",
            cardType:"",
            cardNo:"",
            idno:"",
            name:""
        },
        list:[],
        listLoading:false,
        total:null,
        opreateTypes:[]
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
        cardLogApi.getCardLogs(request).then((response)=>{                                   
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
      //获取账户类型下拉列表
      getOperationType(){
         dataItemApi.getDetailByCode("CardOperationType").then(resp=>{
         this.opreateTypes = resp;
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

