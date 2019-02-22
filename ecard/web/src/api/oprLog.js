import fetch from '@/utils/fetch'

export function getOprLogs(queryCriteria) {
    return fetch({
        url: 'oprLog/GetOprLogs',
        method: 'post',
        data: queryCriteria
    })
}