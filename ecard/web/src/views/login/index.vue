<template>
  <div class="login-container">
    <el-form autoComplete="on" :model="loginForm" :rules="loginRules" ref="loginForm" label-position="left" label-width="0px"
      class="login-form">
      <div class="client-logo">
                <img :src="logo" style="border-radius: 50px;border: 2px solid #f5f5f5;" />
       </div>
      <h3 class="title">一卡通系统</h3>
      <el-form-item prop="username">  
        <el-input name="username" type="text" v-model="loginForm.username" autoComplete="on" placeholder="账号" class="input_field"/>
      </el-form-item>
      <el-form-item prop="password" >    
        <el-input name="password" type="password" @keyup.enter.native="handleLogin" v-model="loginForm.password" autoComplete="on"
          placeholder="密码" class="input_field"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" style="width:90%;" :loading="loading" @click.native.prevent="handleLogin" >
          登录
        </el-button>
      </el-form-item>     
       <div class="copyright">深圳市巨鼎医疗设备有限公司 Copyright © 2018</div>
    </el-form>   
  </div>
</template>

<script>
import { ssoLogin } from '@/api/ssoAuthenticate'
import { oauthTokenManager } from '@/utils/auth'
import { Message } from 'element-ui'
import logo from '@/assets/logo.png'
import '@/assets/js/jquery.min'
//import { run3Dbackground } from '@/assets/js/login-background'

export default {
  name: 'login',
  created(){
    //this.stopFunction = run3Dbackground();
    if(this.$route.query.token && this.$route.query.userName){
      this.loading = true
      ssoLogin(this.$route.query.token, this.$route.query.userName).then((resp)=>{
        oauthTokenManager.setToken(resp.data)
        this.loading = false
        this.loginToSystem()
      }).catch((error)=>{
        this.loading = false
        Message({message:"统一登录失败", type:'error'})
      })
    }
  },
  data() {
    const validateUsername = (rule, value, callback) => {
      if ( !value || value == "") {
        callback(new Error('请输入正确的用户名'))
      } else {
        callback()
      }
    }
    const validatePass = (rule, value, callback) => {
      if (value.length < 5) {
        callback(new Error('密码不能小于5位'))
      } else {
        callback()
      }
    }
    return {
      loginForm: {
        username: '',
        password: ''
      },
      loginRules: {
        username: [{ required: true, trigger: 'blur', validator: validateUsername }],
        password: [{ required: true, trigger: 'blur', validator: validatePass }]
      },
      loading: false,
      logo
    }
  },
  methods: {
    handleLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$store.dispatch('Login', this.loginForm).then(() => {
            this.loading = false
            //this.stopFunction()
            this.loginToSystem()
          }).catch((error) => {
            this.loading = false
            Message({message:"登录失败,用户名与密码不匹配"})
          })
        } else {
          return false
        }
      })
    },
    loginToSystem(){    
      if(this.$route.query.goPath){
        this.$router.push({ path: this.$route.query.goPath })
      } else{
        this.$router.push({ path: '/' })
      }
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" >
  @import "src/styles/mixin.scss";

  $bg:#f4f7fa;
  $dark_gray:#fff;
  $light_gray:#2b3067;

 body {
    margin: 0;
    padding: 0;
    overflow: hidden;
}
  .login-container {
    @include relative;
    height: 100vh;
    background-color: $bg;
    input {
      background: transparent;
      border-top-style:none;
      border-left-style:none;
      border-right-style: none;
      border-bottom-style: solid gray;
      -webkit-appearance: none;
      border-radius: 0px;
      padding: 12px 5px 12px 15px;
      color: $light_gray;
      height: 47px;
      background-color: $dark_gray
    }
    .client-logo {
    margin-top: 10px;
    text-align: center;
    }
    .el-input {
      display: inline-block;
      height: 47px;
      width: 85%;
    }
   
    .title {     
      color: $light_gray;
      margin: 30px 0 40px 0;
      text-align: center;
      font-weight: bold;
      font-size: 32px;
      text-align: center;   
      font-weight: 600;
    }
    .login-form {
      position: absolute;
      left: 0;
      right: 0;
      width: 400px;
      padding: 35px 35px 15px 35px;
      margin: 120px auto;
      background-color: #fff;
      z-index:2
    }
    .input_field{
      border-top-style:none;
      border-left-style:none;
      border-right-style: none;
      border-bottom-style: solid gray;
    }
     .el-form-item {
      color: $light_gray;
      background-color: $dark_gray
     }    
    .copyright {
    position: absolute;
    top: 555px;
    text-align: center;
    margin-left: 20px;
    font-size: 14px;
    color: #9a9db7;
}
  }
</style>
