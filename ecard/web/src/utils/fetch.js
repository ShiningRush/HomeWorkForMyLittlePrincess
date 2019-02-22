import axios from 'axios'
import { Message } from 'element-ui'
import router from '../router'
import { oauthTokenManager } from '@/utils/auth'
import store from '../store'

// 创建axios实例
const service = axios.create({
    baseURL: process.env.BASE_API, // api的base_url
    timeout: 15000 // 请求超时时间
})

// request拦截器
service.interceptors.request.use(config => {
    if (oauthTokenManager.getToken()) {
        config.headers['Authorization'] = 'Bearer ' + oauthTokenManager.getToken() // 让每个请求携带token
    }

    return config
}, error => {
    // Do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
})

// respone拦截器
service.interceptors.response.use(
    response => {
        /**
         * code为非0是抛错 
         */
        const res = response.data
        if (res.code !== '0') {
            let message = "访问接口发生未知错误"
            if (res.message && res.message != "") {
                message = res.message;
            }
            Message({
                message: message,
                type: 'error',
                duration: 5 * 1000
            })

            console.error("response code:" + res.code, "response message:" + res.message)
            return Promise.reject(res)
        }

        return res.data
    },
    error => {
        var errorMessage = '访问接口发生未知错误'
        if(error.code == 'ECONNABORTED'){
            errorMessage = '访问接口超时，请确认服务端是否正常工作后再次尝试该操作'
        }

        if (error.response && error.response.status == 401) {
            errorMessage = '登录信息过期, 请重新登录'
            store.dispatch('FedLogOut').then(() => {
                router.push('/login')
            })
        }
        Message({
            message: errorMessage,
            type: 'error',
            duration: 5 * 1000
        })
        return Promise.reject(error)
    }
)

export default service