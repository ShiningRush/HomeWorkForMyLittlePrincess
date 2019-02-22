<template>
<div>
  <el-menu class="navbar" mode="horizontal">
    <hamburger class="hamburger-container" :toggleClick="toggleSideBar" :isActive="sidebar.opened"></hamburger>
    <levelbar></levelbar>
    <div class="functionContainer">
      <div class="functionButton">{{ name }}</div>
      <div class="functionButton">
        <el-dropdown trigger="click">
          <div class="el-dropdown-link dropDownIcon ">
              <i class="el-icon-setting"></i>
          </div>
          <el-dropdown-menu class="user-dropdown" slot="dropdown">
            <router-link class='inlineBlock' to="/">
              <el-dropdown-item>
                主页
              </el-dropdown-item>
            </router-link>
            <el-dropdown-item divided><span @click="onChangedPasswrod">修改密码</span></el-dropdown-item>
            <el-dropdown-item divided><span @click="logout" style="display:block;">注销</span></el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </div>

    </div>

     
  </el-menu>
 
  <el-dialog title="修改密码" :visible.sync="dialogChangedPasswrodVisible">
        <changedPasswrod @close="dialogChangedPasswrodVisible = false" ref="changedPasswrodComponent"></changedPasswrod>
  </el-dialog>
</div>
  
</template>

<script>
import { mapGetters } from 'vuex'
import Levelbar from './Levelbar'
import Hamburger from '@/components/Hamburger'
import changedPasswrod from './changedPasswrod'

export default {
  components: {
    Levelbar,
    Hamburger,
    changedPasswrod
  },
  data() {
      return {
        dialogChangedPasswrodVisible : false,
    }
  },
  computed: {
    ...mapGetters([
      'sidebar',
      'avatar',
      'name'
    ])
  },
  methods: {
    toggleSideBar() {
      this.$store.dispatch('ToggleSideBar')
    },
    logout() {
      this.$store.dispatch('LogOut').then(() => {
        location.reload()  // 为了重新实例化vue-router对象 避免bug
      })
    },
    onChangedPasswrod(){
      this.dialogChangedPasswrodVisible = true;
      if (this.$refs.changedPasswrodComponent) {
          this.$refs.changedPasswrodComponent.resetInput();
      }
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
    .navbar {
        height: 50px;
        line-height: 50px;
        border-radius: 0px !important;
        .hamburger-container {
            line-height: 58px;
            height: 50px;
            float: left;
            padding: 0 10px;
        }
        .errLog-container {
            display: inline-block;
            position: absolute;
            right: 150px;
        }
        .screenfull {
            position: absolute;
            right: 90px;
            top: 16px;
            color: red;
        }

        .functionContainer{
            height: 50px;
            float: right;
            margin-right: 35px;

            .functionButton{
              display: inline-block;

              .dropDownIcon{
                &:hover{
                  color: #394eff;
                  font-size: 17px;
                }

                font-size: 17px;
                transition: font-size 0.5s;
                cursor: pointer;
                height: 50px;
                width: 50px;
                text-align: center;
              }
            }
        }
    }
</style>

