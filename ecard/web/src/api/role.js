import fetch from '@/utils/fetch'
import axios from 'axios'

export function addRole(form) {
    return fetch({
        url: '/role/AddRole',
        method: 'post',
        data: form,
    })

}


export function updateRole(form) {
    return fetch({
        url: '/role/UpdateRole',
        method: 'post',
        data: form,
    })
}

export function deleteRole(idArray) {
    return fetch({
        url: '/role/DeleteRole',
        method: 'post',
        data: idArray,
    })
}

export function getRole(query) {
    return fetch({
        url: '/role/GetRoles',
        method: 'post',
        data: query
    })
}

export function getRoleUser(id) {
    return fetch({
        url: '/role/GetRoleUser',
        method: 'get',
        params: id
    })
}

export function saveRoleUser(roleUser) {
    return fetch({
        url: '/role/SaveRoleUser',
        method: 'post',
        data: roleUser
    })
}

export function getRoleModuleAuth(id) {
    return fetch({
        url: '/role/GetRoleModuleAuth',
        method: 'get',
        params: id
    })
}

export function saveRoleModuleAuth(moduleAuth) {
    return fetch({
        url: '/role/SaveRoleModuleAuth',
        method: 'post',
        data: moduleAuth
    })
}