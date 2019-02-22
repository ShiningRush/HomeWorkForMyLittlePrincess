import axios from 'axios'

const globalSetting = {
    dbStatus: {}
};

globalSetting.initServerConfig = function(onInitedCallback){
    const backApi = axios({
        url:  process.env.SUB_API + '/SystemInit/GetDbStatus',
        method: 'get'
    });

    backApi.then(resp=>{
        globalSetting.isServerRunning = true;
        globalSetting.dbStatus = resp.data.data;
        globalSetting.dbStatus.isNeedToInitPage = !(globalSetting.dbStatus.isInited && globalSetting.dbStatus.isLatest)
        if(onInitedCallback != undefined){
            onInitedCallback();
        }
    }).catch(error=>{
        globalSetting.dbStatus.isNeedToInitPage = false;
        globalSetting.isServerRunning = false;
        if(onInitedCallback != undefined){
            onInitedCallback();
        }
    })
}
export default globalSetting