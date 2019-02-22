<template>
    <div>
        <el-form :model="input" :rules="inputRules" ref="input" label-width="100px" class="demo-ruleForm" >
            <el-form-item label="旧密码"  prop="oldPassword">
            <el-input type="password" v-model="input.oldPassword" auto-complete="off"></el-input>
            </el-form-item>
            <el-form-item label="新密码"  prop="newPassword">
            <el-input type="password" v-model="input.newPassword" auto-complete="off"></el-input>
            </el-form-item>
            <el-form-item label="重复新密码"  prop="repeatNewPassword">
            <el-input type="password" v-model="input.repeatNewPassword" auto-complete="off"></el-input>
            </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="close">取 消</el-button>
            <el-button type="primary" @click="submitChange('input')">确 定</el-button>
         </div>
    </div>
</template>

<script>
  import {changePassword} from '@/api/user'

export default {
    name:"changedPasswrod",
    props: {
    },
  data() {
      var validatePassEqual = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.input.newPassword) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return {
          input:{
              oldPassword:"",
              newPassword:"",
              repeatNewPassword:"",
          },
        inputRules: {
            oldPassword: [
                { required: true, message: '请输入旧密码', trigger: 'blur' },
                { min: 5, max: 10, message: '长度在 5 到 10 个字符', trigger: 'blur' }
            ],
            newPassword: [
                { required: true, message: '请输入新密码', trigger: 'blur' },
                { min: 5, max: 10, message: '长度在 5 到 10 个字符', trigger: 'blur' }
            ],
            repeatNewPassword: [
                 { validator: validatePassEqual, trigger: 'blur' }
            ],
        }
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
    submitChange(request) {
        this.$refs[request].validate((valid) => {
          if (valid) {
               changePassword(this.$data[request]).then((response) => {
                   alert("修改密码成功");
                this.$emit('close'); 
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '修改密码失败!'
                    });
                });
          } else {
            return false;
          }
        });
    },
    resetInput(){
        this.$refs.input.resetFields();
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss">
</style>
