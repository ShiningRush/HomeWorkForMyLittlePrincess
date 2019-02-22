<template>
    <div style="margin-left:10px;margin-right:20px;margin-top:30px">
        <el-table :data="sys_app"  style="width: 100%"  v-loading.body="listLoading">            
            <!-- <el-table-column prop="appID" label="应用ID"></el-table-column> -->
            <el-table-column prop="appName" label="应用名称"></el-table-column>
            <el-table-column prop="expireTime" label="到期时间"></el-table-column>
            <el-table-column label="是否停用">
                <template slot-scope="scope">
                    <el-switch disabled v-model="sys_app.isStop" active-color="#13ce66" inactive-color="#ff4949"></el-switch>
                </template>
            </el-table-column>
        </el-table>
    </div>
</template>

<script>
import * as authorizationApi from '../../api/authorization'

export default {
    created(){
        this.getList();
        },
    data() {
      return {
        sys_app:[],
        listLoading:false,
    }
  },
  methods: {
      getList(){
            this.listLoading = true;
            authorizationApi.getAllApp().then(resp=>{
            resp.forEach(element => {
                if(element.expireTime && element.expireTime != ""){
                    element.expireTime = new Date(element.expireTime).toLocaleDateString();                
                }
            });

            this.sys_app= resp;
            this.listLoading = false;
        }).catch(error=>{
            this.listLoading = false;
        });
      }
  }
}
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
 body {
    margin: 0;
  }
</style>
