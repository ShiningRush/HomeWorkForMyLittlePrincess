import fetch from '@/utils/fetch'
import axios from 'axios'

///获取用户信息
export function getUsers(form) {
    return fetch({
        url: 'user/GetUsers',
        method: 'post', 
        data: form,         
    })      
}

//创建用户
export function createUser(form) {
    return fetch({
        url: '/user/CreateUser',
        method: 'post',
        data: form, 
    })
}

//重置密码
export function restUserPassword (userId) {
    return fetch({
        url: '/user/RestUserPassword',
        method: 'post',
        params: {userId}, 
    })
}

//删除用户
export function delUserById (userId) {
    return fetch({
        url: '/user/DeleteUser',
        method: 'post',
        params: {userId}, 
    })
}

//修改用户
export function updateUser (form) {
    return fetch({
        url: '/user/UpdateUser ',
        method: 'post',
        data: form, 
    })
}
//获取简单用户信息列表
export function getSimpleUsers(request) {
    return fetch({
        url: '/user/GetSimpleUsers ',
        method: 'post',
        data: request,
    })
}
//修改用户密码
export function changePassword(request) {
    return fetch({
        url: '/user/ChangePassword',
        method: 'post',
        data: request,
    })
}