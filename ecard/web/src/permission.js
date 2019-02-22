import router from './router'
import store from './store'
import NProgress from 'nprogress' // Progress 进度条
import 'nprogress/nprogress.css' // Progress 进度条样式
import { Message } from 'element-ui'
import { oauthTokenManager } from '@/utils/auth' // 验权
import globalSetting from '@/utils/globalSetting'


const whiteList = ['/login', '/initSystem', '/404'] // 不重定向白名单
router.beforeEach((to, from, next) => {
    NProgress.start()
        // 未检测到服务器时，重定向
    if (!globalSetting.isServerRunning) {
        if (to.path != '/404') {
            next({ path: '/404' });
            return;
        }
    }

    // 服务器未初始化时, 重定向
    if (to.path != '/initSystem' && globalSetting.isServerRunning === true && globalSetting.dbStatus.isNeedToInitPage === true) {
        next({ path: '/initSystem' });
        return;
    }
    if (oauthTokenManager.getToken()) {
        if (to.path === '/login') {
            if (to.query.goPath) {
                next({ path: to.query.goPath })
            } else {
                next({ path: '/' })
            }
        } else {
            if (store.getters.modules.length == 0) {
                store.dispatch('GetInfo').then(res => { // 拉取用户信息

                    if (res.modules.length == 0) {
                        Message.error('登录用户无权限')
                        store.dispatch('FedLogOut').then(() => {
                            next({ path: '/login' })
                        })
                    } else {
                        if (!validatePermission(to.path)) {
                            let path = "";
                            if (store.getters.modules[0].children[0].urlAddress) {
                                path = store.getters.modules[0].children[0].urlAddress;
                            } else {
                                path = store.getters.modules[0].urlAddress;
                            }
                            next({ path: path })
                        } else {
                            next();
                        }

                    }

                }).catch(() => {
                    store.dispatch('FedLogOut').then(() => {
                        Message.error('获取用户信息失败,请重新登录')
                        next({ path: '/login' })
                    })
                })

            } else {
                next()
            }
        }

    } else {
        if (whiteList.indexOf(to.path) !== -1) {
            next()
        } else {
            Message.error('用户信息过期, 请重新登录')
            next({ path: '/login', query: { goPath: to.path } })
        }
    }

})

router.afterEach(() => {
    NProgress.done() // 结束Progress
})

//判断跳转的页面是否有权限
function validatePermission(path) {
    let hasPermission = false;
    let moduleList = store.getters.moduleList;
    if (path && path != "") {
        for (var i = 0; i < moduleList.length; i++) {
            if (moduleList[i].urlAddress && moduleList[i].urlAddress.toLowerCase() == path.toLowerCase()) {
                hasPermission = true;
                break;
            }

        }

    }
    return hasPermission;
}