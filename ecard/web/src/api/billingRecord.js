import fetch from '@/utils/fetch'

export function getBillingRecordList(query) {
    return fetch({
        url: '/billingRecord/GetBillingRecordList',
        method: 'post',
        data: query
    })
}

export function deriveBillingRecordList(query) {
    return fetch({
        url: '/billingRecord/deriveBillingRecordList',
        method: 'get',
        params: query
    })
}