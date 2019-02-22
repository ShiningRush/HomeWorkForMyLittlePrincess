<template>
  <div class="formContainer">
    <h1>系统初始化</h1>
    <el-form ref="form" :model="form" label-width="100px">
      <el-form-item label="数据库地址">
        <el-input v-model="form.dbAddress" :disabled="isInited"></el-input>
      </el-form-item>
      <el-form-item label="数据库名称">
        <el-input v-model="form.dbName" :disabled="isInited"></el-input>
      </el-form-item>
      <el-form-item label="数据库端口">
        <el-input v-model="form.dbPort" :disabled="isInited"></el-input>
      </el-form-item>
      <el-form-item label="数据库用户名">
        <el-input v-model="form.userId" :disabled="isInited"></el-input>
      </el-form-item>      
      <el-form-item label="数据库密码">
        <el-input type='password' v-model="form.password" :disabled="isInited"></el-input>
      </el-form-item>
      <el-row type="flex" justify="end">
        <el-col :span="20">
          <span style="color:red;" v-show="isInited">检测到系统已经初始化过数据库, 但是需要更新, 请点击开始按钮更新数据库</span>
        </el-col>
        <el-col :span="4">
          <el-button type="primary" :loading="loading" icon="el-icon-caret-right" @click="onInit">开始</el-button>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script>
  import globalSetting from '@/utils/globalSetting'
  import axios from 'axios'

  export default {
    created(){
    },
    data() {
      return {
        loading: false,
        form: {
          dbAddress: '127.0.0.1',
          dbName: 'ecardsystem',
          dbPort: '3306',
          userId: 'clear',
          password: 'clear123'
        },
        isInited: globalSetting.dbStatus.isInited
      }
    },
    methods: {
      onInit(){
        this.loading = true
        
        const backApi = axios({
              url:  process.env.SUB_API + '/SystemInit/InitialSystem',
              method: 'post',
              params:{
                dbAddress : this.form.dbAddress,
                dbName: this.form.dbName,
                dbPort: this.form.dbPort,
                userId: this.form.userId,
                password: this.form.password
              }
          });

        backApi.then((resp)=>{
          if(resp.data.code == '0'){
            this.onInitSuccessed()
          } else {
            this.onInitFailed(resp.data.message);
          }

        }).catch((error)=>{
          this.onInitFailed();
        })
      },
      onInitFailed(errorMessage){
        if(errorMessage === undefined)
          errorMessage = '数据库初始化失败'
        this.$message({
          message: errorMessage,
          type:'error'
        })
        this.loading = false
      },
      onInitSuccessed(){
        this.$message({
          message: '数据库初始化成功,即将跳转到登录页面',
          type:'success'
        })
        globalSetting.dbStatus.isNeedToInitPage = false
        setTimeout(() => {
          this.$router.push('/login')
        }, 3000);
      }
    }
  }
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  .formContainer{
    width: 764px;
    height: auto;
    border:  solid .5px gray;
    margin: 10px auto;
    border-radius: 5px;
    background-color: white;
    padding: 24px;
  }

  h1 {
    font-size: 30px;
    padding-bottom: 9px;
    margin: 10px 0 20px 0;
    border-bottom: 1px solid #eee;
  }

</style>