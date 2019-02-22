import fetch from '@/utils/fetch'
import axios from 'axios'

//结算
export function SettleBillingRecord(settleTime) {
    return fetch({
        url: '/settlement/SettleBillingRecord',
        method: 'post',
        data:{   
            settleTime
        }
    })
}

//获取结算报表数据
export function GetNotSettledBillingGraphData(settleTime) {
    return fetch({
        url: '/settlement/GetNotSettledBillingGraphData',
        method: 'post',
        data:{
            settleTime
        }
    })
}

//获取结算明细
export function GetSettlementList(queryCriteria) {
    return fetch({
        url: '/settlement/GetSettlementList',
        method: 'post',
        data: queryCriteria
    })
}

//获取单次结算的交易记录
export function GetSingleSettlementDetail(settlementId) {
    return fetch({
        url: '/settlement/GetSingleSettlementDetail',
        method: 'post',
        data: { settlementId }
    })
}