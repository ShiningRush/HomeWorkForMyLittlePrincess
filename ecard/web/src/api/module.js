import fetch from '@/utils/fetch'
import axios from 'axios'

export function getAllModules() {
    return fetch({
        url: '/module/GetAllModules',
        method: 'get',
    })

}

export function updateModuleAuth(form) {
    return fetch({
        url: '/module/UpdateModuleAuth',
        method: 'post',
        data: form
    })

}

export function deleteModule(idArray) {
    return fetch({
        url: '/module/DeleteModule',
        method: 'post',
        data: idArray
    })

}

export function getModuleAuths(moduleArray) {
    return fetch({
        url: '/module/GetModuleAuths',
        method: 'post',
        data: moduleArray
    })

}
export function insertModuleAuth(auth) {
    return fetch({
        url: '/module/InsertModuleAuth',
        method: 'post',
        data: auth
    })

}

export function deleteModuleAuth(idArray) {
    return fetch({
        url: '/module/DeleteModuleAuth',
        method: 'post',
        data: idArray
    })

}