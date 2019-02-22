import Vue from 'vue'
import Router from 'vue-router'
const _import = require('./_import_' + process.env.NODE_ENV)
    // in development env not use Lazy Loading,because Lazy Loading too many pages will cause webpack hot update too slow.so only in production use Lazy Loading

/* layout */
import Layout from '../views/layout/Layout'

Vue.use(Router)

/**
 * icon : the icon show in the sidebar
 * hidden : if `hidden:true` will not show in the sidebar
 * redirect : if `redirect:noredirect` will not redirct in the levelbar
 * noDropdown : if `noDropdown:true` will not has submenu in the sidebar
 * meta : `{ role: ['admin'] }`  will control the page role
 **/
export const constantRouterMap = [{
        path: '/',
        redirect: '/systemManage',
        name: 'master'
    },
    { path: '/login', component: _import('login/index') },
    { path: '/initSystem', component: _import('initSystem') },
    { path: '/ssoBind', component: _import('ssoBind') },
    { path: '/404', component: _import('404') },
    {
        path: '/cardUserManage',
        name: '卡用户管理',
        component: Layout,
        redirect: '/cardUserManage/basicInfoManage',
        noDropdown: false,
        children: [
            { path: 'basicInfoManage', name: '帐户信息管理', component: _import('cardUserManage/basicInfoManage') },
            { path: 'cardStatusManage', name: '卡状态管理', component: _import('cardUserManage/cardStatusManage') },
            { path: 'blackWhiteListManage', name: '黑/白名单管理', component: _import('cardUserManage/blackWhiteListManage') },
            { path: 'fundingAccountManage', name: '资金账户管理', component: _import('cardUserManage/fundingAccountManage') }
        ]
    },
    {
        path: '/businessInquiryManage',
        name: '业务查询统计',
        component: Layout,
        redirect: '/businessInquiryManage/cardLogManage',
        noDropdown: false,
        children: [
            { path: 'cardLogManage', name: '卡务日志管理', component: _import('businessInquiryManage/cardLogManage') },
            { path: 'cardDetailsManage', name: '用卡明细查询', component: _import('businessInquiryManage/cardDetailsManage') },
        ]
    },
    {
        path: '/dailySettleManage',
        name: '日结管理',
        component: Layout,
        redirect: '/dailySettleManage/dailySettleReport',
        noDropdown: false,
        children: [
            { path: 'dailySettleReport', name: '日结报表', component: _import('dailySettleManage/dailySettleReport') },
            { path: 'dailySettleDetails', name: '日结明细查询', component: _import('dailySettleManage/dailySettleDetails') },
        ]
    },
    {
        path: '/systemManage',
        name: '系统管理',
        component: Layout,
        redirect: '/systemManage/orgManage',
        noDropdown: false,
        children: [
            { path: 'orgManage', name: '机构管理', component: _import('systemManage/orgManage') },
            { path: 'deptManage', name: '部门管理', component: _import('systemManage/deptManage') },
            { path: 'userManage', name: '用户管理', component: _import('systemManage/userManage') },
            { path: 'rolePermsManage', name: '角色与权限管理', component: _import('systemManage/rolePermsManage') },
            { path: 'menuManage', name: '菜单管理', component: _import('systemManage/menuManage') },
            { path: 'dicManage', name: '字典管理', component: _import('systemManage/dicManage') },
            { path: 'logManage', name: '日志管理', component: _import('systemManage/logManage') },
            { path: 'appManage', name: '应用管理', component: _import('systemManage/appManage') }
        ]
    },
    { path: '*', redirect: '/404', hidden: true }
]

export default new Router({
    // mode: 'history', //后端支持可开
    scrollBehavior: () => ({ y: 0 }),
    routes: constantRouterMap
})