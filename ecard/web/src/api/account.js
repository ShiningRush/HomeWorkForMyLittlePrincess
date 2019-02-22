import fetch from '@/utils/fetch'
import axios from 'axios'

//根据卡号获取账户信息
export function GetAccountByCard(cardNo) {
    return fetch({
        url: '/account/GetAccountByCard',
        method: 'get',
        params: { cardNo }
    })
}

//充值
export function AccountRecharge(data) {
    return fetch({
        url: '/account/AccountRecharge',
        method: 'post',
        data: data
    })
}

//退费
export function AccountDeductionFee(data) {
    return fetch({
        url: '/account/Refund',
        method: 'post',
        data: data
    })
}

//退卡
export function CancellationCard(data) {
    return fetch({
        url: '/account/CancellationCard',
        method: 'post',
        params: data
    })
}

//创建账户
export function createAccount(form) {
    return fetch({
        url: '/account/CreateAccount',
        method: 'post',
        data: form
    })

}

//更新账户
export function updateAccount(form) {
    return fetch({
        url: '/account/UpdateAccount',
        method: 'post',
        data: form
    })

}
//获取账户列表
export function getAccounts(query) {
    return fetch({
        url: '/account/GetAccounts',
        method: 'post',
        data: query
    })

}

//冻结账户
export function freezeAccount(id) {
    return fetch({
        url: '/account/FreezeAccount',
        method: 'get',
        params: id
    })
}

//解冻账户
export function thawAccount(id) {
    return fetch({
        url: '/account/ThawAccount',
        method: 'get',
        params: id
    })
}