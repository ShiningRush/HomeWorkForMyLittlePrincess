import fetch from '@/utils/fetch'

export function getDataItem(queryinfo) {
    return fetch({
        url: '/dataItem/GetDataItem',
        method: 'post',
        data: queryinfo
    })
}

export function addDataItem(dicinfo) {
    return fetch({
        url: '/dataItem/AddDataItem',
        method: 'post',
        data: dicinfo
    })
}

export function updateDataItem(dicinfo) {
    return fetch({
        url: '/dataItem/UpdateDataItem',
        method: 'post',
        data: dicinfo
    })
}

export function deleteDataItem(idArray) {
    return fetch({
        url: '/dataItem/DeleteDataItem',
        method: 'post',
        data: idArray
    })
}

export function addDataItemDetail(listinfo) {
    return fetch({
        url: '/dataItem/AddDataItemDetail',
        method: 'post',
        data: listinfo
    })
}

export function updateDataItemDetail(listinfo) {
    return fetch({
        url: '/dataItem/UpdateDataItemDetail',
        method: 'post',
        data: listinfo
    })
}

export function getDataItemDetail(queryDetail) {
    return fetch({
        url: '/dataItem/GetDataItemDetail',
        method: 'post',
        data: queryDetail
    })
}

export function deleteDataItemDetail(idArray) {
    return fetch({
        url: '/dataItem/DeleteDataItemDetail',
        method: 'post',
        data: idArray
    })
}

export function getDetailByCode (Code) {
    return fetch({
        url: '/dataItem/GetDetailByCode',
        method: 'get',
        params: {"itemCode":Code}
    })
}