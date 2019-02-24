import fetch from '@/utils/fetch'
import axios from 'axios'

export function addOrganize(form) {
    return fetch({
        url: '/organize/AddOrganize',
        method: 'post',
        data: form,
    })

}

export function updateOrganize(form) {
    return fetch({
        url: '/organize/UpdateOrganize',
        method: 'post',
        data: form,
    })
}

export function deleteOrganize(idArray) {
    return fetch({
        url: '/organize/DeleteOrganize',
        method: 'post',
        data: idArray,
    })


}
export function getOrganize(request) {
    return fetch({
        url: '/organize/GetOrganize',
        method: 'post',
        data: request
    })
}

export function getCurrentUserOrganizes() {
    return fetch({
        url: '/organize/GetCurrentUserOrganizes',
        method: 'get',
    }) 
}