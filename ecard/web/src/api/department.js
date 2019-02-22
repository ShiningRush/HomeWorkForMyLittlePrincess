import fetch from '@/utils/fetch'
import axios from 'axios'

//创建科室
export function addDepartment(form) {
    return fetch({
        url: '/department/AddDepartment',
        method: 'post',
        data: form,
    })
}

//修改科室
export function updateDepartment(form) {
    return fetch({
        url: '/department/UpdateDepartment',
        method: 'post',
        data: form,
    })
}

//删除科室
export function deleteDepartment(params) {
    return fetch({
        url: '/department/DeleteDepartment',
        method: 'post',
        data: params
    })
}

//查询科室
export function GetDepartment(form) {
    return fetch({
        url: '/department/GetDepartment',
        method: 'post',
        data: form
    })
} 

//根据机构获取部门

export function GetDepartmentByOrganizeId(id) {
    return fetch({
         url: '/department/GetDepartmentByOrganizeId',
        method: 'get',
        params: id
    })
}