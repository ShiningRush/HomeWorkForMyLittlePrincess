import fetch from '@/utils/fetch'
import axios from 'axios'
import qs from 'qs'

// 创建axios实例
const service = axios.create({
    baseURL: process.env.BASE_API, // api的base_url
    timeout: 15000 // 请求超时时间
})

export function login(username, password) {
    return service({
        url:  '/Authorization/Token',
        method: 'post',
        data: qs.stringify({
            'grant_type' : 'password',
            'username' : username,
            'password' : password
        }),
        auth: {
            username:"webapp",
            password:""
        }
    })
}


export function refreshToken(refreshToken) {
    return service({
        url: '/Authorization/Token',
        method: 'post',
        data: qs.stringify({
            'grant_type' : 'refresh_token',
            'refresh_token' : refreshToken
        }),
        auth: {
            username:"webapp",
            password:""
        }
    })
}

export function getUserInfo() {
    return fetch({
        url: '/user/GetUserInfo',
        method: 'get'
    })
}

export function logout() {
    return fetch({
        url: '/user/logout',
        method: 'post'
    })
}