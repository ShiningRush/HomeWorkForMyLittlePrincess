import Vue from 'vue'
import ElementUI from 'element-ui'
//import 'element-ui/lib/theme-chalk/index.css'
//import locale from 'element-ui/lib/locale/lang/en'
import 'babel-polyfill'
import App from './App'
import router from './router'
import store from './store'
import '@/icons' // icon
import '@/permission' // 权限
import globalSetting from '@/utils/globalSetting'

import RegionPicker from 'region-picker'

Vue.use(ElementUI)
Vue.use(RegionPicker)

Vue.config.productionTip = false
    //权限指令
Vue.directive('has', {
        bind: function(el, binding) {
            if (!Vue.prototype.$_has(binding.value)) {
                el.parentNode.removeChild(el);
            }
        }
    })
    //防止二次提交指令
Vue.directive('dbClick', {
    inserted: function(el, binding) {
        el.addEventListener('click', e => {
            if (!el.disabled) {
                el.disabled = true;
                let timer = setTimeout(() => {
                    el.disabled = false;
                }, 1000)
            }
        })
    }
})

globalSetting.initServerConfig(() => {
    new Vue({
        el: '#app',
        router,
        store,
        template: '<App/>',
        components: { App }
    })
})