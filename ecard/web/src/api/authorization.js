import fetch from '@/utils/fetch'
import axios from 'axios'

///获取app信息
export function getAllApp() {
    return fetch({
        url: 'authorization/GetAllApp',
        method: 'get',
        data: '',     
    })      
}
