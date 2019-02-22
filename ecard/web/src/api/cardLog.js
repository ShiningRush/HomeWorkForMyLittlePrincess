import fetch from '@/utils/fetch'

export function getCardLogs(query) {
    return fetch({
        url: '/cardLog/GetCardLogList',
        method: 'post',
        data: query
    })
}