<template>
    <div style="margin-left:10px;margin-right:20px;margin-top:30px">
    <!-- 查询控件 -->
        <el-form :inline="true" :model="queryconditions" class="demo-form-inline">
            <el-form-item label="日结截止时间">
                <el-date-picker  v-model="queryconditions.settleTime" type="datetime" placeholder="选择日期时间"></el-date-picker>
            </el-form-item>
            <el-button type="primary" icon="el-icon-search" @click="search()" v-has="'Search'" >查询</el-button>
            <el-button icon="el-icon-tickets" @click="settleAccounts()" v-has="'SettledBilling'" >结算</el-button>
            <el-button  icon="el-icon-printer" @click="print()" v-has="'Print'" >打印</el-button>
        </el-form>
        <el-card style="margin-top:30px" >
            <div slot="header" class="clearfix">
                <span>{{name}}</span>
            </div> 
            <iframe id="dailySettleReport" width="100%" height="600px" frameborder="0" marginheight="0" marginwidth="0" src='/static/settlementReport.htm'></iframe>
        </el-card>
    </div>
</template>
<script>
import { mapGetters } from 'vuex'
import * as settlementApi from '../../api/settlement'

export default {
    created(){
    },
    data() {
        return {
        //查询条件实体
            queryconditions:{
                settleTime:'',//结束时间
            },
        //日结报表变量
            dailySettleReport: '<p style="text-align:center">暂无数据</p>',
        }
    },
    computed: {...mapGetters(['name'])},
    methods: {
    //查询
        search(){
            if(this.queryconditions.settleTime == ''){
                this.$message.error('请选择时间'); 
            }
            else{
                settlementApi.GetNotSettledBillingGraphData(this.queryconditions.settleTime).then(resp=>{                
                        document.getElementById('dailySettleReport').contentWindow.transmitDataFunction(resp);
                    }).catch(error=>{
                    });
            }
        },
    //打印
        print(){
            window.document.body.innerHTML = document.getElementById("dailySettleReport").contentWindow.document.querySelector('body').innerHTML; //获得 dailySettleReport 里的所有 html 数据
            window.print(); //开始打印  
            this.$router.go('/dailySettleManage/dailySettleReport');
        },
    //结算
        settleAccounts(){
            if(this.queryconditions.settleTime == ''){
                this.$message.error('请选择时间'); 
            }
            else{
                this.$confirm('是否确定结算?', '提示', {confirmButtonText: '确定',cancelButtonText: '取消',type: 'warning'}).then(() => {
                    settlementApi.SettleBillingRecord(this.queryconditions.settleTime).then(resp=>{
                        this.$message({type: 'success',message: '结算成功!'});
                    }).catch(error=>{
                        reject(error);
                        this.$message({type: 'error',message: '结算失败!'}); 
                    })
                }).catch(() => {
                            
                });
            }
        },
    }
}
</script>