import axios from 'axios'
import qs from 'qs'
import { oauthTokenManager } from '@/utils/auth'

// 创建axios实例
const service = axios.create({
    baseURL: process.env.BASE_API, // api的base_url
    timeout: 15000 // 请求超时时间
})

export function ssoLogin(token, userName) {
    return service({
        url:  '/Authorization/Token',
        method: 'post',
        data: qs.stringify({
            'grant_type' : 'ssoLogin',
            'username' : userName,
            'token' : token
        }),
        auth: {
            username:"webapp",
            password:""
        }
    })
}

export function ssoBind(userId, appId) {
    return service({
        url:  '/SsoService/BindAccount',
        method: 'post',
        params: {
            userId,
            appId
        },
        headers: {'Authorization':'Bearer ' + oauthTokenManager.getToken()}
    })
}