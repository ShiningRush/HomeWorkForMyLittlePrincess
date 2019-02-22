<template>
  <div class="app-wrapper" :class="{hideSidebar:!sidebar.opened}" >
    <div class="sidebar-wrapper">
      <div class="sidebar-title">一卡通管理平台</div>
      <div class="sidebar-icon">
        <div class="logo-container">
          <img :src="logo">
          <span>{{ name }}</span>
        </div>
        <div class="sidebar-split-line"></div>
      </div>
      <div ref="sidebarcontainer" class="sidebar-container" id='sidebarContainer'>
        <sidebar ></sidebar>
      </div>
    </div>
    <div class="main-container">
      <navbar></navbar>
      <app-main></app-main>
    </div>
    
  </div>
</template>


<script>
import { Navbar, Sidebar, AppMain } from '@/views/layout'
import logo from '@/assets/logo.png'
import { mapGetters } from 'vuex'

export default {
  name: 'layout',
  mounted(){
    var sidebarContainer  = document.getElementById('sidebarContainer');
    sidebarContainer.heigt = window.screen.availHeight - 350
  },
  data(){
    return {
      logo
    }
  },
  components: {
    Navbar,
    Sidebar,
    AppMain
  },
  computed: {
    sidebar() {
      return this.$store.state.app.sidebar
    },
    ...mapGetters(["name"])
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
  @import "src/styles/variable.scss";
  @import "src/styles/mixin.scss";
  
  .app-wrapper {
      @include clearfix;
      position: relative;
      height: 100%;
      width: 100%;
      &.hideSidebar {
          .sidebar-wrapper {
              transform: translate(-140px, 0);
              .sidebar-container {
                  transform: translate(132px, 0);
              }
              &:hover {
                  transform: translate(0, 0);
                  .sidebar-container {
                      transform: translate(0, 0);
                  }
              }
          }
          .main-container {
              margin-left: 40px;             
          }
      }
      .sidebar-wrapper {
          background-color: white;
          width: 180px;
          position: fixed;
          top: 0;
          bottom: 0;
          left: 0;
          z-index: 1001;
          overflow: hidden;
          transition: all .28s ease-out;

          .sidebar-title{
            font-size: 18px;
            padding: 15px 0 0 14px;
            color: white;
            width: 100%;
            height: $sidebar-title-height;
            background-color: $primay-color;
          }

          .sidebar-icon{
              width: 100%;
              height: $sidebar-icon-height;
              
              .logo-container{
                text-align: center;
                font-size: 14px;
                img{
                  border-radius: 50%;
                  border: 1px solid lightgrey;
                  display: block;
                  margin: 30px auto;
                }
              }

            .sidebar-split-line{
              margin-top:10px;
              height: 20px;
              background-color: #f0f0f7;
            }
          }
      }
      .sidebar-container {
          transition: all .28s ease-out;
          position: absolute;
          top: $sidebar-icon-height + $sidebar-title-height;
          bottom: 0px;
          left: 0;
          right: -17px;
          overflow-y: scroll;
          overflow-x: hidden;
      }
     
     @mixin main-container-common{
         transition: all .28s ease-out;
         margin-left: 180px;
         overflow-y: auto;
     }
    @media screen and (min-height: 1080px) {
      .main-container {
         @include main-container-common;
         height:1080px
    }
  }
  @media screen and (min-height: 900px) and(max-height:1080px) {
      .main-container {
         @include main-container-common;
         height:920px
    }
  }
   @media screen and (max-height:900px) {
      .main-container {
         @include main-container-common;
         height:740px
    }
   }
  }
</style>
