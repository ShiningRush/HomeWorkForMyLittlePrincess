import fetch from '@/utils/fetch'

//获取卡状态列表
export function getCards(request) {
    return fetch({
        url: '/card/GetCards',
        method: 'post',
        data: request
    })
}
//挂失卡
export function loss(request) {
    return fetch({
        url: '/card/Loss',
        method: 'post',
        data: request
    })
}
//解挂卡
export function releaseLoss(request) {
    return fetch({
        url: '/card/ReleaseLoss',
        method: 'post',
        data: request
    })
}
//补换卡
export function replaceCard(request) {
    return fetch({
        url: '/card/ReplaceCard',
        method: 'post',
        data: request
    })
}
//退卡
export function cancellationCard(request) {
    return fetch({
        url: '/card/CancellationCard',
        method: 'post',
        data: request
    })
}