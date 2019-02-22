import { login, logout, getUserInfo } from '@/api/authenticate'
import { oauthTokenManager } from '@/utils/auth'
import { getTreeObj } from '@/utils/treeListHandle'

const user = {
    state: {
        token: oauthTokenManager.getToken(),
        name: '',
        avatar: '',
        roles: [],
        modules: [],
        moduleList: [],
        moduleAuths: []
    },

    mutations: {
        SET_NAME: (state, name) => {
            state.name = name
        },
        SET_AVATAR: (state, avatar) => {
            state.avatar = avatar
        },
        SET_ROLES: (state, roles) => {
            state.roles = roles
        },
        SET_MODULES: (state, modules) => {
            state.modules = modules
        },
        SET_MODULELIST: (state, modules) => {
            state.moduleList = modules
        },
        SET_MODULEAUTHS: (state, moduleAuths) => {
            state.moduleAuths = moduleAuths
        }
    },

    actions: {
        // 登录
        Login({ commit }, userInfo) {
            const username = userInfo.username.trim()
            return new Promise((resolve, reject) => {
                login(username, userInfo.password).then(response => {
                    const oauthResp = response.data
                    oauthTokenManager.setToken(oauthResp)
                    resolve()
                }).catch(error => {
                    reject(error)
                })
            })
        },

        // 获取用户信息
        GetInfo({ commit, state }) {
            return new Promise((resolve, reject) => {
                getUserInfo().then(response => {
                    const data = response
                    commit('SET_NAME', data.userName)
                    commit('SET_MODULES', getTreeObj(data.modules))
                    commit('SET_MODULELIST', data.modules)
                    commit('SET_MODULEAUTHS', data.moduleAuths)
                    resolve(response)
                }).catch(error => {
                    reject(error)
                })
            })
        },

        // 登出
        LogOut({ commit, state }) {
            return new Promise((resolve, reject) => {
                logout(state.token).then(() => {
                    commit('SET_ROLES', [])
                    oauthTokenManager.removeToken()
                    resolve()
                }).catch(error => {
                    reject(error)
                })
            })
        },

        // 前端 登出
        FedLogOut({ commit }) {
            return new Promise(resolve => {
                oauthTokenManager.removeToken()
                resolve()
            })
        }

    }
}


export default user