import fetch from '@/utils/fetch'
import axios from 'axios'

//获取黑白名单
export function GetBlacklist(form) {
    return fetch({
        url: '/blacklist/GetBlacklist',
        method: 'post',
        data: form,
    })
}

//添加黑白名单
export function AddBlacklist(form) {
    return fetch({
        url: '/blacklist/AddBlacklist',
        method: 'post',
        data: form,
    })
}

//修改黑白名单
export function UpdateBlacklist(form) {
    return fetch({
        url: '/blacklist/UpdateBlacklist',
        method: 'post',
        data: form,
    })
}

//删除黑白名单
export function DeleteBlacklist(id) {
    return fetch({
        url: '/blacklist/DeleteBlacklist',
        method: 'post',
        params: {id},
    })
}