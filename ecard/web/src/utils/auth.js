import Cookies from 'js-cookie'
import {refreshToken} from '../api/authenticate'

let authorizationState  = undefined
// 还剩多少时间时自动刷新Token(分钟)
const AutoRefreshToken_RestTime = 1
const TokenKey = 'Admin-Token'
let oauthTokenManager = {
  getToken(){
    if(!authorizationState){
      const cookieString = Cookies.get(TokenKey)
      if (cookieString){
        authorizationState = JSON.parse(cookieString)
      }
      else{
        return undefined
      }
    }

    const expireTime = new Date(authorizationState.espireTime)
    const expireTimeDiff = (expireTime - Date.now()) / 1000


    if (expireTimeDiff <= AutoRefreshToken_RestTime * 60 && expireTimeDiff > 0){
      refreshAccessToken()
    }

    if (expireTimeDiff < 0)
      return undefined

    return authorizationState.accessToken
  },
  setToken(oauthResult){
    authorizationState = getAuthorizationState(oauthResult)
    Cookies.set(TokenKey, JSON.stringify(authorizationState))
  },
  removeToken(){
    authorizationState = undefined
    return Cookies.remove(TokenKey)
  }
}

function getAuthorizationState(oauthResult){
  return {
    accessToken : oauthResult.access_token,
    refreshToken : oauthResult.refresh_token,
    espireTime : Date.now() + (oauthResult.expires_in * 1000)
  }
}

function refreshAccessToken(){
  if(!authorizationState){
    return
  }
  refreshToken(authorizationState.refreshToken).then(axiosResp =>{
    if(axiosResp.status != 200){
      return
    }

    const respData = axiosResp.data
    oauthTokenManager.setToken(respData)
  })
}

export { oauthTokenManager }
