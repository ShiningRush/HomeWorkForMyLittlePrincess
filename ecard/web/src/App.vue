<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
import Vue from 'vue';
import {mapGetters} from 'vuex'
import store from './store'
import router from './router'

export default {
  name: 'app',
  created() {
    Vue.prototype.$_has = function(code){
            let moduleList = store.getters.moduleList;
            let moduleAuths = store.getters.moduleAuths;           
            let currentPath = router.currentRoute.path;
            let hasPermission = false;
            for(var i= 0;i< moduleList.length;i++){
                  if(moduleList[i].urlAddress && moduleList[i].urlAddress.toLowerCase() == currentPath.toLowerCase()){                       
                         let buttons = moduleAuths.filter(m=>m.moduleId == moduleList[i].id && m.code.toLowerCase() == code.toLowerCase());                       
                         if(buttons && buttons.length == 1){
                           hasPermission = true;
                         }
                          break ;
                      }
            }          
            return hasPermission;
         }
  },
 
}
</script>

<style lang="scss">
  @import '~normalize.css/normalize.css';// normalize.css 样式格式化
  @import './styles/index.scss'; // 全局自定义的css样式
</style>
