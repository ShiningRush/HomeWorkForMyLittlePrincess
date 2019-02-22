<template>
<div style="margin-left:10px;">
        <el-form>
            <el-form-item>
              <el-form :inline="true">
                <el-row :gutter="50" style="text-align: right">
                  <el-col :span="50" style="text-align: left">
                    <el-form-item prop="clientIpAddress" label="访问来源IP">
                      <el-input auto-complete="off" v-model="queryCriteria.clientIpAddress" placeholder="请输入访问来源IP"></el-input>
                    </el-form-item>
                    <el-form-item prop="keyword" label="操作时间">                     
                    <el-date-picker
                      v-model="valueTime"
                      type="datetimerange"
                      :picker-options="pickerOptions"
                      range-separator="至"
                      start-placeholder="开始时间"
                      end-placeholder="结束时间"
                      format = "yyyy-MM-dd HH:mm:ss"
                      value-format = "yyyy-MM-dd HH:mm:ss"
                      align="right">
                    </el-date-picker>
                    </el-form-item>
                    <el-form-item>
                      <el-button type="primary" icon="el-icon-search" @click="onSubmit" v-has="'Search'">查询</el-button>
                    </el-form-item>
                  </el-col>
                </el-row>
              </el-form>
            </el-form-item>
        </el-form>
        <el-table :data="logData.result" style="width:100%" v-loading.body="listLoading" fit highlight-current-row  >
          <el-table-column prop="module" label="模块"></el-table-column>
          <el-table-column prop="operationType" label="操作类型 "></el-table-column>
          <el-table-column prop="createTime" label="操作时间"></el-table-column>
          <el-table-column prop="clientIpAddress" label="访问来源IP"></el-table-column>
          <el-table-column prop="creatorName" label="操作人"></el-table-column>
          <el-table-column prop="context" label="操作描述"></el-table-column>
        </el-table>
        <div class="block pagination" style="text-align:center ">
          <el-pagination
              @size-change="handleSizeChange"
              @current-change="handleCurrentChange"
              :current-page="currentPage"
              :page-sizes="[10,20,30,50]"
              :page-size="queryCriteria.pageSize"
              layout="total, sizes, prev, pager, next, jumper"
              :total="logData.totalCount">
          </el-pagination>
        </div>
 </div>  
</template>

<script>
  import * as oprLogApi from "@/api/oprLog"
  import * as dateTimeApi from "@/utils/index"
  export default {
    created(){
       this.getLogData();
    },
    data() {
      return {
        currentPage:1,
        logData:{
          result:[],
          totalCount: 0,
          pageIndex: 0,
          pageSize: 0
        },
        queryCriteria:{
          operationType: "",
          module: "",
          clientIpAddress: "",
          startTime: "",
          endTime: "",
          orderBy: "",
          isAsc: true,
          pageIndex: 1,
          pageSize: 10
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
        listLoading:false,
      };
    },
    methods: {
      onSubmit(){
        this.queryCriteria.startTime = "";
        this.queryCriteria.endTime ="";
        if(this.valueTime && this.valueTime.length>0){
          this.queryCriteria.startTime = this.valueTime[0];
          this.queryCriteria.endTime = this.valueTime[1];
        }
        this.getLogData();
      },
      handleSizeChange(val) {
        this.queryCriteria.pageSize = val;
        this.onSubmit();
      },
      handleCurrentChange(val) {
        this.queryCriteria.pageIndex = val;
        this.onSubmit();
      },
      getLogData(){
        this.listLoading = true;
        oprLogApi.getOprLogs(this.queryCriteria).then((response)=>{
            response.result.forEach(element => {
                if(element.createTime && element.createTime != ""){
                 element.createTime = dateTimeApi.parseTime(element.createTime);
                }
            });
            this.logData = response
            this.listLoading = false;
        }).catch(error =>{
           this.$message({
              type: 'error',
              message: '获取列表信息失败!'
           });
           this.listLoading = false;
        })
      }
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